using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Outlook;

namespace OutlookCalendarSync
{
    public class OutlookCalendar
    {
        private MAPIFolder _folder;
        private string _name;
        private List<AppointmentItem> _appointments;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlIgnore]
        public MAPIFolder Folder
        {
            get { return _folder; }
        }

        public OutlookCalendar()
        {
        }

        public OutlookCalendar(string name, MAPIFolder folder)
        {
            _folder = folder;
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }

        public List<AppointmentItem> GetAppointmentItemsInRange()
        {
            if (_appointments != null)
            {
                return _appointments;
            }

            List<AppointmentItem> result = new List<AppointmentItem>();
            
            if (_folder == null)
            {
                return result;
            }

            Items outlookItems = _folder.Items;
            outlookItems.Sort("[Start]",Type.Missing);
            outlookItems.IncludeRecurrences = true;
            
            if (outlookItems != null)
            {
                DateTime min = DateTime.Now.AddDays(-Settings.Instance.DaysInThePast);
                DateTime max = DateTime.Now.AddDays(+Settings.Instance.DaysInTheFuture+1);
                string filter = "[End] >= '" + min.ToString("g") + "' AND [Start] < '" + max.ToString("g") + "'";
                foreach(AppointmentItem ai in outlookItems.Restrict(filter))
                {
                    result.Add(ai);
                }
            }
            _appointments = result;
            return _appointments;
        }
    }
}
