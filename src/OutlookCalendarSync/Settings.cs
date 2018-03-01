
using System;
using System.Collections.Generic;
using System.Drawing;

namespace OutlookCalendarSync
{
    /// <summary>
    /// Description of Settings.
    /// </summary>
    public class Settings
    {
        private static Settings instance;

        public static Settings Instance
        {
            get 
            {
                if (instance == null) instance = new Settings();
                return instance;
            }
            set
            {
                instance = value;
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
