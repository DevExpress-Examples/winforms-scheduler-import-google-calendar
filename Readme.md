<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128635391/15.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3218)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CalendarImporter.cs](./CS/GoogleCalendarExample/CalendarImporter.cs) (VB: [CalendarImporter.vb](./VB/GoogleCalendarExample/CalendarImporter.vb))
* [Form1.cs](./CS/GoogleCalendarExample/Form1.cs) (VB: [Form1.vb](./VB/GoogleCalendarExample/Form1.vb))
* [RecurrencePatternParser.cs](./CS/GoogleCalendarExample/RecurrencePatternParser.cs) (VB: [RecurrencePatternParser.vb](./VB/GoogleCalendarExample/RecurrencePatternParser.vb))
<!-- default file list end -->
# How to import Google Calendar using Google Calendar API


<p>This example demonstrates how you can use the <strong>Google Calendar API</strong> in your scheduling application. Google provides the corresponding guidelines regarding use of this API:<br /><a href="https://developers.google.com/google-apps/calendar/quickstart/dotnet">Google Calendar API</a> </p>
<p>Before using this API, make certain you have read and understand <a href="https://developers.google.com/site-policies">Google’s licensing terms</a>. Next, you’ll need to generate a corresponding JSON file with credentials to enable the <strong>Google Calendar API.</strong></p>
<p>We have a corresponding KB article which contains step-by-step description on how to generate this JSON file:<br /><a href="https://www.devexpress.com/Support/Center/p/T267842">How to enable the Google Calendar API to use it in your application</a><br /><br />After you generate this JSON file and put it in the <strong>Secret </strong>folder of this sample project, you can import appointments from a Google calendar into the SchedulerControl;<br />1. Click the "<strong>Connect</strong>" button to generate a list of available calendars for your Google account<br />2. Select a corresponding calendar from which to import appointments.<br />For importing appointments, we created a corresponding <strong>CalendarImporter</strong> class which creates <strong>Appointment</strong> instances based on loaded <strong>Event (Google.Apis.Calendar.v3.Data)</strong> instances.<br /><br />Appointment entries obtained via the Google API are objects of the <strong>Google.Apis.Calendar.v3.Data.Event</strong> type. Each appointment entry is processed to create an XtraScheduler appointment, recurrence pattern or exception. To parse recurrence information contained within the appointment entry, a <strong>RecurrencePatternParser</strong> class is implemented. It creates the <strong>DevExpress.XtraScheduler.iCalendar.iCalendarEntryParser</strong> instance to parse recurrence information represented by a string in iCalendar format. The <strong>DevExpress.XtraScheduler.iCalendar.iCalendarEntryParser</strong> instance holds the parse results and provides access to property values by property names.<br />Note that a special method is required to link exceptions (changed or deleted occurrences) with their patterns, since they are created independently while processing Google appointment entries.<br /><br /><strong>P.S. To run this example's solution, include the corresponding "Google Calendar API" assemblies into the project.</strong><br /><strong>For this, open the "Package Manager Console" (Tools - NuGet Package Manager) and execute the following command:<br /></strong></p>
<pre class="prettyprint notranslate"><code>Install-Package Google.Apis.Calendar.v3</code></pre>
<p><strong> </strong></p>

<br/>


