using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;

namespace OutlookCalendarSync
{
    /// <summary>
    /// Description of OutlookCalendar.
    /// </summary>
    public class OutlookHelper
    {
        //private static OutlookHelper instance;
        private List<OutlookCalendar> _calendarFolders = new List<OutlookCalendar>();

        private string _accountName = string.Empty;

        public List<OutlookCalendar> CalendarFolders
        {
            get { return _calendarFolders; }
        }

        public string AccountName
        {
            get { return _accountName; }
        }

        public OutlookHelper()
        {
            // Create the Outlook application.
            Application oApp = new Application();

            // Get the NameSpace and Logon information.
            NameSpace oNS = oApp.GetNamespace("mapi");

            //Log on by using a dialog box to choose the profile.
            oNS.Logon("","", true, true);

            Store store = oNS.DefaultStore;
            _accountName = store.DisplayName;
            foreach (Folder mailbox in oNS.Folders)
            {
                foreach (Folder folder in mailbox.Folders)
                {
                    if (folder is MAPIFolder &&
                        folder.DefaultItemType == OlItemType.olAppointmentItem)
                    {
                        CalendarFolders.Add(new OutlookCalendar(mailbox.Name + " - " + folder.Name, (MAPIFolder)folder));
                    }
                    else
                    {
                        Marshal.ReleaseComObject(folder);
                    }
                }

                Marshal.ReleaseComObject(mailbox);
            }

            // Done. Log off.
            oNS.Logoff();
            Marshal.ReleaseComObject(oApp);
            Marshal.ReleaseComObject(oNS);
        }
    }
}
