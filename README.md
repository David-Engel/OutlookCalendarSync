# OutlookCalendarSync

A simple utility to create synced calendar items between multiple Outlook calendars. If you maintain more than one personal Calendar for multiple orgs, this can help you keep your free/busy time correct for people in each org.

This was forked from my Outlook Google Calendar Sync utility for which the original source came from a codeplex project by Zissis Siantidis many years ago and I have modified and extended it to suit my needs.

# Instructions
There is no installer currently. Right now you have to build the source yourself using Visual Studio 2017. You can simply copy the resulting EXE and DLLs to any folder and run the EXE.

Settings are hopefully mostly self-explanatory:
![Settings](img/Settings.png)

**Calendars to Sync**

Check (double-click) all the calendars you want to sync (at least two).

**Sync Date Range**

Set the range of days you want to gather appointments in each calendar to sync. If you want it to sync hourly, check the box and set the desired minute of each hour you want it to run.

For each calendar checked, all appointment items will be read within the date range specified and for every other calendar checked, a copy of the appointment item will be created with most details preserved. The organizer will be you on copies. Reminders will only be copied if you ckecked the Add Reminders option, otherwise they will be set to None. The Subject will be prefixed with (c) to make it easy to identify the original appointment from copies when looking at your calendars in Outlook.

**Save**

Settings changes take effect immediately in the current session. To preserve Settings the next time the application is run, use the Save button to write settings to Settings.xml in the same folder as the EXE. Those settings will be loaded the next time the application is run.
