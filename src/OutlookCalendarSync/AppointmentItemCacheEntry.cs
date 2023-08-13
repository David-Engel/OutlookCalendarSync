using Microsoft.Office.Interop.Outlook;

namespace OutlookCalendarSync
{
    public class AppointmentItemCacheEntry
    {
        private readonly AppointmentItem _appointmentItem;
        private readonly bool _isSyncItem = false;
        private readonly string _fromAccount;
        private readonly string _signature;

        public AppointmentItemCacheEntry(AppointmentItem appointmentItem, string fromAccount)
        {
            _appointmentItem = appointmentItem;
            _fromAccount = fromAccount;
            if (appointmentItem.UserProperties != null)
            {
                var flag = appointmentItem.UserProperties.Find(MainForm.USER_PROPERTY_NAME);
                if (flag != null)
                {
                    _isSyncItem = true;
                    _fromAccount = flag.Value as string;
                }
            }

            if (_isSyncItem)
            {
                _signature = (_appointmentItem.Start + ";" + _appointmentItem.End + ";" +
                    _appointmentItem.Subject.Substring(MainForm.SUBJECT_PREFIX.Length) + ";" +
                    _appointmentItem.Location).Trim() + ";" + _appointmentItem.BusyStatus + ";" +
                    _appointmentItem.Sensitivity;
            }
            else
            {
                _signature = (_appointmentItem.Start + ";" + _appointmentItem.End + ";" +
                    _appointmentItem.Subject + ";" + _appointmentItem.Location).Trim() +
                    ";" + _appointmentItem.BusyStatus + ";" + _appointmentItem.Sensitivity;
            }
        }

        public AppointmentItem AppointmentItem
        {
            get { return _appointmentItem; }
        }

        public bool IsSyncItem
        {
            get { return _isSyncItem; }
        }

        public string FromAccount
        {
            get { return _fromAccount; }
        }

        public string Signature
        {
            get { return _signature; }
        }
    }
}
