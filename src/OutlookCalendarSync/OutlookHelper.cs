using System;
using System.Collections.Generic;
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
        private List<OutlookCalendar> calendarFolders = new List<OutlookCalendar>();

        private string _accountName = string.Empty;

        public List<OutlookCalendar> CalendarFolders
        {
            get { return calendarFolders; }
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
            // Outlook.NameSpace oNS = (Outlook.NameSpace)oApp.GetNamespace("mapi");
            NameSpace oNS = oApp.GetNamespace("mapi");

            //Log on by using a dialog box to choose the profile.
            oNS.Logon("","", true, true);

            _accountName = oNS.DefaultStore.DisplayName;
            foreach (Folder mailbox in oNS.Folders)
            {
                foreach (Folder folder in mailbox.Folders)
                {
                    if (folder is MAPIFolder &&
                        folder.DefaultItemType == OlItemType.olAppointmentItem)
                    {
                        CalendarFolders.Add(new OutlookCalendar(mailbox.Name + " - " + folder.Name, (MAPIFolder)folder));
                    }
                }
            }

            // Done. Log off.
            oNS.Logoff();
        }
    }
}
