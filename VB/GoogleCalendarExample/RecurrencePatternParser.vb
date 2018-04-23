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
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.iCalendar.Components
Imports DevExpress.XtraScheduler.iCalendar
Imports DevExpress.XtraScheduler.iCalendar.Native

Namespace GoogleCalendarExample
	Public Class RecurrencePatternParser
		Private ReadOnly storage_Renamed As SchedulerStorage
		Private rule As VRecurrenceRule = Nothing

		Public Sub New(ByVal storage As SchedulerStorage)
			Me.storage_Renamed = storage
			Me.rule = Nothing
		End Sub

		Public ReadOnly Property Storage() As SchedulerStorage
			Get
				Return storage_Renamed
			End Get
		End Property

		Public Function Parse(ByVal stringValue As IList(Of String), ByVal start As DateTime, ByVal [end] As DateTime) As Appointment
			Dim parser As New iCalendarEntryParser()
			Dim entryContainer As iCalendarEntryContainer = parser.ParseString(String.Join(Constants.vbLf, stringValue.ToArray()))
			Me.rule = entryContainer.GetPropertyValue(Of VRecurrenceRule)(RecurrenceRuleProperty.TokenName)
			If Me.rule Is Nothing Then
				Return Nothing
			End If

			Dim pattern As Appointment = Storage.CreateAppointment(AppointmentType.Pattern)
			pattern.Start = start
			pattern.End = [end]
			iCalendarHelper.ApplyRecurrenceInfo(pattern.RecurrenceInfo, start, rule)
			Dim exceptionProperty As ExceptionDateTimesProperty = TryCast(entryContainer.GetProperty(ExceptionDateTimesProperty.TokenName), ExceptionDateTimesProperty)
			If exceptionProperty IsNot Nothing Then
				'exceptionProperty.ApplyTimeZone(new LazyTimeZoneManager());
				Dim calculator As OccurrenceCalculator = OccurrenceCalculator.CreateInstance(pattern.RecurrenceInfo)
				For Each item In exceptionProperty.Values
					Dim indx As Integer = calculator.FindOccurrenceIndex(item, pattern)
					If indx < 0 Then
						Continue For
					End If
					pattern.CreateException(AppointmentType.DeletedOccurrence, indx)
				Next item
			End If
			Return pattern
		End Function
	End Class
End Namespace
