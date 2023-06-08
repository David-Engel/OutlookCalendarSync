using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace OutlookCalendarSync
{
    public static class OutlookHelper
    {
        public static List<OutlookCalendar> GetCalendars()
        {
            List<OutlookCalendar> calendars = new List<OutlookCalendar>();

            // Create the Outlook application.
            Application oApp = null;
            NameSpace oNS = null;

            try
            {
                oApp = new Application();

                // Get the NameSpace and Logon information.
                oNS = (NameSpace)RetryInteropAction(new Func<NameSpace>(() =>
                {
                    return oApp.GetNamespace("mapi");
                }));

                Store store = oNS.DefaultStore;
                foreach (Folder mailbox in oNS.Folders)
                {
                    foreach (Folder folder in mailbox.Folders)
                    {
                        if (folder is MAPIFolder &&
                            folder.DefaultItemType == OlItemType.olAppointmentItem)
                        {
                            calendars.Add(new OutlookCalendar(mailbox.Name, folder.Name));
                        }

                        Marshal.FinalReleaseComObject(folder);
                    }

                    Marshal.FinalReleaseComObject(mailbox);
                }
            }
            finally
            {
                if (oApp != null)
                {
                    Marshal.FinalReleaseComObject(oApp);
                    oApp = null;
                }

                if (oNS != null)
                {
                    Marshal.FinalReleaseComObject(oNS);
                    oNS = null;
                }
            }

            return calendars;
        }

        public static MAPIFolder GetFolder(OutlookCalendar calendar)
        {
            MAPIFolder returnFolder = null;

            // Create the Outlook application.
            Application oApp = null;
            NameSpace oNS = null;

            try
            {
                oApp = new Application();

                // Get the NameSpace and Logon information.
                oNS = (NameSpace)RetryInteropAction(new Func<NameSpace>(() =>
                {
                    return oApp.GetNamespace("mapi");
                }));

                Store store = oNS.DefaultStore;
                foreach (Folder mailbox in oNS.Folders)
                {
                    if (mailbox.Name.Equals(calendar.MailboxName))
                    {
                        foreach (Folder folder in mailbox.Folders)
                        {
                            if (folder is MAPIFolder &&
                                folder.DefaultItemType == OlItemType.olAppointmentItem &&
                                folder.Name.Equals(calendar.FolderName))
                            {
                                returnFolder = folder;
                            }
                            else
                            {
                                Marshal.FinalReleaseComObject(folder);
                            }
                        }
                    }

                    Marshal.FinalReleaseComObject(mailbox);
                }
            }
            finally
            {
                if (oApp != null)
                {
                    Marshal.FinalReleaseComObject(oApp);
                    oApp = null;
                }

                if (oNS != null)
                {
                    Marshal.FinalReleaseComObject(oNS);
                    oNS = null;
                }
            }

            return returnFolder;
        }

        private static object RetryInteropAction(Func<object> action)
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
