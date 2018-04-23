Imports Microsoft.VisualBasic
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Calendar.v3
Imports Google.Apis.Calendar.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace GoogleCalendarExample
	Partial Public Class Form1
		Inherits Form
		Private Const DefaultCaption As String = "Google Calendar Importer"
		Private Shared Scopes() As String = { CalendarService.Scope.CalendarReadonly }
		Private Shared ApplicationName As String = "GoogleCalendarExample"

		Public Sub New()
			InitializeComponent()
			UpdateFormState()
		End Sub

		Private privateCalendarService As CalendarService
		Private Property CalendarService() As CalendarService
			Get
				Return privateCalendarService
			End Get
			Set(ByVal value As CalendarService)
				privateCalendarService = value
			End Set
		End Property

		Private Sub OnBtnConnectClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click, btnUpdate.Click
			Dim credential As UserCredential
			Using stream = New FileStream("secret\client_secret.json", FileMode.Open, FileAccess.Read)
				Dim credPath As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)
				credPath = Path.Combine(credPath, ".credentials")

				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, New FileDataStore(credPath, True)).Result
				Log("Credential file saved to: " & credPath)
			End Using

			' Create Google Calendar API service.
			CalendarService = New CalendarService(New BaseClientService.Initializer() With {.HttpClientInitializer = credential, .ApplicationName = ApplicationName})
			Dim calendarListRequtes = CalendarService.CalendarList.List()
			Dim calendarList As CalendarList = calendarListRequtes.Execute()
			For Each item In calendarList.Items
				Log(item.Summary)
			Next item
			cbCalendars.DisplayMember = "Summary"
			cbCalendars.DataSource = calendarList.Items

			AddHandler cbCalendars.SelectedValueChanged, AddressOf OnCbCalendarsSelectedValueChanged
			UpdateFormState()
		End Sub
		Private Sub OnCbCalendarsSelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim calendarEntry As CalendarListEntry = TryCast(Me.cbCalendars.SelectedValue, CalendarListEntry)
			Dim calendar As Calendar = CalendarService.Calendars.Get(calendarEntry.Id).Execute()
			Dim events As Events = CalendarService.Events.List(calendarEntry.Id).Execute()
			Log("Loaded {0} events", events.Items.Count)
			Me.schedulerStorage1.Appointments.Items.Clear()
			Me.schedulerStorage1.BeginUpdate()
			Try
				Dim importer As New CalendarImporter(Me.schedulerStorage1)
				importer.Import(events.Items)
			Finally
				Me.schedulerStorage1.EndUpdate()
			End Try
			SetStatus(String.Format("Loaded {0} events", events.Items.Count))
			UpdateFormState()
		End Sub

		Private Sub UpdateFormState()
			If CalendarService Is Nothing Then
				Me.cbCalendars.Enabled = False
				Me.btnUpdate.Enabled = False
				Me.btnConnect.Enabled = True
			Else
				Me.cbCalendars.Enabled = True
				Me.btnUpdate.Enabled = True
				Me.btnConnect.Enabled = False
			End If
		End Sub

		Private Sub Log(ByVal message As String)
			Me.tbLog.Text += message & Constants.vbCrLf
		End Sub
		Private Sub Log(ByVal format As String, ParamArray ByVal args() As Object)
			Log(String.Format(format, args))
		End Sub
		Private Sub SetStatus(ByVal message As String)
			Me.tsStatus.Text = message
		End Sub
	End Class
End Namespace
