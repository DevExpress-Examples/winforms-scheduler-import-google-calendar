Imports Microsoft.VisualBasic
Imports System
Namespace GoogleCalendarExample
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim timeRuler1 As New DevExpress.XtraScheduler.TimeRuler()
			Dim timeRuler2 As New DevExpress.XtraScheduler.TimeRuler()
			Dim timeRuler3 As New DevExpress.XtraScheduler.TimeRuler()
			Me.btnConnect = New System.Windows.Forms.Button()
			Me.tbLog = New System.Windows.Forms.TextBox()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.panel2 = New System.Windows.Forms.Panel()
			Me.cbCalendars = New System.Windows.Forms.ComboBox()
			Me.panel3 = New System.Windows.Forms.Panel()
			Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
			Me.tsStatus = New System.Windows.Forms.ToolStripStatusLabel()
			Me.schedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
			Me.schedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
			Me.btnUpdate = New System.Windows.Forms.Button()
			Me.panel1.SuspendLayout()
			Me.panel2.SuspendLayout()
			Me.panel3.SuspendLayout()
			Me.statusStrip1.SuspendLayout()
			CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' btnConnect
			' 
			Me.btnConnect.Location = New System.Drawing.Point(12, 12)
			Me.btnConnect.Name = "btnConnect"
			Me.btnConnect.Size = New System.Drawing.Size(75, 23)
			Me.btnConnect.TabIndex = 0
			Me.btnConnect.Text = "Connect"
			Me.btnConnect.UseVisualStyleBackColor = True
'			Me.btnConnect.Click += New System.EventHandler(Me.OnBtnConnectClick);
			' 
			' tbLog
			' 
			Me.tbLog.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.tbLog.Location = New System.Drawing.Point(0, 286)
			Me.tbLog.Multiline = True
			Me.tbLog.Name = "tbLog"
			Me.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
			Me.tbLog.Size = New System.Drawing.Size(857, 112)
			Me.tbLog.TabIndex = 1
			' 
			' panel1
			' 
			Me.panel1.Controls.Add(Me.panel3)
			Me.panel1.Controls.Add(Me.tbLog)
			Me.panel1.Controls.Add(Me.statusStrip1)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel1.Location = New System.Drawing.Point(0, 48)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(857, 420)
			Me.panel1.TabIndex = 2
			' 
			' panel2
			' 
			Me.panel2.Controls.Add(Me.cbCalendars)
			Me.panel2.Controls.Add(Me.btnUpdate)
			Me.panel2.Controls.Add(Me.btnConnect)
			Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
			Me.panel2.Location = New System.Drawing.Point(0, 0)
			Me.panel2.Name = "panel2"
			Me.panel2.Size = New System.Drawing.Size(857, 48)
			Me.panel2.TabIndex = 3
			' 
			' cbCalendars
			' 
			Me.cbCalendars.FormattingEnabled = True
			Me.cbCalendars.Location = New System.Drawing.Point(93, 14)
			Me.cbCalendars.Name = "cbCalendars"
			Me.cbCalendars.Size = New System.Drawing.Size(670, 21)
			Me.cbCalendars.TabIndex = 1
			' 
			' panel3
			' 
			Me.panel3.Controls.Add(Me.schedulerControl1)
			Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel3.Location = New System.Drawing.Point(0, 0)
			Me.panel3.Name = "panel3"
			Me.panel3.Size = New System.Drawing.Size(857, 286)
			Me.panel3.TabIndex = 2
			' 
			' statusStrip1
			' 
			Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.tsStatus})
			Me.statusStrip1.Location = New System.Drawing.Point(0, 398)
			Me.statusStrip1.Name = "statusStrip1"
			Me.statusStrip1.Size = New System.Drawing.Size(857, 22)
			Me.statusStrip1.TabIndex = 3
			Me.statusStrip1.Text = "statusStrip1"
			' 
			' tsStatus
			' 
			Me.tsStatus.Name = "tsStatus"
			Me.tsStatus.Size = New System.Drawing.Size(0, 17)
			' 
			' schedulerControl1
			' 
			Me.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.schedulerControl1.Location = New System.Drawing.Point(0, 0)
			Me.schedulerControl1.Name = "schedulerControl1"
			Me.schedulerControl1.Size = New System.Drawing.Size(857, 286)
			Me.schedulerControl1.Start = New System.DateTime(2015, 7, 14, 0, 0, 0, 0)
			Me.schedulerControl1.Storage = Me.schedulerStorage1
			Me.schedulerControl1.TabIndex = 0
			Me.schedulerControl1.Text = "schedulerControl1"
			Me.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1)
			Me.schedulerControl1.Views.FullWeekView.Enabled = True
			Me.schedulerControl1.Views.FullWeekView.TimeRulers.Add(timeRuler2)
			Me.schedulerControl1.Views.WeekView.Enabled = False
			Me.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler3)
			' 
			' btnUpdate
			' 
			Me.btnUpdate.Location = New System.Drawing.Point(770, 12)
			Me.btnUpdate.Name = "btnUpdate"
			Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
			Me.btnUpdate.TabIndex = 0
			Me.btnUpdate.Text = "Update"
			Me.btnUpdate.UseVisualStyleBackColor = True
'			Me.btnUpdate.Click += New System.EventHandler(Me.OnBtnConnectClick);
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(857, 468)
			Me.Controls.Add(Me.panel1)
			Me.Controls.Add(Me.panel2)
			Me.Name = "Form1"
			Me.Text = "Google Calendar Importer"
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.panel2.ResumeLayout(False)
			Me.panel3.ResumeLayout(False)
			Me.statusStrip1.ResumeLayout(False)
			Me.statusStrip1.PerformLayout()
			CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents btnConnect As System.Windows.Forms.Button
		Private tbLog As System.Windows.Forms.TextBox
		Private panel1 As System.Windows.Forms.Panel
		Private panel2 As System.Windows.Forms.Panel
		Private cbCalendars As System.Windows.Forms.ComboBox
		Private panel3 As System.Windows.Forms.Panel
		Private statusStrip1 As System.Windows.Forms.StatusStrip
		Private tsStatus As System.Windows.Forms.ToolStripStatusLabel
		Private schedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
		Private schedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
		Private WithEvents btnUpdate As System.Windows.Forms.Button
	End Class
End Namespace

