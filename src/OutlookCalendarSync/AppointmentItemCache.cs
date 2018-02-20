using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;

namespace OutlookCalendarSync
{
    class AppointmentItemCache
    {
        private Dictionary<AppointmentItem, AppointmentItemCacheEntry> _cache = new Dictionary<AppointmentItem, AppointmentItemCacheEntry>();

        public AppointmentItemCache()
        {
        }

        public AppointmentItemCacheEntry getAppointmentItemCacheEntry(AppointmentItem appointmentItem, string fromAccount)
        {
            if (!_cache.ContainsKey(appointmentItem))
            {
                _cache.Add(appointmentItem, new AppointmentItemCacheEntry(appointmentItem, fromAccount));
            }

            return _cache[appointmentItem];
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
