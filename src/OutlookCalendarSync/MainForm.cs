using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;

namespace OutlookCalendarSync
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public static MainForm Instance;

        public const string FILENAME = "settings.xml";
        public const string USER_PROPERTY_NAME = "OutlookCalendarSyncFlag";
        public const string SUBJECT_PREFIX = "(c)";

        public Timer ogstimer;
        public DateTime oldtime;
        public List<int> MinuteOffsets = new List<int>();

        private Dictionary<AppointmentItem, string> _outlookAppointmentSignatures = new Dictionary<AppointmentItem, string>();

        public MainForm()
        {
            InitializeComponent();
            labelAbout.Text = labelAbout.Text.Replace("{version}", System.Windows.Forms.Application.ProductVersion);

            Instance = this;

            //load settings/create settings file
            if (File.Exists(FILENAME))
            {
                Settings.Instance = XMLManager.import<Settings>(FILENAME);
            }
            else
            {
                XMLManager.export(Settings.Instance, FILENAME);
            }

            //update GUI from Settings
            tbDaysInThePast.Text = Settings.Instance.DaysInThePast.ToString();
            tbDaysInTheFuture.Text = Settings.Instance.DaysInTheFuture.ToString();
            tbMinuteOffsets.Text = Settings.Instance.MinuteOffsets;
            OutlookHelper oh = new OutlookHelper();
            checkedListBoxCalendars.Items.Clear();
            List<OutlookCalendar> savedCalendarsToSync = new List<OutlookCalendar>();
            savedCalendarsToSync.AddRange(Settings.Instance.CalendarsToSync.ToArray());
            foreach (OutlookCalendar calendar in oh.CalendarFolders)
            {
                int position = checkedListBoxCalendars.Items.Add(calendar);
                foreach (OutlookCalendar entry in savedCalendarsToSync)
                {
                    if (entry.Name.Equals(calendar.Name))
                    {
                        checkedListBoxCalendars.SetItemChecked(position, true);
                    }
                }
            }
            cbSyncEveryHour.Checked = Settings.Instance.SyncEveryHour;
            cbShowBubbleTooltips.Checked = Settings.Instance.ShowBubbleTooltipWhenSyncing;
            cbStartInTray.Checked = Settings.Instance.StartInTray;
            cbMinimizeToTray.Checked = Settings.Instance.MinimizeToTray;
            cbCreateFiles.Checked = Settings.Instance.CreateTextFiles;

            //Start in tray?
            if (cbStartInTray.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;
                this.Hide();
            }

            //set up timer (every 30s) for checking the minute offsets
            ogstimer = new Timer();
            ogstimer.Interval = 30000;
            ogstimer.Tick += new EventHandler(ogstimer_Tick);
            ogstimer.Start();
            oldtime = DateTime.Now;

            //set up tooltips for some controls
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 200;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(tbMinuteOffsets,
                "One ore more Minute Offsets at which the sync is automatically started each hour. \n" +
                "Separate by comma (e.g. 5,15,25).");
            toolTip1.SetToolTip(cbAddReminders,
                "If checked, the reminder will be duplicated across calendars.");
            toolTip1.SetToolTip(cbCreateFiles,
                "If checked, all entries found in Outlook and identified for creation/deletion will be exported \n" +
                "to 4 separate text files in the application's directory (named \"export_*.txt\"). \n" +
                "Only for debug/diagnostic purposes.");
            toolTip1.SetToolTip(buttonDeleteAllSyncItems,
                "Delete all sync calendar items which were created by this utility on the selected Calendars falling within the day range defined.");
        }

        void ogstimer_Tick(object sender, EventArgs e)
        {
            if (!cbSyncEveryHour.Checked)
                return;
            DateTime newtime = DateTime.Now;
            if (newtime.Minute != oldtime.Minute)
            {
                oldtime = newtime;
                if (MinuteOffsets.Contains(newtime.Minute))
                {
                    if (cbShowBubbleTooltips.Checked)
                        notifyIcon1.ShowBalloonTip(
                            500,
                            "OutlookCalendarSync",
                            "Sync started at desired minute offset " + newtime.Minute.ToString(),
                            ToolTipIcon.Info
                            );
                    SyncNow_Click(null, null);
                }
            }
        }

        void SyncNow_Click(object sender, EventArgs e)
        {
            _outlookAppointmentSignatures.Clear();

            bSyncNow.Enabled = false;
            buttonDeleteAllSyncItems.Enabled = false;

            LogBox.Clear();

            DateTime SyncStarted = DateTime.Now;

            try
            {
                logboxout("Sync started at " + SyncStarted.ToString());
                logboxout("--------------------------------------------------");


                OutlookHelper oh = new OutlookHelper();
                List<OutlookCalendar> calendarsToSync = new List<OutlookCalendar>();
                foreach (OutlookCalendar calendar in oh.CalendarFolders)
                {
                    foreach (OutlookCalendar entry in Settings.Instance.CalendarsToSync)
                    {
                        if (entry.Name.Equals(calendar.Name))
                        {
                            calendarsToSync.Add(calendar);
                        }
                    }
                }

                if (calendarsToSync.Count < 2)
                {
                    MessageBox.Show("You need at least two calendars to sync on the 'Settings' tab.");
                    return;
                }

                foreach (OutlookCalendar calendarFrom in calendarsToSync)
                {
                    logboxout("Reading Outlook Calendar entries from " + calendarFrom.Name + " as Source...");
                    List<AppointmentItem> fromOutlookEntries = calendarFrom.getAppointmentItemsInRange();

                    logboxout("Found " + fromOutlookEntries.Count + " calendar Entries.");
                    logboxout("--------------------------------------------------");

                    foreach (OutlookCalendar calendarTo in calendarsToSync.Where(c => !c.Name.Equals(calendarFrom.Name)))
                    {

                        logboxout("Syncing calendar from " + calendarFrom.Name + " to " + calendarTo.Name + " as Destination");
                        List<AppointmentItem> toOutlookEntries = calendarTo.getAppointmentItemsInRange();
                        logboxout("Found " + fromOutlookEntries.Count + " Destination calendar Entries.");
                        logboxout("--------------------------------------------------");

                        List<AppointmentItem> itemsToDelete = IdentifyEntriesToBeDeleted(fromOutlookEntries, toOutlookEntries, calendarFrom.Name);
                        logboxout("Found " + itemsToDelete.Count + " sync items to delete in Destination calendar.");

                        List<AppointmentItem> itemsToCreate = IdentifyEntriesToBeCreated(fromOutlookEntries, toOutlookEntries, calendarFrom.Name);
                        logboxout("Found " + itemsToCreate.Count + " items to create in Destination calendar.");

                        if (itemsToDelete.Count > 0)
                        {
                            logboxout("Deleting " + itemsToDelete.Count + " sync items from Destination calendar...");
                            foreach (AppointmentItem ai in itemsToDelete)
                            {
                                ai.Delete();
                            }
                            logboxout("Done.");
                            logboxout("--------------------------------------------------");
                        }

                        if (itemsToCreate.Count > 0)
                        {
                            logboxout("Creating " + itemsToCreate.Count + " items in Destination calendar...");
                            foreach (AppointmentItem ai in itemsToCreate)
                            {
                                AppointmentItem newAi = calendarTo.Folder.Items.Add(OlItemType.olAppointmentItem) as AppointmentItem;
                                newAi.UserProperties.Add(USER_PROPERTY_NAME, OlUserPropertyType.olText).Value = calendarFrom.Name;
                                newAi.Start = ai.Start;
                                newAi.StartTimeZone = ai.StartTimeZone;
                                newAi.End = ai.End;
                                newAi.EndTimeZone = ai.EndTimeZone;
                                newAi.AllDayEvent = ai.AllDayEvent;
                                newAi.Subject = SUBJECT_PREFIX + ai.Subject;
                                newAi.Location = ai.Location;
                                newAi.BusyStatus = ai.BusyStatus;
                                newAi.Importance = ai.Importance;
                                newAi.Sensitivity = ai.Sensitivity;
                                newAi.RequiredAttendees = ai.RequiredAttendees;
                                newAi.OptionalAttendees = ai.OptionalAttendees;
                                newAi.RTFBody = ai.RTFBody;
                                newAi.Body = ai.Body;
                                newAi.Body += Environment.NewLine;
                                newAi.Body += Environment.NewLine + "==============================================";
                                newAi.Body += Environment.NewLine + "Added by OutlookCalendarSync (" + calendarFrom.Name + "):" + Environment.NewLine;
                                newAi.Body += Environment.NewLine + "ORGANIZER: " + Environment.NewLine + ai.Organizer + Environment.NewLine;
                                newAi.Body += Environment.NewLine + "==============================================";

                                if (cbAddReminders.Checked && ai.ReminderSet)
                                {
                                    newAi.ReminderSet = ai.ReminderSet;
                                    newAi.ReminderMinutesBeforeStart = ai.ReminderMinutesBeforeStart;
                                }
                                else
                                {
                                    newAi.ReminderSet = false;
                                }

                                newAi.Save();
                                //Marshal.ReleaseComObject(newAi);
                            }
                            logboxout("Done.");
                            logboxout("--------------------------------------------------");
                        }
                    }
                }

                DateTime SyncFinished = DateTime.Now;
                TimeSpan Elapsed = SyncFinished - SyncStarted;
                logboxout("Sync finished at " + SyncFinished.ToString());
                logboxout("Time needed: " + Elapsed.Minutes + " min " + Elapsed.Seconds + " s");
            }
            catch (System.Exception ex)
            {
                logboxout("Error Syncing:\r\n" + ex.ToString());
            }

            buttonDeleteAllSyncItems.Enabled = true;
            bSyncNow.Enabled = true;
        }

        public List<AppointmentItem> IdentifyEntriesToBeDeleted(List<AppointmentItem> fromItems, List<AppointmentItem> toItems, string fromAccountName)
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            foreach (AppointmentItem toItem in toItems.Where(i => isSyncItemForAccount(i, fromAccountName)))
            {
                bool found = false;
                foreach (AppointmentItem fromItem in fromItems.Where(i => !isSyncItem(i)))
                {
                    if (signature(fromItem) == signature(toItem))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    result.Add(toItem);
                }
            }

            return result;
        }

        public List<AppointmentItem> IdentifyEntriesToBeCreated(List<AppointmentItem> fromItems, List<AppointmentItem> toItems, string fromAccountName)
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            foreach (AppointmentItem fromItem in fromItems.Where(i => !isSyncItem(i)))
            {
                bool found = false;
                foreach (AppointmentItem toItem in toItems.Where(i => isSyncItemForAccount(i, fromAccountName)))
                {
                    if (signature(fromItem) == signature(toItem))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    result.Add(fromItem);
                }
            }

            return result;
        }

        //creates a standardized summary string with the key attributes of a calendar entry for comparison
        public string signature(AppointmentItem ai)
        {
            if (!_outlookAppointmentSignatures.ContainsKey(ai))
            {
                if (isSyncItem(ai))
                {
                    _outlookAppointmentSignatures.Add(ai, (ai.Start + ";" + ai.End + ";" + ai.Subject.Substring(SUBJECT_PREFIX.Length) + ";" + ai.Location).Trim());
                }
                else
                {
                    _outlookAppointmentSignatures.Add(ai, (ai.Start + ";" + ai.End + ";" + ai.Subject + ";" + ai.Location).Trim());
                }
            }

            return _outlookAppointmentSignatures[ai];
        }

        public bool isSyncItem(AppointmentItem ai)
        {
            if (ai.UserProperties != null)
            {
                var flag = ai.UserProperties.Find(USER_PROPERTY_NAME);
                return (flag != null);
            }

            return false;
        }

        public bool isSyncItemForAccount(AppointmentItem ai, string accountName)
        {
            if (ai.UserProperties != null)
            {
                var flag = ai.UserProperties.Find(USER_PROPERTY_NAME, OlUserPropertyType.olText);
                if (flag != null &&
                    flag.Value as string == accountName)
                {
                    return true;
                }
            }

            return false;
        }

        void logboxout(string s)
        {
            LogBox.Text += s + Environment.NewLine;
            LogBox.SelectionStart = LogBox.Text.Length - 1;
            LogBox.SelectionLength = 0;
            LogBox.ScrollToCaret();
        }

        void Save_Click(object sender, EventArgs e)
        {
            try
            {
                XMLManager.export(Settings.Instance, FILENAME);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error Saving:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void TbDaysInThePastTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInThePast = int.Parse(tbDaysInThePast.Text);
        }

        void TbDaysInTheFutureTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInTheFuture = int.Parse(tbDaysInTheFuture.Text);
        }

        void TbMinuteOffsetsTextChanged(object sender, EventArgs e)
        {
            Settings.Instance.MinuteOffsets = tbMinuteOffsets.Text;

            MinuteOffsets.Clear();
            char[] delimiters = { ' ', ',', '.', ':', ';' };
            string[] chunks = tbMinuteOffsets.Text.Split(delimiters);
            foreach (string c in chunks)
            {
                int min = 0;
                int.TryParse(c, out min);
                MinuteOffsets.Add(min);
            }
        }


        void CbSyncEveryHourCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.SyncEveryHour = cbSyncEveryHour.Checked;
        }

        void CbShowBubbleTooltipsCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.ShowBubbleTooltipWhenSyncing = cbShowBubbleTooltips.Checked;
        }

        void CbStartInTrayCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.StartInTray = cbStartInTray.Checked;
        }

        void CbMinimizeToTrayCheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.MinimizeToTray = cbMinimizeToTray.Checked;
        }

        void CbAddRemindersCheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.AddReminders = cbAddReminders.Checked;
        }

        void cbCreateFiles_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.CreateTextFiles = cbCreateFiles.Checked;
        }

        void NotifyIcon1Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        void MainFormResize(object sender, EventArgs e)
        {
            if (!cbMinimizeToTray.Checked)
                return;
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                //notifyIcon1.ShowBalloonTip(500, "OutlookCalendarSync", "Click to open again.", ToolTipIcon.Info);
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon1.Visible = false;
            }
        }

        void LinkLabel1LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void buttonDeleteAllSyncItems_Click(object sender, EventArgs e)
        {
            buttonDeleteAllSyncItems.Enabled = false;
            bSyncNow.Enabled = false;
            LogBox.Clear();
            DateTime SyncStarted = DateTime.Now;

            try
            {
                logboxout("Delete started at " + SyncStarted.ToString());
                logboxout("--------------------------------------------------");

                OutlookHelper oh = new OutlookHelper();
                List<OutlookCalendar> calendarsToSync = Settings.Instance.CalendarsToSync;
                List<AppointmentItem> itemsToDelete = new List<AppointmentItem>();
                foreach (OutlookCalendar calendar in oh.CalendarFolders)
                {
                    if (!calendarsToSync.Exists(c => c.Name.Equals(calendar.Name)))
                    {
                        //Not a synced calendar
                        continue;
                    }

                    List<AppointmentItem> entries = calendar.getAppointmentItemsInRange();
                    foreach (AppointmentItem item in entries.Where(i => isSyncItem(i)))
                    {
                        itemsToDelete.Add(item);
                    }
                }

                logboxout("Found " + itemsToDelete.Count + " sync Outlook Calendar Entries to delete.");

                if (itemsToDelete.Count > 0)
                {
                    logboxout("Deleting items...");

                    foreach (AppointmentItem item in itemsToDelete)
                    {
                        item.Delete();
                    }
                }

                logboxout("Done.");
                logboxout("--------------------------------------------------");

                DateTime SyncFinished = DateTime.Now;
                TimeSpan Elapsed = SyncFinished - SyncStarted;
                logboxout("Delete finished at " + SyncFinished.ToString());
                logboxout("Time needed: " + Elapsed.Minutes + " min " + Elapsed.Seconds + " s");
            }
            catch (System.Exception ex)
            {
                logboxout("Error Deleting:\r\n" + ex.ToString());
            }

            buttonDeleteAllSyncItems.Enabled = true;
            bSyncNow.Enabled = true;
        }

        private void checkedListBoxCalendars_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<OutlookCalendar> checkedItems = new List<OutlookCalendar>();
            foreach (var item in checkedListBoxCalendars.CheckedItems)
            {
                checkedItems.Add((OutlookCalendar)item);
            }

            if (e.NewValue == CheckState.Checked)
            {
                checkedItems.Add((OutlookCalendar)checkedListBoxCalendars.Items[e.Index]);
            }

            Settings.Instance.CalendarsToSync = checkedItems;
        }
    }
}
