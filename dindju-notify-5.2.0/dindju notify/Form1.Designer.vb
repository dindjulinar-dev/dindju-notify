<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnSendNotification = New System.Windows.Forms.Button()
        Me.txtMessageToSend = New System.Windows.Forms.TextBox()
        Me.txtServerIP = New System.Windows.Forms.TextBox()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxTitle = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rtbLog = New System.Windows.Forms.RichTextBox()
        Me.btnStartRemoteApp = New System.Windows.Forms.Button()
        Me.txtRemoteAppToStart = New System.Windows.Forms.TextBox()
        Me.btnLocked = New System.Windows.Forms.Button()
        Me.lblServerStatus = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Btn_Firefox = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnUnMute = New System.Windows.Forms.Button()
        Me.btnMute = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.btnScreenshot = New System.Windows.Forms.Button()
        Me.btnCancelShutdown = New System.Windows.Forms.Button()
        Me.btnRestart = New System.Windows.Forms.Button()
        Me.btnShutdown = New System.Windows.Forms.Button()
        Me.btnKillProcess = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.btnTerminal = New System.Windows.Forms.Button()
        Me.btnLiveView = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSendNotification
        '
        Me.btnSendNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSendNotification.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSendNotification.Location = New System.Drawing.Point(11, 138)
        Me.btnSendNotification.Name = "btnSendNotification"
        Me.btnSendNotification.Size = New System.Drawing.Size(81, 28)
        Me.btnSendNotification.TabIndex = 1
        Me.btnSendNotification.Text = "envoyé"
        Me.btnSendNotification.UseVisualStyleBackColor = True
        '
        'txtMessageToSend
        '
        Me.txtMessageToSend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMessageToSend.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtMessageToSend.Font = New System.Drawing.Font("Comic Sans MS", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMessageToSend.Location = New System.Drawing.Point(9, 77)
        Me.txtMessageToSend.Name = "txtMessageToSend"
        Me.txtMessageToSend.Size = New System.Drawing.Size(228, 28)
        Me.txtMessageToSend.TabIndex = 2
        '
        'txtServerIP
        '
        Me.txtServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServerIP.Cursor = System.Windows.Forms.Cursors.Default
        Me.txtServerIP.Font = New System.Drawing.Font("Comic Sans MS", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServerIP.Location = New System.Drawing.Point(309, 30)
        Me.txtServerIP.Name = "txtServerIP"
        Me.txtServerIP.Size = New System.Drawing.Size(224, 28)
        Me.txtServerIP.TabIndex = 3
        Me.txtServerIP.Text = "127.0.0.1"
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(9, 112)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(114, 22)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Mode fenaitré"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 18)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Logs :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 18)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Message notif :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(306, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 18)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Ip PC distante"
        '
        'TextBoxTitle
        '
        Me.TextBoxTitle.Font = New System.Drawing.Font("Comic Sans MS", 11.25!)
        Me.TextBoxTitle.Location = New System.Drawing.Point(9, 28)
        Me.TextBoxTitle.Name = "TextBoxTitle"
        Me.TextBoxTitle.Size = New System.Drawing.Size(228, 28)
        Me.TextBoxTitle.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 18)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Titre notif :"
        '
        'rtbLog
        '
        Me.rtbLog.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.rtbLog.Location = New System.Drawing.Point(3, 105)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.Size = New System.Drawing.Size(224, 217)
        Me.rtbLog.TabIndex = 12
        Me.rtbLog.Text = ""
        '
        'btnStartRemoteApp
        '
        Me.btnStartRemoteApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStartRemoteApp.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnStartRemoteApp.Location = New System.Drawing.Point(4, 52)
        Me.btnStartRemoteApp.Name = "btnStartRemoteApp"
        Me.btnStartRemoteApp.Size = New System.Drawing.Size(291, 36)
        Me.btnStartRemoteApp.TabIndex = 13
        Me.btnStartRemoteApp.Text = "Ouvrir l'app"
        Me.btnStartRemoteApp.UseVisualStyleBackColor = True
        '
        'txtRemoteAppToStart
        '
        Me.txtRemoteAppToStart.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemoteAppToStart.Location = New System.Drawing.Point(4, 24)
        Me.txtRemoteAppToStart.Name = "txtRemoteAppToStart"
        Me.txtRemoteAppToStart.Size = New System.Drawing.Size(291, 24)
        Me.txtRemoteAppToStart.TabIndex = 14
        '
        'btnLocked
        '
        Me.btnLocked.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLocked.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnLocked.Location = New System.Drawing.Point(6, 23)
        Me.btnLocked.Name = "btnLocked"
        Me.btnLocked.Size = New System.Drawing.Size(290, 36)
        Me.btnLocked.TabIndex = 15
        Me.btnLocked.Text = "Bloquer le pc distant"
        Me.btnLocked.UseVisualStyleBackColor = True
        '
        'lblServerStatus
        '
        Me.lblServerStatus.AutoSize = True
        Me.lblServerStatus.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.lblServerStatus.Location = New System.Drawing.Point(0, 325)
        Me.lblServerStatus.Name = "lblServerStatus"
        Me.lblServerStatus.Size = New System.Drawing.Size(49, 18)
        Me.lblServerStatus.TabIndex = 16
        Me.lblServerStatus.Text = "Label5"
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Button2.Location = New System.Drawing.Point(3, 388)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(224, 36)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "Verifier les mises à jours"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Button6.Location = New System.Drawing.Point(300, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(93, 36)
        Me.Button6.TabIndex = 18
        Me.Button6.Text = "Chrome"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Button3.Location = New System.Drawing.Point(3, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(93, 36)
        Me.Button3.TabIndex = 19
        Me.Button3.Text = "Cmd"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Button4.Location = New System.Drawing.Point(102, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(93, 36)
        Me.Button4.TabIndex = 20
        Me.Button4.Text = "Powershell"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Button5.Location = New System.Drawing.Point(201, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(93, 36)
        Me.Button5.TabIndex = 21
        Me.Button5.Text = "Bloc notes"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.Btn_Firefox)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Controls.Add(Me.Button6)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Location = New System.Drawing.Point(1, 115)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(294, 61)
        Me.Panel1.TabIndex = 22
        '
        'Btn_Firefox
        '
        Me.Btn_Firefox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Firefox.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Btn_Firefox.Location = New System.Drawing.Point(399, 3)
        Me.Btn_Firefox.Name = "Btn_Firefox"
        Me.Btn_Firefox.Size = New System.Drawing.Size(93, 36)
        Me.Btn_Firefox.TabIndex = 22
        Me.Btn_Firefox.Text = "Firefox "
        Me.Btn_Firefox.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(156, 18)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Chemin de l'application :"
        '
        'btnUnMute
        '
        Me.btnUnMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUnMute.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnUnMute.Location = New System.Drawing.Point(8, 293)
        Me.btnUnMute.Name = "btnUnMute"
        Me.btnUnMute.Size = New System.Drawing.Size(288, 36)
        Me.btnUnMute.TabIndex = 24
        Me.btnUnMute.Text = "Dé Mute"
        Me.btnUnMute.UseVisualStyleBackColor = True
        '
        'btnMute
        '
        Me.btnMute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMute.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnMute.Location = New System.Drawing.Point(8, 251)
        Me.btnMute.Name = "btnMute"
        Me.btnMute.Size = New System.Drawing.Size(288, 36)
        Me.btnMute.TabIndex = 13
        Me.btnMute.Text = "Mute"
        Me.btnMute.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(1, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(213, 18)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Préconfigurations d'applications :"
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Button1.Location = New System.Drawing.Point(3, 346)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(224, 36)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = "Scanneur d'IP"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.Button7.Location = New System.Drawing.Point(3, 430)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(224, 36)
        Me.Button7.TabIndex = 27
        Me.Button7.Text = "Quitter X"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'btnScreenshot
        '
        Me.btnScreenshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnScreenshot.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnScreenshot.Location = New System.Drawing.Point(8, 10)
        Me.btnScreenshot.Name = "btnScreenshot"
        Me.btnScreenshot.Size = New System.Drawing.Size(288, 35)
        Me.btnScreenshot.TabIndex = 28
        Me.btnScreenshot.Text = "Capture d'ecran du pc distant"
        Me.btnScreenshot.UseVisualStyleBackColor = True
        '
        'btnCancelShutdown
        '
        Me.btnCancelShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelShutdown.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnCancelShutdown.Location = New System.Drawing.Point(8, 139)
        Me.btnCancelShutdown.Name = "btnCancelShutdown"
        Me.btnCancelShutdown.Size = New System.Drawing.Size(288, 35)
        Me.btnCancelShutdown.TabIndex = 32
        Me.btnCancelShutdown.Text = "Annuler Arreter"
        Me.btnCancelShutdown.UseVisualStyleBackColor = True
        '
        'btnRestart
        '
        Me.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestart.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnRestart.Location = New System.Drawing.Point(8, 180)
        Me.btnRestart.Name = "btnRestart"
        Me.btnRestart.Size = New System.Drawing.Size(288, 35)
        Me.btnRestart.TabIndex = 31
        Me.btnRestart.Text = "Redémarrer le pc distant"
        Me.btnRestart.UseVisualStyleBackColor = True
        '
        'btnShutdown
        '
        Me.btnShutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShutdown.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnShutdown.Location = New System.Drawing.Point(8, 97)
        Me.btnShutdown.Name = "btnShutdown"
        Me.btnShutdown.Size = New System.Drawing.Size(288, 35)
        Me.btnShutdown.TabIndex = 30
        Me.btnShutdown.Text = "Arreter le pc distant"
        Me.btnShutdown.UseVisualStyleBackColor = True
        '
        'btnKillProcess
        '
        Me.btnKillProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnKillProcess.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnKillProcess.Location = New System.Drawing.Point(4, 183)
        Me.btnKillProcess.Name = "btnKillProcess"
        Me.btnKillProcess.Size = New System.Drawing.Size(292, 35)
        Me.btnKillProcess.TabIndex = 29
        Me.btnKillProcess.Text = "Terminer le processe"
        Me.btnKillProcess.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(85, 79)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 31
        Me.PictureBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(90, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(159, 37)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "Barrien Notify"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Franklin Gothic Medium Cond", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(213, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(43, 17)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "V 5.0.2"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(233, 84)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(310, 389)
        Me.TabControl1.TabIndex = 34
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.btnSendNotification)
        Me.TabPage1.Controls.Add(Me.TextBoxTitle)
        Me.TabPage1.Controls.Add(Me.txtMessageToSend)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.CheckBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(302, 363)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Notification"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.txtRemoteAppToStart)
        Me.TabPage2.Controls.Add(Me.btnKillProcess)
        Me.TabPage2.Controls.Add(Me.btnStartRemoteApp)
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(302, 363)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Applications"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Label11)
        Me.TabPage3.Controls.Add(Me.Label10)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Controls.Add(Me.btnUnMute)
        Me.TabPage3.Controls.Add(Me.btnCancelShutdown)
        Me.TabPage3.Controls.Add(Me.btnLocked)
        Me.TabPage3.Controls.Add(Me.btnMute)
        Me.TabPage3.Controls.Add(Me.btnRestart)
        Me.TabPage3.Controls.Add(Me.btnShutdown)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(302, 363)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Utilitaire"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(3, 230)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(146, 18)
        Me.Label11.TabIndex = 37
        Me.Label11.Text = "Arreter / redémarrer :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 76)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(138, 18)
        Me.Label10.TabIndex = 36
        Me.Label10.Text = "Arreter / redémarrer"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Comic Sans MS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(3, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(141, 18)
        Me.Label9.TabIndex = 35
        Me.Label9.Text = "Bloquer le pc distant :"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btnTerminal)
        Me.TabPage4.Controls.Add(Me.btnLiveView)
        Me.TabPage4.Controls.Add(Me.btnScreenshot)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(302, 363)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "PC Distant"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'btnTerminal
        '
        Me.btnTerminal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTerminal.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnTerminal.Location = New System.Drawing.Point(8, 92)
        Me.btnTerminal.Name = "btnTerminal"
        Me.btnTerminal.Size = New System.Drawing.Size(288, 35)
        Me.btnTerminal.TabIndex = 30
        Me.btnTerminal.Text = "Live en temps réeel d'ecran du pc distant"
        Me.btnTerminal.UseVisualStyleBackColor = True
        '
        'btnLiveView
        '
        Me.btnLiveView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLiveView.Font = New System.Drawing.Font("Comic Sans MS", 9.75!)
        Me.btnLiveView.Location = New System.Drawing.Point(8, 51)
        Me.btnLiveView.Name = "btnLiveView"
        Me.btnLiveView.Size = New System.Drawing.Size(288, 35)
        Me.btnLiveView.TabIndex = 29
        Me.btnLiveView.Text = "Live en temps réeel d'ecran du pc distant"
        Me.btnLiveView.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 472)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.lblServerStatus)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.rtbLog)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtServerIP)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " BARRIEN notify 5.0.2"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSendNotification As Button
    Friend WithEvents txtMessageToSend As TextBox
    Friend WithEvents txtServerIP As TextBox
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxTitle As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents rtbLog As RichTextBox
    Friend WithEvents btnStartRemoteApp As Button
    Friend WithEvents txtRemoteAppToStart As TextBox
    Friend WithEvents btnLocked As Button
    Friend WithEvents lblServerStatus As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents btnUnMute As Button
    Friend WithEvents btnMute As Button
    Friend WithEvents btnScreenshot As Button
    Friend WithEvents btnCancelShutdown As Button
    Friend WithEvents btnRestart As Button
    Friend WithEvents btnShutdown As Button
    Friend WithEvents btnKillProcess As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents btnLiveView As Button
    Friend WithEvents Btn_Firefox As Button
    Friend WithEvents btnTerminal As Button
End Class
