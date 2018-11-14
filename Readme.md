# How to import Google Calendar using Google Calendar API


<p>This example demonstrates how you can use the <strong>Google Calendar API</strong> in your scheduling application. Google provides the corresponding guidelines regarding use of this API:<br /><a href="https://developers.google.com/google-apps/calendar/quickstart/dotnet">Google Calendar API</a> </p>
<p>Before using this API, make certain you have read and understand <a href="https://developers.google.com/site-policies">Google’s licensing terms</a>. Next, you’ll need to generate a corresponding JSON file with credentials to enable the <strong>Google Calendar API.</strong></p>
<p>We have a corresponding KB article which contains step-by-step description on how to generate this JSON file:<br /><a href="https://www.devexpress.com/Support/Center/p/T267842">How to enable the Google Calendar API to use it in your application</a><br /><br />
We provide a special <a href="https://documentation.devexpress.com/WindowsForms/DevExpress.XtraScheduler.GoogleCalendar.DXGoogleCalendarSync.class">DXGoogleCalendarSync</a> component allowing you to transfer data between a Google calendar and a control storage. Please refer to the <a href="https://documentation.devexpress.com/WindowsForms/120605/Controls-and-Libraries/Scheduler/Import-and-Export/Google-Calendars">Google Calendars</a> help article to learn how to use this component.
<br/>
<br/>
<strong>P.S. To run this example's solution, include the corresponding "Google Calendar API" assemblies into the project.
For this, open the "Package Manager Console" (Tools - NuGet Package Manager) and execute the following command:

Install-Package Google.Apis.Calendar.v3</strong>

