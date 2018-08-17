using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OutlookCalendarSync
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private const string FILENAME = "settings.xml";
        internal const string USER_PROPERTY_NAME = "OutlookCalendarSyncFlag";
        internal const string SUBJECT_PREFIX = "(c)";

        private Timer _ogstimer;
        private DateTime _oldtime;
        private List<int> _minuteOffsets = new List<int>();
        private FormWindowState _previousWindowState = FormWindowState.Normal;

        private AppointmentItemCache _aiCache = new AppointmentItemCache();
        private BackgroundWorker _syncWorker = new BackgroundWorker();

        public MainForm()
        {
            InitializeComponent();
            labelAbout.Text = labelAbout.Text.Replace("{version}", System.Windows.Forms.Application.ProductVersion);

            //load settings/create settings file
            if (File.Exists(FILENAME))
            {
                Settings.Instance = XMLManager.Import<Settings>(FILENAME);
            }
            else
            {
                XMLManager.Export(Settings.Instance, FILENAME);
            }

            //update GUI from Settings
            numericUpDownDaysInThePast.Value = Settings.Instance.DaysInThePast;
            numericUpDownDaysInTheFuture.Value = Settings.Instance.DaysInTheFuture;
            textBoxMinuteOffsets.Text = Settings.Instance.MinuteOffsets;
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

            checkBoxSyncEveryHour.Checked = Settings.Instance.SyncEveryHour;
            checkBoxShowBubbleTooltips.Checked = Settings.Instance.ShowBubbleTooltipWhenSyncing;
            checkBoxStartInTray.Checked = Settings.Instance.StartInTray;
            checkBoxMinimizeToTray.Checked = Settings.Instance.MinimizeToTray;
            checkBoxCreateFiles.Checked = Settings.Instance.CreateTextFiles;

            //set up timer (every 30s) for checking the minute offsets
            _ogstimer = new Timer();
            _ogstimer.Interval = 30000;
            _ogstimer.Tick += new EventHandler(ogstimer_Tick);
            _ogstimer.Start();
            _oldtime = DateTime.Now;

            _syncWorker.WorkerReportsProgress = true;
            _syncWorker.WorkerSupportsCancellation = true;
            _syncWorker.DoWork += syncWorker_DoWork;
            _syncWorker.ProgressChanged += syncWorker_ProgressChanged;
            _syncWorker.RunWorkerCompleted += syncWorker_RunWorkerCompleted;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Start in tray?
            if (checkBoxStartInTray.Checked)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        void ogstimer_Tick(object sender, EventArgs e)
        {
            if (!checkBoxSyncEveryHour.Checked)
                return;

            DateTime newtime = DateTime.Now;
            if (newtime.Minute != _oldtime.Minute)
            {
                _oldtime = newtime;
                if (_minuteOffsets.Contains(newtime.Minute))
                {
                    if (checkBoxShowBubbleTooltips.Checked)
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

        private void SyncNow_Click(object sender, EventArgs e)
        {
            if (_syncWorker.IsBusy)
            {
                return;
            }

            buttonSyncNow.Enabled = false;
            buttonDeleteAllSyncItems.Enabled = false;

            textBoxLogs.Clear();

            _syncWorker.RunWorkerAsync();
        }

        private void syncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is string &&
                "DELETE".Equals((string)e.Argument))
            {
                deleteAllSyncItems();
                return;
            }

            DateTime syncStarted = DateTime.Now;

            OutlookHelper oh = null;

            try
            {
                logboxout("Sync started at " + syncStarted.ToString());
                logboxout("--------------------------------------------------");


                oh = new OutlookHelper();
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
                    logboxout("Reading Outlook Calendar entries from Source:\r\n " + calendarFrom.Name);
                    List<AppointmentItemCacheEntry> fromOutlookEntries = new List<AppointmentItemCacheEntry>();
                    foreach (AppointmentItem a in calendarFrom.GetAppointmentItemsInRange(syncStarted))
                    {
                        fromOutlookEntries.Add(_aiCache.GetAppointmentItemCacheEntry(a, calendarFrom.Name));
                    }

                    logboxout("Found " + fromOutlookEntries.Count + " calendar Entries.");

                    foreach (OutlookCalendar calendarTo in calendarsToSync.Where(c => !c.Name.Equals(calendarFrom.Name)))
                    {

                        logboxout("Syncing calendar from Source to Destination:\r\n " + calendarTo.Name);
                        List<AppointmentItemCacheEntry> toOutlookEntries = new List<AppointmentItemCacheEntry>();
                        foreach (AppointmentItem a in calendarTo.GetAppointmentItemsInRange(syncStarted))
                        {
                            toOutlookEntries.Add(_aiCache.GetAppointmentItemCacheEntry(a, calendarTo.Name));
                        }

                        logboxout("Found " + fromOutlookEntries.Count + " Destination calendar Entries.");

                        List<AppointmentItem> itemsToDelete = identifyEntriesToBeDeleted(fromOutlookEntries, toOutlookEntries, calendarFrom.Name);
                        logboxout("Found " + itemsToDelete.Count + " sync items to delete in Destination calendar.");

                        List<AppointmentItem> itemsToCreate = identifyEntriesToBeCreated(fromOutlookEntries, toOutlookEntries);
                        logboxout("Found " + itemsToCreate.Count + " items to create in Destination calendar.");

                        if (itemsToDelete.Count > 0)
                        {
                            logboxout("Deleting " + itemsToDelete.Count + " sync items from Destination calendar...");
                            foreach (AppointmentItem ai in itemsToDelete)
                            {
                                ai.Delete();
                            }
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

                                if (checkBoxAddReminders.Checked && ai.ReminderSet)
                                {
                                    newAi.ReminderSet = ai.ReminderSet;
                                    newAi.ReminderMinutesBeforeStart = ai.ReminderMinutesBeforeStart;
                                }
                                else
                                {
                                    newAi.ReminderSet = false;
                                }

                                newAi.Save();
                            }
                        }
                        logboxout("Done.");
                        logboxout("--------------------------------------------------");
                    }
                }

                DateTime syncFinished = DateTime.Now;
                TimeSpan elapsed = syncFinished - syncStarted;
                logboxout("Sync finished at " + syncFinished.ToString());
                logboxout("Time needed: " + elapsed.Minutes + " min " + elapsed.Seconds + " s");
            }
            catch (System.Exception ex)
            {
                logboxout("Error Syncing:\r\n" + ex.ToString());
            }

            freeCOMResources(oh);
        }

        private void freeCOMResources(OutlookHelper oh)
        {
            try
            {
                _aiCache.ClearAndReleaseAll();
                if (oh != null && oh.CalendarFolders != null)
                {
                    foreach (OutlookCalendar oc in oh.CalendarFolders)
                    {
                        if (oc != null && oc.Folder != null)
                        {
                            Marshal.FinalReleaseComObject(oc.Folder);
                        }
                    }
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (System.Exception ex)
            {
                logboxout("Warning: Error freeing COM resources:\r\n" + ex.ToString());
            }
        }

        private void syncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonDeleteAllSyncItems.Enabled = true;
            buttonSyncNow.Enabled = true;
        }

        public List<AppointmentItem> identifyEntriesToBeDeleted(List<AppointmentItemCacheEntry> fromItems, List<AppointmentItemCacheEntry> toItems, string fromAccountName)
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            foreach (AppointmentItemCacheEntry toItem in toItems.Where(i => isSyncItemForAccount(i, fromAccountName)))
            {
                bool found = false;
                foreach (AppointmentItemCacheEntry fromItem in fromItems.Where(i => !i.IsSyncItem))
                {
                    if (fromItem.Signature.Equals(toItem.Signature))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    result.Add(toItem.AppointmentItem);
                }
            }

            return result;
        }

        public List<AppointmentItem> identifyEntriesToBeCreated(List<AppointmentItemCacheEntry> fromItems, List<AppointmentItemCacheEntry> toItems)
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            foreach (AppointmentItemCacheEntry fromItem in fromItems.Where(i => !i.IsSyncItem))
            {
                bool found = false;
                foreach (AppointmentItemCacheEntry toItem in toItems.Where(i => isSyncItemForAccount(i, fromItem.FromAccount)))
                {
                    if (fromItem.Signature.Equals(toItem.Signature))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    result.Add(fromItem.AppointmentItem);
                }
            }

            return result;
        }

        public bool isSyncItemForAccount(AppointmentItemCacheEntry ai, string accountName)
        {
            return (ai.IsSyncItem && accountName.Equals(ai.FromAccount));
        }

        private void logboxout(string s)
        {
            _syncWorker.ReportProgress(0, s);
        }

        private void syncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string s = (string)e.UserState;
            textBoxLogs.Text += s + Environment.NewLine;
            textBoxLogs.SelectionStart = textBoxLogs.Text.Length - 1;
            textBoxLogs.SelectionLength = 0;
            textBoxLogs.ScrollToCaret();
        }

        void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                XMLManager.Export(Settings.Instance, FILENAME);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error Saving:\r\n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void numericUpDownDaysInThePast_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInThePast = (int)numericUpDownDaysInThePast.Value;
        }

        void numericUpDownDaysInTheFuture_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInTheFuture = (int)numericUpDownDaysInTheFuture.Value;
        }

        void textBoxMinuteOffsets_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.MinuteOffsets = textBoxMinuteOffsets.Text;

            _minuteOffsets.Clear();
            char[] delimiters = { ' ', ',', '.', ':', ';' };
            string[] chunks = textBoxMinuteOffsets.Text.Split(delimiters);
            foreach (string c in chunks)
            {
                int min = 0;
                int.TryParse(c, out min);
                _minuteOffsets.Add(min);
            }
        }


        void checkBoxSyncEveryHour_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.SyncEveryHour = checkBoxSyncEveryHour.Checked;
        }

        void checkBoxShowBubbleTooltips_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.ShowBubbleTooltipWhenSyncing = checkBoxShowBubbleTooltips.Checked;
        }

        void checkBoxStartInTray_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.StartInTray = checkBoxStartInTray.Checked;
        }

        void checkBoxMinimizeToTray_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.MinimizeToTray = checkBoxMinimizeToTray.Checked;
        }

        void checkBoxAddReminders_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.AddReminders = checkBoxAddReminders.Checked;
        }

        void checkBoxCreateFiles_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.CreateTextFiles = checkBoxCreateFiles.Checked;
        }

        void notifyIcon1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = _previousWindowState;
        }

        void MainForm_Resized(object sender, EventArgs e)
        {
            if (!checkBoxMinimizeToTray.Checked)
            {
                return;
            }

            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                ShowInTaskbar = false;
                Hide();
            }
            else
            {
                _previousWindowState = WindowState;
                notifyIcon1.Visible = false;
                ShowInTaskbar = true;
            }
        }

        void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabelWebsite.Text);
        }

        private void buttonDeleteAllSyncItems_Click(object sender, EventArgs e)
        {
            if (_syncWorker.IsBusy)
            {
                return;
            }

            buttonDeleteAllSyncItems.Enabled = false;
            buttonSyncNow.Enabled = false;
            textBoxLogs.Clear();

            _syncWorker.RunWorkerAsync("DELETE");
        }

        private void deleteAllSyncItems()
        {
            DateTime syncStarted = DateTime.Now;

            OutlookHelper oh = null;

            try
            {
                logboxout("Delete started at " + syncStarted.ToString());
                logboxout("--------------------------------------------------");

                oh = new OutlookHelper();
                List<OutlookCalendar> calendarsToSync = Settings.Instance.CalendarsToSync;
                List<AppointmentItem> itemsToDelete = new List<AppointmentItem>();
                foreach (OutlookCalendar calendar in oh.CalendarFolders)
                {
                    if (!calendarsToSync.Exists(c => c.Name.Equals(calendar.Name)))
                    {
                        //Not a synced calendar
                        continue;
                    }

                    List<AppointmentItem> entries = calendar.GetAppointmentItemsInRange(syncStarted);
                    foreach (AppointmentItem item in entries.Where(i => _aiCache.GetAppointmentItemCacheEntry(i, calendar.Name).IsSyncItem))
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

                DateTime syncFinished = DateTime.Now;
                TimeSpan elapsed = syncFinished - syncStarted;
                logboxout("Delete finished at " + syncFinished.ToString());
                logboxout("Time needed: " + elapsed.Minutes + " min " + elapsed.Seconds + " s");
            }
            catch (System.Exception ex)
            {
                logboxout("Error Deleting:\r\n" + ex.ToString());
            }

            freeCOMResources(oh);
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
