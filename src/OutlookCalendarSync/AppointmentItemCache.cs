using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OutlookCalendarSync
{
    class AppointmentItemCache
    {
        private Dictionary<AppointmentItem, AppointmentItemCacheEntry> _cache = new Dictionary<AppointmentItem, AppointmentItemCacheEntry>();

        public AppointmentItemCache()
        {
        }

        public AppointmentItemCacheEntry GetAppointmentItemCacheEntry(AppointmentItem appointmentItem, string fromAccount)
        {
            if (!_cache.ContainsKey(appointmentItem))
            {
                _cache.Add(appointmentItem, new AppointmentItemCacheEntry(appointmentItem, fromAccount));
            }

            return _cache[appointmentItem];
        }

        public void ClearAndReleaseAll()
        {
            foreach (AppointmentItem ai in _cache.Keys)
            {
                Marshal.FinalReleaseComObject(ai);
            }

            _cache.Clear();
        }
    }
}
