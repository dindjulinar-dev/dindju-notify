Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.IO
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class Form1

    ' ===============================================
    '           Variables Globales
    ' ===============================================
    Private Const PORT As Integer = 3600
    Private Const AUTH_TOKEN As String = "DINDJU-SECRET-2024"  ' ← Changez ce token !
    Private serverListener As TcpListener
    Private cancellationTokenSource As CancellationTokenSource
    Private serverRunning As Boolean = False
    Private Const MAX_CONCURRENT_CLIENTS As Integer = 10
    Private currentClientCount As Integer = 0

    ' Référence unique au formulaire de verrouillage
    Private lockedFormInstance As LockedForm = Nothing

    ' ===============================================
    '           Variables Live View (visionnage à distance)
    ' ===============================================
    Private liveViewForm As Form = Nothing
    Private liveViewPictureBox As PictureBox = Nothing
    Private liveViewCTS As CancellationTokenSource = Nothing
    Private liveViewRunning As Boolean = False
    Private liveViewStopping As Boolean = False

    ' ===============================================
    '           API Volume (NAudio alternatif via WinAPI)
    ' ===============================================
    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    Private Const WM_APPCOMMAND As Integer = &H319
    Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000

    Private Shared volumeMuted As Boolean = False

    ' ===============================================
    '           Initialisation du Formulaire
    ' ===============================================
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MAJs.Show()

        NotifyIcon1.Visible = True
        NotifyIcon1.Text = "Enterprise Notifier"
        If NotifyIcon1.Icon Is Nothing Then
            NotifyIcon1.Icon = System.Drawing.SystemIcons.Information
        End If

        lblServerStatus.Text = "Initialisation..."
        txtServerIP.Text = "127.0.0.1"
        rtbLog.ReadOnly = True
        txtRemoteAppToStart.Text = "C:\Windows\System32\"

        SetupNotifyIconContextMenu()
        StartServerAuto()
    End Sub

    Private Sub StartServerAuto()
        Try
            serverListener = New TcpListener(IPAddress.Any, PORT)
            cancellationTokenSource = New CancellationTokenSource()
            serverListener.Start()
            serverRunning = True
            lblServerStatus.Text = "Serveur Actif (Port " & PORT & ")"
            lblServerStatus.ForeColor = Color.Green
            LogMessage("Serveur démarré automatiquement sur le port " & PORT)

            Task.Factory.StartNew(Sub() ListenForClients(cancellationTokenSource.Token), TaskCreationOptions.LongRunning)
        Catch ex As Exception
            lblServerStatus.Text = "Erreur Serveur"
            lblServerStatus.ForeColor = Color.Red
            LogMessage("Erreur démarrage serveur : " & ex.Message, Color.Red)
        End Try
    End Sub

    ' ===============================================
    '           Gestion des Connexions
    ' ===============================================
    Private Sub ListenForClients(token As CancellationToken)
        While serverRunning AndAlso Not token.IsCancellationRequested
            Try
                If serverListener.Pending() Then
                    If currentClientCount >= MAX_CONCURRENT_CLIENTS Then
                        LogMessage("Connexion refusée : limite de " & MAX_CONCURRENT_CLIENTS & " clients atteinte.", Color.OrangeRed)
                        Thread.Sleep(200)
                        Continue While
                    End If

                    Dim client As TcpClient = serverListener.AcceptTcpClient()
                    client.ReceiveTimeout = 5000  ' Timeout 5 secondes (commandes normales)
                    client.SendTimeout = 10000    ' Timeout envoi 10 secondes

                    Interlocked.Increment(currentClientCount)
                    Task.Factory.StartNew(Sub() HandleClient(client), token)
                Else
                    Thread.Sleep(100)
                End If
            Catch ex As SocketException
                If serverRunning Then
                    LogMessage("Erreur socket : " & ex.Message, Color.Red)
                End If
            Catch ex As Exception
                Exit While
            End Try
        End While
    End Sub

    Private Sub HandleClient(client As TcpClient)
        Dim clientIP As String = "inconnu"
        Try
            clientIP = CType(client.Client.RemoteEndPoint, IPEndPoint).Address.ToString()

            Using networkStream As NetworkStream = client.GetStream()
                Dim reader As New StreamReader(networkStream, Encoding.UTF8)
                Dim msg As String = reader.ReadLine()

                If String.IsNullOrEmpty(msg) Then Return

                ' --- Vérification du token d'authentification ---
                ' Format attendu : "TOKEN:DINDJU-SECRET-2024|COMMANDE:PAYLOAD"
                If msg.Contains("|") Then
                    Dim parts() As String = msg.Split(New Char() {"|"c}, 2)
                    If parts(0).StartsWith("TOKEN:", StringComparison.OrdinalIgnoreCase) Then
                        Dim receivedToken As String = parts(0).Substring(6).Trim()
                        If receivedToken <> AUTH_TOKEN Then
                            LogMessage("Connexion refusée depuis " & clientIP & " (token invalide)", Color.Red)
                            Return
                        End If
                        msg = parts(1).Trim() ' On traite la commande réelle
                    Else
                        LogMessage("Connexion sans token depuis " & clientIP & " — refusée.", Color.OrangeRed)
                        Return
                    End If
                Else
                    ' Compatibilité mode sans token (réseau local de confiance uniquement)
                    LogMessage("Commande sans auth depuis " & clientIP & " (mode non sécurisé)", Color.Orange)
                End If

                LogMessage("[" & clientIP & "] Commande : " & msg, Color.Blue)
                ProcessCommand(msg, networkStream)
            End Using
        Catch ex As Exception
            LogMessage("Erreur client [" & clientIP & "] : " & ex.Message, Color.Red)
        Finally
            client.Close()
            Interlocked.Decrement(currentClientCount)
        End Try
    End Sub

    ' ===============================================
    '           Dispatch des Commandes
    ' ===============================================
    Private Sub ProcessCommand(msg As String, responseStream As NetworkStream)
        If msg.StartsWith("START_APP:", StringComparison.OrdinalIgnoreCase) Then
            LaunchSoftware(msg.Substring(10).Trim())

        ElseIf msg.Equals("COMMAND:TOGGLE_LOCKED_FORM", StringComparison.OrdinalIgnoreCase) Then
            ToggleLockedForm()

        ElseIf msg.StartsWith("NOTIFY_MB:", StringComparison.OrdinalIgnoreCase) Then
            ShowNotificationOnClient(msg.Substring(10).Trim(), True)

        ElseIf msg.StartsWith("NOTIFY_BUBBLE:", StringComparison.OrdinalIgnoreCase) Then
            ShowNotificationOnClient(msg.Substring(14).Trim(), False)

            ' --- NOUVELLES COMMANDES ---
        ElseIf msg.StartsWith("SHUTDOWN:", StringComparison.OrdinalIgnoreCase) Then
            Dim delayStr As String = msg.Substring(9).Trim()
            Dim delay As Integer = 0
            Integer.TryParse(delayStr, delay)
            ExecuteShutdown(delay, False)

        ElseIf msg.StartsWith("RESTART:", StringComparison.OrdinalIgnoreCase) Then
            Dim delayStr As String = msg.Substring(8).Trim()
            Dim delay As Integer = 0
            Integer.TryParse(delayStr, delay)
            ExecuteShutdown(delay, True)

        ElseIf msg.Equals("SHUTDOWN:CANCEL", StringComparison.OrdinalIgnoreCase) Then
            CancelShutdown()

        ElseIf msg.StartsWith("KILL_PROCESS:", StringComparison.OrdinalIgnoreCase) Then
            KillProcessByName(msg.Substring(13).Trim())

        ElseIf msg.Equals("SCREENSHOT", StringComparison.OrdinalIgnoreCase) Then
            SendScreenshot(responseStream)

        ElseIf msg.Equals("MUTE", StringComparison.OrdinalIgnoreCase) Then
            SetMute(True)

        ElseIf msg.Equals("UNMUTE", StringComparison.OrdinalIgnoreCase) Then
            SetMute(False)

        Else
            ShowNotificationOnClient(msg, False)
        End If
    End Sub

    ' ===============================================
    '           Commandes Existantes (réparées)
    ' ===============================================
    Private Sub ToggleLockedForm()
        If Me.InvokeRequired Then
            Me.Invoke(New Action(AddressOf ToggleLockedForm))
        Else
            ' CORRECTION : vérification IsDisposed en plus de Visible
            If lockedFormInstance IsNot Nothing AndAlso Not lockedFormInstance.IsDisposed AndAlso lockedFormInstance.Visible Then
                lockedFormInstance.Close()
                lockedFormInstance.Dispose()
                lockedFormInstance = Nothing
                LogMessage("PC Déverrouillé (LockedForm fermé)", Color.DarkGreen)
            Else
                If lockedFormInstance IsNot Nothing AndAlso Not lockedFormInstance.IsDisposed Then
                    lockedFormInstance.Dispose()
                    lockedFormInstance = Nothing
                End If
                lockedFormInstance = New LockedForm()
                lockedFormInstance.Show()
                LogMessage("PC Verrouillé (LockedForm ouvert)", Color.DarkRed)
            End If
        End If
    End Sub

    Private Sub LaunchSoftware(path As String)
        If Me.InvokeRequired Then
            Me.Invoke(New Action(Of String)(AddressOf LaunchSoftware), path)
        Else
            Try
                If File.Exists(path) Then
                    Process.Start(path)
                    LogMessage("Lancé : " & path)
                Else
                    LogMessage("Fichier introuvable : " & path, Color.Red)
                End If
            Catch ex As Exception
                LogMessage("Erreur lancement : " & ex.Message, Color.Red)
            End Try
        End If
    End Sub

    ' ===============================================
    '           NOUVELLES COMMANDES - Implémentations
    ' ===============================================

    ''' <summary>
    ''' SHUTDOWN:[delai_secondes] ou RESTART:[delai_secondes]
    ''' Exemple : SHUTDOWN:60 → éteint dans 60 secondes
    ''' Exemple : RESTART:0  → redémarre immédiatement
    ''' </summary>
    Private Sub ExecuteShutdown(delaySeconds As Integer, restart As Boolean)
        Try
            Dim flag As String = If(restart, "/r", "/s")
            Dim args As String = flag & " /t " & delaySeconds.ToString()
            Process.Start(New ProcessStartInfo("shutdown.exe", args) With {
                .CreateNoWindow = True,
                .UseShellExecute = False
            })
            Dim action As String = If(restart, "Redémarrage", "Arrêt")
            LogMessage(action & " programmé dans " & delaySeconds & " secondes.", Color.DarkOrange)
            ShowNotificationOnClient(action & " du PC dans " & delaySeconds & " secondes.", False)
        Catch ex As Exception
            LogMessage("Erreur shutdown : " & ex.Message, Color.Red)
        End Try
    End Sub

    ''' <summary>
    ''' Annule un shutdown/restart en cours : SHUTDOWN:CANCEL
    ''' </summary>
    Private Sub CancelShutdown()
        Try
            Process.Start(New ProcessStartInfo("shutdown.exe", "/a") With {
                .CreateNoWindow = True,
                .UseShellExecute = False
            })
            LogMessage("Arrêt/redémarrage annulé.", Color.Green)
            ShowNotificationOnClient("Arrêt programmé annulé.", False)
        Catch ex As Exception
            LogMessage("Erreur annulation shutdown : " & ex.Message, Color.Red)
        End Try
    End Sub

    ''' <summary>
    ''' KILL_PROCESS:nom.exe — Tue tous les processus portant ce nom
    ''' Exemple : KILL_PROCESS:notepad.exe
    ''' </summary>
    Private Sub KillProcessByName(processName As String)
        Try
            ' Supprimer l'extension si fournie pour GetProcessesByName
            Dim nameNoExt As String = Path.GetFileNameWithoutExtension(processName)
            Dim processes() As Process = Process.GetProcessesByName(nameNoExt)

            If processes.Length = 0 Then
                LogMessage("Processus introuvable : " & processName, Color.Orange)
                Return
            End If

            Dim killed As Integer = 0
            For Each p As Process In processes
                Try
                    p.Kill()
                    p.WaitForExit(3000)
                    killed += 1
                Catch killEx As Exception
                    LogMessage("Impossible de tuer " & p.ProcessName & " (PID " & p.Id & ") : " & killEx.Message, Color.Red)
                End Try
            Next

            LogMessage("KILL_PROCESS : " & killed & " instance(s) de '" & processName & "' terminée(s).", Color.DarkOrange)
        Catch ex As Exception
            LogMessage("Erreur KILL_PROCESS : " & ex.Message, Color.Red)
        End Try
    End Sub

    ''' <summary>
    ''' SCREENSHOT — Capture l'écran principal et l'envoie en JPEG via le stream réseau.
    ''' Protocole de réponse : 4 octets (Int32 BigEndian) = taille des données, puis les octets JPEG.
    ''' </summary>
    Private Sub SendScreenshot(responseStream As NetworkStream)
        Try
            ' Capturer tous les écrans
            Dim totalBounds As Rectangle = Rectangle.Empty
            For Each screen As Screen In Screen.AllScreens
                totalBounds = Rectangle.Union(totalBounds, screen.Bounds)
            Next

            Using bmp As New Bitmap(totalBounds.Width, totalBounds.Height, PixelFormat.Format32bppArgb)
                Using g As Graphics = Graphics.FromImage(bmp)
                    g.CopyFromScreen(totalBounds.Location, Point.Empty, totalBounds.Size)
                End Using

                ' Encoder en JPEG qualité 70 (bon compromis taille/qualité)
                Dim jpegEncoder As ImageCodecInfo = Nothing
                For Each codec As ImageCodecInfo In ImageCodecInfo.GetImageDecoders()
                    If codec.FormatID = ImageFormat.Jpeg.Guid Then
                        jpegEncoder = codec
                        Exit For
                    End If
                Next

                Using ms As New MemoryStream()
                    If jpegEncoder IsNot Nothing Then
                        Dim encoderParams As New EncoderParameters(1)
                        encoderParams.Param(0) = New EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 70L)
                        bmp.Save(ms, jpegEncoder, encoderParams)
                    Else
                        bmp.Save(ms, ImageFormat.Jpeg)
                    End If

                    Dim imgBytes() As Byte = ms.ToArray()

                    ' Envoyer la taille (4 octets Big Endian) puis les données
                    Dim sizeBytes() As Byte = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(imgBytes.Length))
                    responseStream.Write(sizeBytes, 0, 4)
                    responseStream.Write(imgBytes, 0, imgBytes.Length)
                    responseStream.Flush()
                End Using
            End Using

            LogMessage("Screenshot envoyé avec succès.", Color.DarkGreen)
        Catch ex As Exception
            LogMessage("Erreur SCREENSHOT : " & ex.Message, Color.Red)
        End Try
    End Sub

    ' ===============================================
    '   LIVE VIEW — Version simple (screenshot en boucle)
    ' ===============================================

    Public Sub StartLiveView(ip As String, intervalSeconds As Double)
        If liveViewRunning Then
            LogMessage("Live View déjà actif.", Color.Orange)
            Return
        End If

        liveViewStopping = False
        liveViewRunning = True
        liveViewCTS = New CancellationTokenSource()
        Dim currentInterval As Double = intervalSeconds

        ' --- Fenêtre de visionnage ---
        Me.Invoke(Sub()
                      liveViewForm = New Form()
                      liveViewForm.Text = "Live View — " & ip
                      liveViewForm.Size = New Size(1280, 800)
                      liveViewForm.StartPosition = FormStartPosition.CenterScreen
                      liveViewForm.BackColor = Color.Black

                      ' Barre de contrôle en bas
                      Dim controlPanel As New Panel()
                      controlPanel.Dock = DockStyle.Bottom
                      controlPanel.Height = 40
                      controlPanel.BackColor = Color.FromArgb(25, 25, 25)

                      ' Statut (gauche)
                      Dim statusLabel As New Label()
                      statusLabel.Name = "statusLabel"
                      statusLabel.AutoSize = False
                      statusLabel.Width = 480
                      statusLabel.Height = 30
                      statusLabel.Location = New Point(6, 5)
                      statusLabel.ForeColor = Color.LimeGreen
                      statusLabel.BackColor = Color.Transparent
                      statusLabel.TextAlign = ContentAlignment.MiddleLeft
                      statusLabel.Text = "  Connexion..."

                      ' Label "Intervalle :"
                      Dim lblInterval As New Label()
                      lblInterval.Text = "Intervalle :"
                      lblInterval.AutoSize = True
                      lblInterval.Location = New Point(494, 12)
                      lblInterval.ForeColor = Color.Silver
                      lblInterval.BackColor = Color.Transparent

                      ' TrackBar — de 1 à 100 (représente 0.1s à 10.0s, pas de 0.1s)
                      ' Valeur initiale = intervalSeconds * 10
                      Dim slider As New TrackBar()
                      slider.Minimum = 1      ' = 0.1s
                      slider.Maximum = 100    ' = 10.0s
                      slider.Value = CInt(Math.Min(100, Math.Max(1, intervalSeconds * 10)))
                      slider.TickFrequency = 10
                      slider.SmallChange = 1
                      slider.LargeChange = 5
                      slider.Width = 180
                      slider.Height = 30
                      slider.Location = New Point(563, 5)
                      slider.BackColor = Color.FromArgb(25, 25, 25)

                      ' Label valeur actuelle "5.0s"
                      Dim lblValue As New Label()
                      lblValue.Name = "lblValue"
                      lblValue.AutoSize = False
                      lblValue.Width = 44
                      lblValue.Height = 30
                      lblValue.Location = New Point(748, 7)
                      lblValue.ForeColor = Color.White
                      lblValue.BackColor = Color.Transparent
                      lblValue.TextAlign = ContentAlignment.MiddleLeft
                      lblValue.Text = intervalSeconds.ToString("0.0") & "s"

                      ' Mise à jour intervalle en temps réel via le slider
                      AddHandler slider.ValueChanged, Sub(s2, ev2)
                                                          Dim newVal As Double = slider.Value / 100.0
                                                          currentInterval = newVal
                                                          lblValue.Text = newVal.ToString("0.0") & "s"
                                                      End Sub

                      ' Bouton Arrêter
                      Dim btnStop As New Button()
                      btnStop.Text = "⏹  Arrêter"
                      btnStop.Width = 88
                      btnStop.Height = 28
                      btnStop.Location = New Point(800, 6)
                      btnStop.BackColor = Color.FromArgb(180, 40, 40)
                      btnStop.ForeColor = Color.White
                      btnStop.FlatStyle = FlatStyle.Flat
                      btnStop.FlatAppearance.BorderSize = 0
                      AddHandler btnStop.Click, Sub(s2, ev2) StopLiveView()

                      controlPanel.Controls.AddRange(New Control() {
                          statusLabel, lblInterval, slider, lblValue, btnStop})

                      liveViewPictureBox = New PictureBox()
                      liveViewPictureBox.Dock = DockStyle.Fill
                      liveViewPictureBox.SizeMode = PictureBoxSizeMode.Zoom
                      liveViewPictureBox.BackColor = Color.Black

                      liveViewForm.Controls.Add(liveViewPictureBox)
                      liveViewForm.Controls.Add(controlPanel)

                      AddHandler liveViewForm.FormClosing, Sub(s2, ev2)
                                                               If Not liveViewStopping Then StopLiveView()
                                                           End Sub

                      liveViewForm.Show()
                  End Sub)

        ' --- Boucle de capture ---
        Task.Factory.StartNew(Sub()
                                  Dim token As CancellationToken = liveViewCTS.Token

                                  While liveViewRunning AndAlso Not token.IsCancellationRequested
                                      Dim t0 As Long = Stopwatch.GetTimestamp()

                                      ' Réutilise exactement le même système que le bouton Screenshot
                                      Dim imgBytes() As Byte = SendMessageAndReceiveBytes(ip, "SCREENSHOT")

                                      If imgBytes IsNot Nothing AndAlso imgBytes.Length > 0 Then
                                          Try
                                              Using ms As New MemoryStream(imgBytes)
                                                  Dim img As Bitmap = CType(Bitmap.FromStream(ms), Bitmap)
                                                  Dim elapsed As Double = (Stopwatch.GetTimestamp() - t0) /
                                                                           Stopwatch.Frequency * 1000

                                                  If liveViewForm IsNot Nothing AndAlso Not liveViewForm.IsDisposed Then
                                                      liveViewForm.Invoke(Sub()
                                                                              If liveViewPictureBox Is Nothing OrElse
                                                                                 liveViewPictureBox.IsDisposed Then Return
                                                                              Dim old As Image = liveViewPictureBox.Image
                                                                              liveViewPictureBox.Image = img
                                                                              If old IsNot Nothing Then old.Dispose()

                                                                              ' Mettre à jour le statut
                                                                              For Each ctrl As Control In liveViewForm.Controls
                                                                                  If TypeOf ctrl Is Panel Then
                                                                                      Dim lbl As Control = ctrl.Controls("statusLabel")
                                                                                      If lbl IsNot Nothing Then
                                                                                          lbl.Text = String.Format(
                                                                                              "  {0} octets  |  {1:F0} ms  |  {2}  |  intervalle : {3}s",
                                                                                              imgBytes.Length, elapsed,
                                                                                              DateTime.Now.ToString("HH:mm:ss"),
                                                                                              currentInterval)
                                                                                      End If
                                                                                      Exit For
                                                                                  End If
                                                                              Next
                                                                          End Sub)
                                                  End If
                                              End Using
                                          Catch ex As Exception
                                              LogMessage("Live View erreur image : " & ex.Message, Color.Red)
                                          End Try
                                      Else
                                          If liveViewRunning Then
                                              LogMessage("Live View : pas de réponse de " & ip, Color.Orange)
                                          End If
                                      End If

                                      ' Attendre l'intervalle en soustrayant le temps déjà écoulé
                                      Dim elapsed2 As Double = (Stopwatch.GetTimestamp() - t0) /
                                                                Stopwatch.Frequency * 1000
                                      Dim waitMs As Integer = CInt(currentInterval * 1000) - CInt(elapsed2)
                                      If waitMs > 0 Then
                                          Try
                                              Task.Delay(waitMs, token).Wait()
                                          Catch : End Try
                                      End If
                                  End While

                                  LogMessage("Live View thread terminé.", Color.Gray)
                              End Sub, TaskCreationOptions.LongRunning)
    End Sub

    Private Sub StopLiveView()
        If liveViewStopping Then Return
        liveViewStopping = True
        liveViewRunning = False
        liveViewCTS?.Cancel()

        Dim f As Form = liveViewForm
        liveViewForm = Nothing
        liveViewPictureBox = Nothing

        If f IsNot Nothing AndAlso Not f.IsDisposed Then
            Try
                If f.InvokeRequired Then
                    f.Invoke(Sub() If Not f.IsDisposed Then f.Close())
                Else
                    f.Close()
                End If
            Catch : End Try
        End If

        LogMessage("Live View arrêté.", Color.Gray)
    End Sub

    ''' <summary>
    ''' MUTE / UNMUTE — Contrôle du volume via WinAPI (sans dépendance externe)
    ''' </summary>
    Private Sub SetMute(mute As Boolean)
        Try
            ' Si l'état demandé est différent de l'état actuel, on toggle
            If mute <> volumeMuted Then
                SendMessage(Me.Handle, WM_APPCOMMAND, Me.Handle, New IntPtr(APPCOMMAND_VOLUME_MUTE))
                volumeMuted = mute
            End If
            LogMessage("Volume : " & If(mute, "MUET", "ACTIF"), Color.Purple)
            ShowNotificationOnClient("Volume " & If(mute, "coupé (MUTE)", "rétabli"), False)
        Catch ex As Exception
            LogMessage("Erreur MUTE/UNMUTE : " & ex.Message, Color.Red)
        End Try
    End Sub

    ' ===============================================
    '           Logique d'Envoi (Client)
    ' ===============================================

    ''' <summary>
    ''' Envoie un message avec authentification par token.
    ''' Format : TOKEN:[token]|[commande]
    ''' </summary>
    Private Sub SendMessageToServer(ip As String, message As String)
        Try
            Using client As New TcpClient()
                client.Connect(ip, PORT)
                Using writer As New StreamWriter(client.GetStream(), Encoding.UTF8)
                    writer.WriteLine("TOKEN:" & AUTH_TOKEN & "|" & message)
                    writer.Flush()
                End Using
            End Using
        Catch ex As Exception
            LogMessage("Erreur d'envoi vers " & ip & " : " & ex.Message, Color.Red)
        End Try
    End Sub

    ''' <summary>
    ''' Envoi avec réception d'une réponse binaire (pour SCREENSHOT).
    ''' </summary>
    Private Function SendMessageAndReceiveBytes(ip As String, message As String) As Byte()
        Try
            Using client As New TcpClient()
                client.Connect(ip, PORT)
                client.ReceiveTimeout = 15000

                Dim stream As NetworkStream = client.GetStream()

                ' Envoyer la commande
                Dim data() As Byte = Encoding.UTF8.GetBytes("TOKEN:" & AUTH_TOKEN & "|" & message & vbLf)
                stream.Write(data, 0, data.Length)
                stream.Flush()

                ' Lire la taille (4 octets)
                Dim sizeBuffer(3) As Byte
                Dim totalRead As Integer = 0
                While totalRead < 4
                    Dim read As Integer = stream.Read(sizeBuffer, totalRead, 4 - totalRead)
                    If read = 0 Then Return Nothing
                    totalRead += read
                End While

                Dim dataSize As Integer = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(sizeBuffer, 0))
                If dataSize <= 0 OrElse dataSize > 10_000_000 Then Return Nothing ' Max 10 Mo

                ' Lire les données image
                Dim imgBuffer(dataSize - 1) As Byte
                totalRead = 0
                While totalRead < dataSize
                    Dim read As Integer = stream.Read(imgBuffer, totalRead, dataSize - totalRead)
                    If read = 0 Then Exit While
                    totalRead += read
                End While

                Return imgBuffer
            End Using
        Catch ex As Exception
            LogMessage("Erreur réception screenshot : " & ex.Message, Color.Red)
            Return Nothing
        End Try
    End Function

    ' ===============================================
    '           Boutons UI
    ' ===============================================
    Private Sub btnLocked_Click(sender As Object, e As EventArgs) Handles btnLocked.Click
        Dim ip As String = txtServerIP.Text.Trim()
        If Not String.IsNullOrWhiteSpace(ip) Then
            Task.Factory.StartNew(Sub() SendMessageToServer(ip, "COMMAND:TOGGLE_LOCKED_FORM"))
        End If
    End Sub

    Private Sub btnSendNotification_Click(sender As Object, e As EventArgs) Handles btnSendNotification.Click
        Dim ip As String = txtServerIP.Text.Trim()
        Dim msg As String = txtMessageToSend.Text.Trim()
        If msg <> "" Then
            Dim prefix As String = If(CheckBox1.Checked, "NOTIFY_MB:", "NOTIFY_BUBBLE:")
            Task.Factory.StartNew(Sub() SendMessageToServer(ip, prefix & msg))
        End If
    End Sub

    Private Sub btnStartRemoteApp_Click(sender As Object, e As EventArgs) Handles btnStartRemoteApp.Click
        SendMessageToServer(txtServerIP.Text.Trim(), "START_APP:" & txtRemoteAppToStart.Text.Trim())
    End Sub

    ' --- Boutons nouvelles commandes ---
    Private Sub btnShutdown_Click(sender As Object, e As EventArgs) Handles btnShutdown.Click
        Dim delay As String = InputBox("Délai avant extinction (secondes) :", "Shutdown", "60")
        If Not String.IsNullOrWhiteSpace(delay) Then
            Task.Factory.StartNew(Sub() SendMessageToServer(txtServerIP.Text.Trim(), "SHUTDOWN:" & delay.Trim()))
        End If
    End Sub

    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles btnRestart.Click
        Dim delay As String = InputBox("Délai avant redémarrage (secondes) :", "Restart", "60")
        If Not String.IsNullOrWhiteSpace(delay) Then
            Task.Factory.StartNew(Sub() SendMessageToServer(txtServerIP.Text.Trim(), "RESTART:" & delay.Trim()))
        End If
    End Sub

    Private Sub btnCancelShutdown_Click(sender As Object, e As EventArgs) Handles btnCancelShutdown.Click
        Task.Factory.StartNew(Sub() SendMessageToServer(txtServerIP.Text.Trim(), "SHUTDOWN:CANCEL"))
    End Sub

    Private Sub btnKillProcess_Click(sender As Object, e As EventArgs) Handles btnKillProcess.Click
        Dim procName As String = InputBox("Nom du processus à tuer :", "Kill Process", "notepad.exe")
        If Not String.IsNullOrWhiteSpace(procName) Then
            Task.Factory.StartNew(Sub() SendMessageToServer(txtServerIP.Text.Trim(), "KILL_PROCESS:" & procName.Trim()))
        End If
    End Sub

    Private Sub btnScreenshot_Click(sender As Object, e As EventArgs) Handles btnScreenshot.Click
        Dim ip As String = txtServerIP.Text.Trim()
        If String.IsNullOrWhiteSpace(ip) Then Return

        Task.Factory.StartNew(Sub()
                                  LogMessage("Demande de screenshot vers " & ip & "...", Color.Gray)
                                  Dim imgBytes() As Byte = SendMessageAndReceiveBytes(ip, "SCREENSHOT")

                                  If imgBytes IsNot Nothing AndAlso imgBytes.Length > 0 Then
                                      Me.Invoke(Sub()
                                                    Try
                                                        Using ms As New MemoryStream(imgBytes)
                                                            Dim img As Image = Image.FromStream(ms)
                                                            ' Afficher dans une nouvelle fenêtre
                                                            Dim previewForm As New Form()
                                                            previewForm.Text = "Screenshot — " & ip
                                                            previewForm.Size = New Size(1024, 600)
                                                            previewForm.StartPosition = FormStartPosition.CenterScreen

                                                            Dim pb As New PictureBox()
                                                            pb.Dock = DockStyle.Fill
                                                            pb.SizeMode = PictureBoxSizeMode.Zoom
                                                            pb.Image = img
                                                            previewForm.Controls.Add(pb)

                                                            ' Bouton sauvegarde
                                                            Dim btnSave As New Button()
                                                            btnSave.Text = "Enregistrer..."
                                                            btnSave.Dock = DockStyle.Bottom
                                                            btnSave.Height = 30
                                                            AddHandler btnSave.Click, Sub(s, ev)
                                                                                          Using sfd As New SaveFileDialog()
                                                                                              sfd.Filter = "Image JPEG|*.jpg"
                                                                                              sfd.FileName = "screenshot_" & ip.Replace(".", "_") & "_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".jpg"
                                                                                              If sfd.ShowDialog() = DialogResult.OK Then
                                                                                                  img.Save(sfd.FileName, ImageFormat.Jpeg)
                                                                                                  LogMessage("Screenshot enregistré : " & sfd.FileName, Color.Green)
                                                                                              End If
                                                                                          End Using
                                                                                      End Sub
                                                            previewForm.Controls.Add(btnSave)

                                                            previewForm.Show()
                                                            LogMessage("Screenshot reçu de " & ip & " (" & imgBytes.Length & " octets)", Color.DarkGreen)
                                                        End Using
                                                    Catch ex As Exception
                                                        LogMessage("Erreur affichage screenshot : " & ex.Message, Color.Red)
                                                    End Try
                                                End Sub)
                                  Else
                                      LogMessage("Aucune donnée reçue pour le screenshot.", Color.Orange)
                                  End If
                              End Sub)
    End Sub

    Private Sub btnMute_Click(sender As Object, e As EventArgs) Handles btnMute.Click
        Task.Factory.StartNew(Sub() SendMessageToServer(txtServerIP.Text.Trim(), "MUTE"))
    End Sub

    Private Sub btnUnmute_Click(sender As Object, e As EventArgs) Handles btnUnMute.Click
        Task.Factory.StartNew(Sub() SendMessageToServer(txtServerIP.Text.Trim(), "UNMUTE"))
    End Sub

    Private Sub btnLiveView_Click(sender As Object, e As EventArgs) Handles btnLiveView.Click
        Dim ip As String = txtServerIP.Text.Trim()
        If String.IsNullOrWhiteSpace(ip) Then
            MessageBox.Show("Entrez une adresse IP.", "Live View", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If liveViewRunning Then
            StopLiveView()
            Return
        End If

        ' Intervalle par défaut : 5 secondes (slider entre 0.1s et 10.0s)
        StartLiveView(ip, 0.5)
    End Sub

    ' --- Raccourcis applications ---
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        txtRemoteAppToStart.Text = "C:\windows\system32\cmd.exe"
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        txtRemoteAppToStart.Text = "C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe"
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        txtRemoteAppToStart.Text = "C:\windows\system32\notepad.exe"
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        txtRemoteAppToStart.Text = "C:\Program Files\Google\Chrome\Application\chrome.exe"
    End Sub
    Private Sub Btn_Firefox_Click(sender As Object, e As EventArgs) Handles Btn_Firefox.Click
        txtRemoteAppToStart.Text = "C:\Program Files\Mozilla Firefox\firefox.exe"
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Scanneur_d_ip.Show()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MAJs.Show()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    ' ===============================================
    '           Utilitaires UI
    ' ===============================================
    Private Sub LogMessage(message As String, Optional color As Color = Nothing)
        If rtbLog.InvokeRequired Then
            rtbLog.Invoke(New Action(Of String, Color)(AddressOf LogMessage), message, color)
        Else
            rtbLog.SelectionStart = rtbLog.TextLength
            rtbLog.SelectionColor = If(color = Nothing OrElse color = Color.Empty, Color.Black, color)
            rtbLog.AppendText(DateTime.Now.ToString("HH:mm:ss") & " - " & message & Environment.NewLine)
            rtbLog.ScrollToCaret()
        End If
    End Sub

    Private Sub ShowNotificationOnClient(text As String, useMessageBox As Boolean)
        If Me.InvokeRequired Then
            Me.Invoke(New Action(Of String, Boolean)(AddressOf ShowNotificationOnClient), text, useMessageBox)
        Else
            If useMessageBox Then
                MessageBox.Show(text, TextBoxTitle.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                NotifyIcon1.ShowBalloonTip(5000, TextBoxTitle.Text, text, ToolTipIcon.Info)
            End If
        End If
    End Sub

    Private Sub SetupNotifyIconContextMenu()
        Dim contextMenu As New ContextMenuStrip()
        contextMenu.Items.Add("Afficher/Masquer", Nothing, AddressOf ShowHideForm)
        contextMenu.Items.Add("Quitter", Nothing, AddressOf ExitApplication)
        NotifyIcon1.ContextMenuStrip = contextMenu
    End Sub

    Private Sub ShowHideForm(sender As Object, e As EventArgs)
        If Me.Visible Then Me.Hide() Else Me.Show()
    End Sub

    Private Sub ExitApplication(sender As Object, e As EventArgs)
        CleanupServer()
        Application.Exit()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        CleanupServer()
    End Sub

    Private Sub CleanupServer()
        serverRunning = False
        cancellationTokenSource?.Cancel()
        serverListener?.Stop()
        NotifyIcon1.Visible = False
        If lockedFormInstance IsNot Nothing AndAlso Not lockedFormInstance.IsDisposed Then
            lockedFormInstance.Close()
        End If
        ' Arrêt Live View
        StopLiveView()
    End Sub

End Class