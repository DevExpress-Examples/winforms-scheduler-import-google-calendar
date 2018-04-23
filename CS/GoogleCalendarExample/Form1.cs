using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleCalendarExample {
    public partial class Form1 : Form {
        const string DefaultCaption = "Google Calendar Importer";
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "GoogleCalendarExample";

        public Form1() {
            InitializeComponent();
            UpdateFormState();
        }

        CalendarService CalendarService { get; set; }

        void OnBtnConnectClick(object sender, EventArgs e) {            
            UserCredential credential;
            using(var stream =
                new FileStream("secret\\client_secret.json", FileMode.Open, FileAccess.Read)) {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Log("Credential file saved to: " + credPath);
            }
            
            // Create Google Calendar API service.
            CalendarService = new CalendarService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            var calendarListRequtes = CalendarService.CalendarList.List();
            CalendarList calendarList = calendarListRequtes.Execute();
            foreach(var item in calendarList.Items) {
                Log(item.Summary);
            }
            cbCalendars.DisplayMember = "Summary";
            cbCalendars.DataSource = calendarList.Items;

            cbCalendars.SelectedValueChanged += OnCbCalendarsSelectedValueChanged;
            UpdateFormState();
        }
        void OnCbCalendarsSelectedValueChanged(object sender, EventArgs e) {
            CalendarListEntry calendarEntry = this.cbCalendars.SelectedValue as CalendarListEntry;
            Calendar calendar = CalendarService.Calendars.Get(calendarEntry.Id).Execute();
            Events events = CalendarService.Events.List(calendarEntry.Id).Execute();
            Log("Loaded {0} events", events.Items.Count);
            this.schedulerStorage1.Appointments.Items.Clear();
            this.schedulerStorage1.BeginUpdate();
            try {
                CalendarImporter importer = new CalendarImporter(this.schedulerStorage1);
                importer.Import(events.Items);
            } finally {
                this.schedulerStorage1.EndUpdate();
            }
            SetStatus(String.Format("Loaded {0} events", events.Items.Count));
            UpdateFormState();
        }

        void UpdateFormState() {
            if(CalendarService == null) {
                this.cbCalendars.Enabled = false;
                this.btnUpdate.Enabled = false;
                this.btnConnect.Enabled = true;
            } else {
                this.cbCalendars.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnConnect.Enabled = false;
            }
        }

        void Log(string message) {
            this.tbLog.Text += message + "\r\n";
        }
        void Log(string format, params object[] args ) {
            Log(string.Format(format, args));
        }
        void SetStatus(string message) {
            this.tsStatus.Text = message;
        }
    }
}
