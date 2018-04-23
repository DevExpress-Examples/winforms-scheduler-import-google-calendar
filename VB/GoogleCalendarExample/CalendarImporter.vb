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

Namespace GoogleCalendarExample
	Friend Class CalendarImporter
		Private storage_Renamed As SchedulerStorage
		Public Sub New(ByVal storage As SchedulerStorage)
			Me.storage_Renamed = storage
		End Sub

		Public ReadOnly Property Storage() As SchedulerStorage
			Get
				Return storage_Renamed
			End Get
		End Property

        Public Sub Import(ByVal events As IList(Of Google.Apis.Calendar.v3.Data.Event))
            Dim patternHash As New Dictionary(Of String, Appointment)()
            Dim occurrenceHash As New Dictionary(Of String, Google.Apis.Calendar.v3.Data.Event)()

            Storage.Appointments.BeginUpdate()
            Dim parser As New RecurrencePatternParser(storage_Renamed)
            For Each item As [Event] In events
                Dim appointment As Appointment = Nothing
                If item.RecurringEventId IsNot Nothing Then 'occurrence?
                    occurrenceHash.Add(item.Id, item) 'recurrence
                ElseIf item.Recurrence IsNot Nothing Then
                    appointment = parser.Parse(item.Recurrence, ConvertDateTime(item.Start), ConvertDateTime(item.End)) 'parse and create pattern
                    patternHash.Add(item.Id, appointment) 'normal appointment
                Else
                    appointment = storage_Renamed.CreateAppointment(AppointmentType.Normal)
                    AssingTimeIntervalPropertiesTo(appointment, item)
                End If
                If appointment Is Nothing Then
                    Continue For
                End If
                AssignCommonPropertiesTo(appointment, item)
                Storage.Appointments.Add(appointment)
            Next item
            LinkOccurrencesWithPatterns(occurrenceHash, patternHash)
            Storage.Appointments.EndUpdate()
        End Sub
        Private Sub LinkOccurrencesWithPatterns(ByVal occurrenceHash As Dictionary(Of String, Google.Apis.Calendar.v3.Data.Event), ByVal patternHash As Dictionary(Of String, Appointment))
            For Each occurrenceEntry As KeyValuePair(Of String, Google.Apis.Calendar.v3.Data.Event) In occurrenceHash
                Dim occurrenceEvent As [Event] = occurrenceEntry.Value
                Dim patternId As String = occurrenceEntry.Value.RecurringEventId
                If occurrenceEvent IsNot Nothing AndAlso patternHash.ContainsKey(patternId) Then
                    Dim pattern As Appointment = patternHash(patternId)
                    Dim exceptionType As AppointmentType = If((occurrenceEvent.Sequence Is Nothing), AppointmentType.DeletedOccurrence, AppointmentType.ChangedOccurrence)
                    If exceptionType.Equals(AppointmentType.DeletedOccurrence) Then
                        CreateDeletedOccurrence(pattern, occurrenceEvent)
                    Else
                        CreateChangedOccurrence(pattern, occurrenceEvent)
                    End If
                End If
            Next occurrenceEntry
        End Sub
		Private Sub CreateChangedOccurrence(ByVal pattern As Appointment, ByVal occurrenceEvent As [Event])
			Dim indx As Integer = CalculateOccurrenceIndex(pattern, occurrenceEvent.OriginalStartTime)
			Dim newOccurrence As Appointment = pattern.CreateException(AppointmentType.ChangedOccurrence, indx)
			newOccurrence.Start = ConvertDateTime(occurrenceEvent.Start)
			newOccurrence.End = ConvertDateTime(occurrenceEvent.End)
			AssignCommonPropertiesTo(newOccurrence, occurrenceEvent)
		End Sub
		Private Sub CreateDeletedOccurrence(ByVal pattern As Appointment, ByVal occurrenceEvent As [Event])
			Dim indx As Integer = CalculateOccurrenceIndex(pattern, occurrenceEvent.OriginalStartTime)
			Dim exception As Appointment = pattern.CreateException(AppointmentType.DeletedOccurrence, indx)
		End Sub

		Private Function CalculateOccurrenceIndex(ByVal pattern As Appointment, ByVal originalStartTime As EventDateTime) As Integer
			Dim calculator As OccurrenceCalculator = OccurrenceCalculator.CreateInstance(pattern.RecurrenceInfo)
			Return calculator.FindOccurrenceIndex(ConvertDateTime(originalStartTime), pattern)
		End Function
		Private Sub AssignCommonPropertiesTo(ByVal target As Appointment, ByVal source As [Event])
			target.Subject = source.Summary
			target.Description = source.Description
		End Sub
		Private Sub AssingTimeIntervalPropertiesTo(ByVal target As Appointment, ByVal source As [Event])
			If (Not source.Start.DateTime.HasValue) Then
				target.AllDay = True
			End If
			target.Start = ConvertDateTime(source.Start)
			target.End = ConvertDateTime(source.End)
		End Sub
		Private Function ConvertDateTime(ByVal start As EventDateTime) As DateTime
			If start.DateTime.HasValue Then
				Return start.DateTime.Value
			End If
			Return DateTime.Parse(start.Date)
		End Function
	End Class
End Namespace
