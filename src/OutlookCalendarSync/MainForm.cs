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

        private readonly Timer _ogstimer;
        private DateTime _oldtime;
        private readonly List<int> _minuteOffsets = new List<int>();
        private FormWindowState _previousWindowState = FormWindowState.Normal;

        private readonly AppointmentItemCache _aiCache = new AppointmentItemCache();
        private readonly BackgroundWorker _syncWorker = new BackgroundWorker();

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

            checkedListBoxCalendars.Items.Clear();
            foreach (OutlookCalendar calendar in Settings.Instance.CalendarsToSync.ToArray())
            {
                int position = checkedListBoxCalendars.Items.Add(calendar);
                checkedListBoxCalendars.SetItemChecked(position, true);
            }

            checkBoxSyncEveryHour.Checked = Settings.Instance.SyncEveryHour;
            checkBoxShowBubbleTooltips.Checked = Settings.Instance.ShowBubbleTooltipWhenSyncing;
            checkBoxStartInTray.Checked = Settings.Instance.StartInTray;
            checkBoxMinimizeToTray.Checked = Settings.Instance.MinimizeToTray;
            checkBoxCreateFiles.Checked = Settings.Instance.CreateTextFiles;

            //set up timer (every 30s) for checking the minute offsets
            _ogstimer = new Timer
            {
                Interval = 30000
            };
            _ogstimer.Tick += new EventHandler(Ogstimer_Tick);
            _ogstimer.Start();
            _oldtime = DateTime.Now;

            _syncWorker.WorkerReportsProgress = true;
            _syncWorker.WorkerSupportsCancellation = true;
            _syncWorker.DoWork += SyncWorker_DoWork;
            _syncWorker.ProgressChanged += SyncWorker_ProgressChanged;
            _syncWorker.RunWorkerCompleted += SyncWorker_RunWorkerCompleted;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Start in tray?
            if (checkBoxStartInTray.Checked)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        void Ogstimer_Tick(object sender, EventArgs e)
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

        private void SyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is string arg &&
                "DELETE".Equals(arg))
            {
                DeleteAllSyncItems();
                return;
            }

            DateTime syncStarted = DateTime.Now;

            try
            {
                Logboxout("Sync started at " + syncStarted.ToString());
                Logboxout("--------------------------------------------------");

                List<OutlookCalendar> calendarsToSync = Settings.Instance.CalendarsToSync;

                if (calendarsToSync.Count < 2)
                {
                    MessageBox.Show("You need at least two calendars to sync on the 'Settings' tab.");
                    return;
                }

                foreach (OutlookCalendar calendarFrom in calendarsToSync)
                {
                    Logboxout("Reading Outlook Calendar entries from Source:\r\n " + calendarFrom.ToString());
                    List<AppointmentItemCacheEntry> fromOutlookEntries = new List<AppointmentItemCacheEntry>();
                    foreach (AppointmentItem a in calendarFrom.GetAppointmentItemsInRange(syncStarted))
                    {
                        fromOutlookEntries.Add(_aiCache.GetAppointmentItemCacheEntry(a, calendarFrom.ToString()));
                    }

                    Logboxout("Found " + fromOutlookEntries.Count + " calendar Entries.");

                    foreach (OutlookCalendar calendarTo in calendarsToSync.Where(c => !c.ToString().Equals(calendarFrom.ToString())))
                    {

                        Logboxout("Syncing calendar from Source to Destination:\r\n " + calendarTo.ToString());
                        List<AppointmentItemCacheEntry> toOutlookEntries = new List<AppointmentItemCacheEntry>();
                        foreach (AppointmentItem a in calendarTo.GetAppointmentItemsInRange(syncStarted))
                        {
                            toOutlookEntries.Add(_aiCache.GetAppointmentItemCacheEntry(a, calendarTo.ToString()));
                        }

                        Logboxout("Found " + fromOutlookEntries.Count + " Destination calendar Entries.");

                        List<AppointmentItem> itemsToDelete = IdentifyEntriesToBeDeleted(fromOutlookEntries, toOutlookEntries, calendarFrom.ToString());
                        Logboxout("Found " + itemsToDelete.Count + " sync items to delete in Destination calendar.");

                        List<AppointmentItem> itemsToCreate = IdentifyEntriesToBeCreated(fromOutlookEntries, toOutlookEntries);
                        Logboxout("Found " + itemsToCreate.Count + " items to create in Destination calendar.");

                        if (itemsToDelete.Count > 0)
                        {
                            Logboxout("Deleting " + itemsToDelete.Count + " sync items from Destination calendar...");
                            foreach (AppointmentItem ai in itemsToDelete)
                            {
                                ai.Delete();
                            }
                        }

                        if (itemsToCreate.Count > 0)
                        {
                            Logboxout("Creating " + itemsToCreate.Count + " items in Destination calendar...");
                            MAPIFolder folderTo = null;
                            try
                            {
                                folderTo = calendarTo.GetFolder();
                                foreach (AppointmentItem ai in itemsToCreate)
                                {
                                    AppointmentItem newAi = folderTo.Items.Add(OlItemType.olAppointmentItem) as AppointmentItem;
                                    newAi.UserProperties.Add(USER_PROPERTY_NAME, OlUserPropertyType.olText).Value = calendarFrom.ToString();
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
                                    Marshal.ReleaseComObject(newAi);
                                }
                            }
                            finally
                            {
                                if (folderTo != null)
                                {
                                    Marshal.ReleaseComObject(folderTo);
                                }
                            }
                        }
                        Logboxout("Done.");
                        Logboxout("--------------------------------------------------");
                    }
                }

                foreach (OutlookCalendar cal in calendarsToSync)
                {
                    cal.ClearCache();
                }

                DateTime syncFinished = DateTime.Now;
                TimeSpan elapsed = syncFinished - syncStarted;
                Logboxout("Sync finished at " + syncFinished.ToString());
                Logboxout("Time needed: " + elapsed.Minutes + " min " + elapsed.Seconds + " s");
            }
            catch (System.Exception ex)
            {
                Logboxout("Error Syncing:\r\n" + ex.ToString());
            }

            FreeCOMResources();
        }

        private void FreeCOMResources()
        {
            try
            {
                _aiCache.ClearAndReleaseAll();

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (System.Exception ex)
            {
                Logboxout("Warning: Error freeing COM resources:\r\n" + ex.ToString());
            }
        }

        private void SyncWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonDeleteAllSyncItems.Enabled = true;
            buttonSyncNow.Enabled = true;
        }

        public List<AppointmentItem> IdentifyEntriesToBeDeleted(List<AppointmentItemCacheEntry> fromItems, List<AppointmentItemCacheEntry> toItems, string fromAccountName)
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            foreach (AppointmentItemCacheEntry toItem in toItems.Where(i => IsSyncItemForAccount(i, fromAccountName)))
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

        public List<AppointmentItem> IdentifyEntriesToBeCreated(List<AppointmentItemCacheEntry> fromItems, List<AppointmentItemCacheEntry> toItems)
        {
            List<AppointmentItem> result = new List<AppointmentItem>();
            foreach (AppointmentItemCacheEntry fromItem in fromItems.Where(i => !i.IsSyncItem))
            {
                bool found = false;
                foreach (AppointmentItemCacheEntry toItem in toItems.Where(i => IsSyncItemForAccount(i, fromItem.FromAccount)))
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

        public bool IsSyncItemForAccount(AppointmentItemCacheEntry ai, string accountName)
        {
            return (ai.IsSyncItem && accountName.Equals(ai.FromAccount));
        }

        private void Logboxout(string s)
        {
            _syncWorker.ReportProgress(0, s);
        }

        private void SyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string s = (string)e.UserState;
            textBoxLogs.Text += s + Environment.NewLine;
            textBoxLogs.SelectionStart = textBoxLogs.Text.Length - 1;
            textBoxLogs.SelectionLength = 0;
            textBoxLogs.ScrollToCaret();
        }

        void ButtonSave_Click(object sender, EventArgs e)
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

        void NumericUpDownDaysInThePast_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInThePast = (int)numericUpDownDaysInThePast.Value;
        }

        void NumericUpDownDaysInTheFuture_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.DaysInTheFuture = (int)numericUpDownDaysInTheFuture.Value;
        }

        void TextBoxMinuteOffsets_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.MinuteOffsets = textBoxMinuteOffsets.Text;

            _minuteOffsets.Clear();
            char[] delimiters = { ' ', ',', '.', ':', ';' };
            string[] chunks = textBoxMinuteOffsets.Text.Split(delimiters);
            foreach (string c in chunks)
            {
                int.TryParse(c, out int min);
                _minuteOffsets.Add(min);
            }
        }


        void CheckBoxSyncEveryHour_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.SyncEveryHour = checkBoxSyncEveryHour.Checked;
        }

        void CheckBoxShowBubbleTooltips_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.ShowBubbleTooltipWhenSyncing = checkBoxShowBubbleTooltips.Checked;
        }

        void CheckBoxStartInTray_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.StartInTray = checkBoxStartInTray.Checked;
        }

        void CheckBoxMinimizeToTray_CheckedChanged(object sender, System.EventArgs e)
        {
            Settings.Instance.MinimizeToTray = checkBoxMinimizeToTray.Checked;
        }

        void CheckBoxAddReminders_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.AddReminders = checkBoxAddReminders.Checked;
        }

        void CheckBoxCreateFiles_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.CreateTextFiles = checkBoxCreateFiles.Checked;
        }

        void NotifyIcon1_Click(object sender, EventArgs e)
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

        void LinkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabelWebsite.Text);
        }

        private void ButtonDeleteAllSyncItems_Click(object sender, EventArgs e)
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

        private void DeleteAllSyncItems()
        {
            DateTime syncStarted = DateTime.Now;

            try
            {
                Logboxout("Delete started at " + syncStarted.ToString());
                Logboxout("--------------------------------------------------");

                List<OutlookCalendar> calendarsToSync = Settings.Instance.CalendarsToSync;
                List<AppointmentItem> itemsToDelete = new List<AppointmentItem>();
                foreach (OutlookCalendar calendar in calendarsToSync)
                {
                    List<AppointmentItem> entries = calendar.GetAppointmentItemsInRange(syncStarted);
                    foreach (AppointmentItem item in entries.Where(i => _aiCache.GetAppointmentItemCacheEntry(i, calendar.ToString()).IsSyncItem))
                    {
                        itemsToDelete.Add(item);
                    }
                }

                Logboxout("Found " + itemsToDelete.Count + " sync Outlook Calendar Entries to delete.");

                if (itemsToDelete.Count > 0)
                {
                    Logboxout("Deleting items...");

                    foreach (AppointmentItem item in itemsToDelete)
                    {
                        item.Delete();
                        Marshal.ReleaseComObject(item);
                    }
                }

                Logboxout("Done.");
                Logboxout("--------------------------------------------------");

                DateTime syncFinished = DateTime.Now;
                TimeSpan elapsed = syncFinished - syncStarted;
                Logboxout("Delete finished at " + syncFinished.ToString());
                Logboxout("Time needed: " + elapsed.Minutes + " min " + elapsed.Seconds + " s");
            }
            catch (System.Exception ex)
            {
                Logboxout("Error Deleting:\r\n" + ex.ToString());
            }

            FreeCOMResources();
        }

        private void CheckedListBoxCalendars_ItemCheck(object sender, ItemCheckEventArgs e)
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
            else
            {
                checkedItems.Remove((OutlookCalendar)checkedListBoxCalendars.Items[e.Index]);
            }

            Settings.Instance.CalendarsToSync = checkedItems;
        }

        private void ButtonLoadCalendars_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<OutlookCalendar> saved = new List<OutlookCalendar>();
            saved.AddRange(Settings.Instance.CalendarsToSync.ToArray());
            try
            {
                checkedListBoxCalendars.Items.Clear();
                foreach (OutlookCalendar calendar in OutlookHelper.GetCalendars())
                {
                    int position = checkedListBoxCalendars.Items.Add(calendar);
                    if (saved.Exists(c => c.ToString().Equals(calendar.ToString())))
                    {
                        checkedListBoxCalendars.SetItemChecked(position, true);
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (System.Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
