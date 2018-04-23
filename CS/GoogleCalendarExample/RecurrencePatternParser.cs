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
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.iCalendar.Components;
using DevExpress.XtraScheduler.iCalendar;
using DevExpress.XtraScheduler.iCalendar.Native;

namespace GoogleCalendarExample {
    public class RecurrencePatternParser {
        readonly SchedulerStorage storage;
        VRecurrenceRule rule = null;

        public RecurrencePatternParser(SchedulerStorage storage) {
            this.storage = storage;
            this.rule = null;
        }

        public SchedulerStorage Storage { get { return storage; } }

        public Appointment Parse(IList<string> stringValue, DateTime start, DateTime end) {
            iCalendarEntryParser parser = new iCalendarEntryParser();
            iCalendarEntryContainer entryContainer = parser.ParseString(String.Join("\n", stringValue.ToArray()));
            this.rule = entryContainer.GetPropertyValue<VRecurrenceRule>(RecurrenceRuleProperty.TokenName);
            if(this.rule == null)
                return null;

            Appointment pattern = Storage.CreateAppointment(AppointmentType.Pattern);
            pattern.Start = start;
            pattern.End = end;
            iCalendarHelper.ApplyRecurrenceInfo(pattern.RecurrenceInfo, start, rule);
            ExceptionDateTimesProperty exceptionProperty = entryContainer.GetProperty(ExceptionDateTimesProperty.TokenName) as ExceptionDateTimesProperty;
            if(exceptionProperty != null) {
                //exceptionProperty.ApplyTimeZone(new LazyTimeZoneManager());
                OccurrenceCalculator calculator = OccurrenceCalculator.CreateInstance(pattern.RecurrenceInfo);
                foreach(var item in exceptionProperty.Values) {
                    int indx = calculator.FindOccurrenceIndex(item, pattern);
                    if(indx < 0)
                        continue;
                    pattern.CreateException(AppointmentType.DeletedOccurrence, indx);
                }
            }
            return pattern;
        }
    }
}
