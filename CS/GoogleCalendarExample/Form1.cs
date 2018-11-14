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
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "GoogleCalendarExample";
        
        public Form1() {
            InitializeComponent();
            UpdateFormState();
            schedulerControl1.Start = DateTime.Now;
        }

        CalendarService CalendarService { get; set; }
        CalendarListEntry CalendarEntry { get; set; }

        void Synchronize() {
            this.dxGoogleCalendarSync1.CalendarService = CalendarService;
            this.dxGoogleCalendarSync1.CalendarId = CalendarEntry.Id;
            this.dxGoogleCalendarSync1.Synchronize();
        }

        void OnBtnConnectClick(object sender, EventArgs e) {
            cbCalendars.SelectedValueChanged -= OnCbCalendarsSelectedValueChanged;
            UserCredential credential;
            using(var stream =
                new FileStream("secret\\client_secret.json", FileMode.Open, FileAccess.Read)) {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentialss");

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
            CalendarEntry = this.cbCalendars.SelectedValue as CalendarListEntry;
            Synchronize();
            UpdateFormState();
        }
        void OnCbCalendarsSelectedValueChanged(object sender, EventArgs e) {
            CalendarEntry = this.cbCalendars.SelectedValue as CalendarListEntry;
            this.dxGoogleCalendarSync1.Storage = null;
            this.schedulerStorage1.Appointments.Clear();
            this.dxGoogleCalendarSync1.Storage = this.schedulerStorage1;
            Synchronize();            
            UpdateFormState();
        }

        void UpdateFormState() {
            if(CalendarService == null) {
                this.cbCalendars.Enabled = false;
                this.btnUpdate.Enabled = false;
                this.btnConnect.Enabled = true;
            }
            else {
                this.cbCalendars.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnConnect.Enabled = false;
            }
        }

        void Log(string message) {
            this.tbLog.Text += message + "\r\n";
        }
        void Log(string format, params object[] args) {
            Log(string.Format(format, args));
        }
        void SetStatus(string message) {
            this.tsStatus.Text = message;
        }

        void OnBtnUpdateClick(object sender, EventArgs e) {
            Synchronize();
        }       
    }
}
