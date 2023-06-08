using System.Collections.Generic;

namespace OutlookCalendarSync
{
    public class Settings
    {
        private static Settings _instance;

        public static Settings Instance
        {
            get
            {
                if (_instance == null) _instance = new Settings();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public string RefreshToken = "";
        public string MinuteOffsets = "58";
        public int DaysInThePast = 14;
        public int DaysInTheFuture = 60;
        public List<OutlookCalendar> CalendarsToSync = new List<OutlookCalendar>();

        public bool SyncEveryHour = false;
        public bool ShowBubbleTooltipWhenSyncing = true;
        public bool StartInTray = false;
        public bool MinimizeToTray = true;

        public bool AddReminders = false;
        public bool CreateTextFiles = true;

        public Settings()
        {
        }
    }
}
