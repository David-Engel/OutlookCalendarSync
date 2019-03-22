using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
//using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Interop.Outlook;

namespace OutlookCalendarSync
{
    /// <summary>
    /// Description of OutlookCalendar.
    /// </summary>
    public class OutlookHelper
    {
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
            NameSpace oNS = (NameSpace)RetryInteropAction(new Func<NameSpace>(() =>
            {
                return oApp.GetNamespace("mapi");
            }));

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
                        Marshal.FinalReleaseComObject(folder);
                    }
                }

                Marshal.FinalReleaseComObject(mailbox);
            }

            Marshal.FinalReleaseComObject(oApp);
            Marshal.FinalReleaseComObject(oNS);
            oApp = null;
            oNS = null;
        }

        private object RetryInteropAction(Func<object> action)
        {
            int count = 0;
            while (count < 5)
            {
                try
                {
                    count++;
                    return action();
                }
                catch (COMException)
                {
                    if (count < 5)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    throw;
                }
            }

            return null;
        }
    }
}
