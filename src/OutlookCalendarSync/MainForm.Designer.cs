﻿/*
 * Created by SharpDevelop.
 * User: zsianti
 * Date: 14.08.2012
 * Time: 07:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OutlookCalendarSync
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSync = new System.Windows.Forms.TabPage();
            this.buttonDeleteAllSyncItems = new System.Windows.Forms.Button();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.buttonSyncNow = new System.Windows.Forms.Button();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.groupBoxCalendars = new System.Windows.Forms.GroupBox();
            this.checkedListBoxCalendars = new System.Windows.Forms.CheckedListBox();
            this.groupBoxAddReminders = new System.Windows.Forms.GroupBox();
            this.checkBoxAddReminders = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.checkBoxStartInTray = new System.Windows.Forms.CheckBox();
            this.checkBoxCreateFiles = new System.Windows.Forms.CheckBox();
            this.groupBoxSyncRegularly = new System.Windows.Forms.GroupBox();
            this.checkBoxShowBubbleTooltips = new System.Windows.Forms.CheckBox();
            this.checkBoxSyncEveryHour = new System.Windows.Forms.CheckBox();
            this.textBoxMinuteOffsets = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxSyncDateRange = new System.Windows.Forms.GroupBox();
            this.numericUpDownDaysInTheFuture = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDaysInThePast = new System.Windows.Forms.NumericUpDown();
            this.labelDaysInTheFuture = new System.Windows.Forms.Label();
            this.labelDaysInThePast = new System.Windows.Forms.Label();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.linkLabelWebsite = new System.Windows.Forms.LinkLabel();
            this.labelAbout = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageSync.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBoxCalendars.SuspendLayout();
            this.groupBoxAddReminders.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.groupBoxSyncRegularly.SuspendLayout();
            this.groupBoxSyncDateRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDaysInTheFuture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDaysInThePast)).BeginInit();
            this.tabPageAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSync);
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(778, 814);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageSync
            // 
            this.tabPageSync.Controls.Add(this.buttonDeleteAllSyncItems);
            this.tabPageSync.Controls.Add(this.textBoxLogs);
            this.tabPageSync.Controls.Add(this.buttonSyncNow);
            this.tabPageSync.Location = new System.Drawing.Point(4, 29);
            this.tabPageSync.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSync.Name = "tabPageSync";
            this.tabPageSync.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSync.Size = new System.Drawing.Size(770, 781);
            this.tabPageSync.TabIndex = 0;
            this.tabPageSync.Text = "Sync";
            this.tabPageSync.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteAllSyncItems
            // 
            this.buttonDeleteAllSyncItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteAllSyncItems.Location = new System.Drawing.Point(522, 714);
            this.buttonDeleteAllSyncItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteAllSyncItems.Name = "buttonDeleteAllSyncItems";
            this.buttonDeleteAllSyncItems.Size = new System.Drawing.Size(232, 48);
            this.buttonDeleteAllSyncItems.TabIndex = 2;
            this.buttonDeleteAllSyncItems.Text = "Delete All Sync Items";
            this.toolTip1.SetToolTip(this.buttonDeleteAllSyncItems, "Delete all sync calendar items which were created by this utility on the selected" +
        " Calendars falling within the day range defined.");
            this.buttonDeleteAllSyncItems.UseVisualStyleBackColor = true;
            this.buttonDeleteAllSyncItems.Click += new System.EventHandler(this.buttonDeleteAllSyncItems_Click);
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogs.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogs.Location = new System.Drawing.Point(10, 9);
            this.textBoxLogs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxLogs.MaxLength = 327670;
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(742, 693);
            this.textBoxLogs.TabIndex = 1;
            // 
            // buttonSyncNow
            // 
            this.buttonSyncNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSyncNow.Location = new System.Drawing.Point(9, 714);
            this.buttonSyncNow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSyncNow.Name = "buttonSyncNow";
            this.buttonSyncNow.Size = new System.Drawing.Size(147, 48);
            this.buttonSyncNow.TabIndex = 0;
            this.buttonSyncNow.Text = "Sync now";
            this.buttonSyncNow.UseVisualStyleBackColor = true;
            this.buttonSyncNow.Click += new System.EventHandler(this.SyncNow_Click);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.groupBoxCalendars);
            this.tabPageSettings.Controls.Add(this.groupBoxAddReminders);
            this.tabPageSettings.Controls.Add(this.groupBoxOptions);
            this.tabPageSettings.Controls.Add(this.groupBoxSyncRegularly);
            this.tabPageSettings.Controls.Add(this.buttonSave);
            this.tabPageSettings.Controls.Add(this.groupBoxSyncDateRange);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 29);
            this.tabPageSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageSettings.Size = new System.Drawing.Size(770, 781);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // groupBoxCalendars
            // 
            this.groupBoxCalendars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCalendars.Controls.Add(this.checkedListBoxCalendars);
            this.groupBoxCalendars.Location = new System.Drawing.Point(9, 9);
            this.groupBoxCalendars.Name = "groupBoxCalendars";
            this.groupBoxCalendars.Size = new System.Drawing.Size(723, 208);
            this.groupBoxCalendars.TabIndex = 0;
            this.groupBoxCalendars.TabStop = false;
            this.groupBoxCalendars.Text = "Calendars to Sync";
            // 
            // checkedListBoxCalendars
            // 
            this.checkedListBoxCalendars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxCalendars.FormattingEnabled = true;
            this.checkedListBoxCalendars.Location = new System.Drawing.Point(3, 22);
            this.checkedListBoxCalendars.Name = "checkedListBoxCalendars";
            this.checkedListBoxCalendars.Size = new System.Drawing.Size(717, 183);
            this.checkedListBoxCalendars.TabIndex = 0;
            this.checkedListBoxCalendars.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxCalendars_ItemCheck);
            // 
            // groupBoxAddReminders
            // 
            this.groupBoxAddReminders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAddReminders.Controls.Add(this.checkBoxAddReminders);
            this.groupBoxAddReminders.Location = new System.Drawing.Point(9, 365);
            this.groupBoxAddReminders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxAddReminders.Name = "groupBoxAddReminders";
            this.groupBoxAddReminders.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxAddReminders.Size = new System.Drawing.Size(723, 82);
            this.groupBoxAddReminders.TabIndex = 15;
            this.groupBoxAddReminders.TabStop = false;
            this.groupBoxAddReminders.Text = "When creating Sync Calendar Entries...   ";
            // 
            // checkBoxAddReminders
            // 
            this.checkBoxAddReminders.Location = new System.Drawing.Point(18, 29);
            this.checkBoxAddReminders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAddReminders.Name = "checkBoxAddReminders";
            this.checkBoxAddReminders.Size = new System.Drawing.Size(208, 37);
            this.checkBoxAddReminders.TabIndex = 8;
            this.checkBoxAddReminders.Text = "Add Reminders";
            this.toolTip1.SetToolTip(this.checkBoxAddReminders, "If checked, the reminder will be duplicated across calendars.");
            this.checkBoxAddReminders.UseVisualStyleBackColor = true;
            this.checkBoxAddReminders.CheckedChanged += new System.EventHandler(this.checkBoxAddReminders_CheckedChanged);
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOptions.Controls.Add(this.checkBoxMinimizeToTray);
            this.groupBoxOptions.Controls.Add(this.checkBoxStartInTray);
            this.groupBoxOptions.Controls.Add(this.checkBoxCreateFiles);
            this.groupBoxOptions.Location = new System.Drawing.Point(9, 457);
            this.groupBoxOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxOptions.Size = new System.Drawing.Size(723, 177);
            this.groupBoxOptions.TabIndex = 20;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Options";
            // 
            // checkBoxMinimizeToTray
            // 
            this.checkBoxMinimizeToTray.Location = new System.Drawing.Point(18, 75);
            this.checkBoxMinimizeToTray.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxMinimizeToTray.Name = "checkBoxMinimizeToTray";
            this.checkBoxMinimizeToTray.Size = new System.Drawing.Size(156, 37);
            this.checkBoxMinimizeToTray.TabIndex = 4;
            this.checkBoxMinimizeToTray.Text = "Minimize to Tray";
            this.checkBoxMinimizeToTray.UseVisualStyleBackColor = true;
            this.checkBoxMinimizeToTray.CheckedChanged += new System.EventHandler(this.checkBoxMinimizeToTray_CheckedChanged);
            // 
            // checkBoxStartInTray
            // 
            this.checkBoxStartInTray.Location = new System.Drawing.Point(18, 29);
            this.checkBoxStartInTray.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxStartInTray.Name = "checkBoxStartInTray";
            this.checkBoxStartInTray.Size = new System.Drawing.Size(156, 37);
            this.checkBoxStartInTray.TabIndex = 1;
            this.checkBoxStartInTray.Text = "Start in Tray";
            this.checkBoxStartInTray.UseVisualStyleBackColor = true;
            this.checkBoxStartInTray.CheckedChanged += new System.EventHandler(this.checkBoxStartInTray_CheckedChanged);
            // 
            // checkBoxCreateFiles
            // 
            this.checkBoxCreateFiles.Enabled = false;
            this.checkBoxCreateFiles.Location = new System.Drawing.Point(18, 122);
            this.checkBoxCreateFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxCreateFiles.Name = "checkBoxCreateFiles";
            this.checkBoxCreateFiles.Size = new System.Drawing.Size(352, 37);
            this.checkBoxCreateFiles.TabIndex = 7;
            this.checkBoxCreateFiles.Text = "Create text files with found/identified entries";
            this.toolTip1.SetToolTip(this.checkBoxCreateFiles, resources.GetString("checkBoxCreateFiles.ToolTip"));
            this.checkBoxCreateFiles.UseVisualStyleBackColor = true;
            this.checkBoxCreateFiles.CheckedChanged += new System.EventHandler(this.checkBoxCreateFiles_CheckedChanged);
            // 
            // groupBoxSyncRegularly
            // 
            this.groupBoxSyncRegularly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSyncRegularly.Controls.Add(this.checkBoxShowBubbleTooltips);
            this.groupBoxSyncRegularly.Controls.Add(this.checkBoxSyncEveryHour);
            this.groupBoxSyncRegularly.Controls.Add(this.textBoxMinuteOffsets);
            this.groupBoxSyncRegularly.Location = new System.Drawing.Point(266, 225);
            this.groupBoxSyncRegularly.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxSyncRegularly.Name = "groupBoxSyncRegularly";
            this.groupBoxSyncRegularly.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxSyncRegularly.Size = new System.Drawing.Size(466, 131);
            this.groupBoxSyncRegularly.TabIndex = 10;
            this.groupBoxSyncRegularly.TabStop = false;
            this.groupBoxSyncRegularly.Text = "Sync Regularly";
            // 
            // checkBoxShowBubbleTooltips
            // 
            this.checkBoxShowBubbleTooltips.Location = new System.Drawing.Point(9, 75);
            this.checkBoxShowBubbleTooltips.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxShowBubbleTooltips.Name = "checkBoxShowBubbleTooltips";
            this.checkBoxShowBubbleTooltips.Size = new System.Drawing.Size(388, 37);
            this.checkBoxShowBubbleTooltips.TabIndex = 7;
            this.checkBoxShowBubbleTooltips.Text = "Show Bubble Tooltip in Taskbar when Syncing";
            this.checkBoxShowBubbleTooltips.UseVisualStyleBackColor = true;
            this.checkBoxShowBubbleTooltips.CheckedChanged += new System.EventHandler(this.checkBoxShowBubbleTooltips_CheckedChanged);
            // 
            // checkBoxSyncEveryHour
            // 
            this.checkBoxSyncEveryHour.Location = new System.Drawing.Point(9, 29);
            this.checkBoxSyncEveryHour.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxSyncEveryHour.Name = "checkBoxSyncEveryHour";
            this.checkBoxSyncEveryHour.Size = new System.Drawing.Size(332, 37);
            this.checkBoxSyncEveryHour.TabIndex = 1;
            this.checkBoxSyncEveryHour.Text = "Sync every hour at these Minute Offset(s)";
            this.checkBoxSyncEveryHour.UseVisualStyleBackColor = true;
            this.checkBoxSyncEveryHour.CheckedChanged += new System.EventHandler(this.checkBoxSyncEveryHour_CheckedChanged);
            // 
            // textBoxMinuteOffsets
            // 
            this.textBoxMinuteOffsets.Location = new System.Drawing.Point(346, 32);
            this.textBoxMinuteOffsets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMinuteOffsets.Name = "textBoxMinuteOffsets";
            this.textBoxMinuteOffsets.Size = new System.Drawing.Size(98, 26);
            this.textBoxMinuteOffsets.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBoxMinuteOffsets, "One ore more Minute Offsets at which the sync is automatically started each hour." +
        "\r\nSeparate multiple values by commas (e.g. 5,15,25).");
            this.textBoxMinuteOffsets.TextChanged += new System.EventHandler(this.textBoxMinuteOffsets_TextChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(9, 646);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 48);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // groupBoxSyncDateRange
            // 
            this.groupBoxSyncDateRange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxSyncDateRange.Controls.Add(this.numericUpDownDaysInTheFuture);
            this.groupBoxSyncDateRange.Controls.Add(this.numericUpDownDaysInThePast);
            this.groupBoxSyncDateRange.Controls.Add(this.labelDaysInTheFuture);
            this.groupBoxSyncDateRange.Controls.Add(this.labelDaysInThePast);
            this.groupBoxSyncDateRange.Location = new System.Drawing.Point(9, 225);
            this.groupBoxSyncDateRange.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxSyncDateRange.Name = "groupBoxSyncDateRange";
            this.groupBoxSyncDateRange.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxSyncDateRange.Size = new System.Drawing.Size(248, 131);
            this.groupBoxSyncDateRange.TabIndex = 5;
            this.groupBoxSyncDateRange.TabStop = false;
            this.groupBoxSyncDateRange.Text = "Sync Date Range";
            // 
            // numericUpDownDaysInTheFuture
            // 
            this.numericUpDownDaysInTheFuture.Location = new System.Drawing.Point(158, 77);
            this.numericUpDownDaysInTheFuture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownDaysInTheFuture.Name = "numericUpDownDaysInTheFuture";
            this.numericUpDownDaysInTheFuture.Size = new System.Drawing.Size(69, 26);
            this.numericUpDownDaysInTheFuture.TabIndex = 4;
            this.numericUpDownDaysInTheFuture.ValueChanged += new System.EventHandler(this.numericUpDownDaysInTheFuture_ValueChanged);
            // 
            // numericUpDownDaysInThePast
            // 
            this.numericUpDownDaysInThePast.Location = new System.Drawing.Point(158, 32);
            this.numericUpDownDaysInThePast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numericUpDownDaysInThePast.Name = "numericUpDownDaysInThePast";
            this.numericUpDownDaysInThePast.Size = new System.Drawing.Size(69, 26);
            this.numericUpDownDaysInThePast.TabIndex = 3;
            this.numericUpDownDaysInThePast.ValueChanged += new System.EventHandler(this.numericUpDownDaysInThePast_ValueChanged);
            // 
            // labelDaysInTheFuture
            // 
            this.labelDaysInTheFuture.Location = new System.Drawing.Point(9, 83);
            this.labelDaysInTheFuture.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDaysInTheFuture.Name = "labelDaysInTheFuture";
            this.labelDaysInTheFuture.Size = new System.Drawing.Size(150, 35);
            this.labelDaysInTheFuture.TabIndex = 0;
            this.labelDaysInTheFuture.Text = "Days in the Future";
            // 
            // labelDaysInThePast
            // 
            this.labelDaysInThePast.Location = new System.Drawing.Point(9, 37);
            this.labelDaysInThePast.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDaysInThePast.Name = "labelDaysInThePast";
            this.labelDaysInThePast.Size = new System.Drawing.Size(150, 35);
            this.labelDaysInThePast.TabIndex = 0;
            this.labelDaysInThePast.Text = "Days in the Past";
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.linkLabelWebsite);
            this.tabPageAbout.Controls.Add(this.labelAbout);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 29);
            this.tabPageAbout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageAbout.Size = new System.Drawing.Size(770, 781);
            this.tabPageAbout.TabIndex = 2;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // linkLabelWebsite
            // 
            this.linkLabelWebsite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelWebsite.Location = new System.Drawing.Point(0, 258);
            this.linkLabelWebsite.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelWebsite.Name = "linkLabelWebsite";
            this.linkLabelWebsite.Size = new System.Drawing.Size(736, 35);
            this.linkLabelWebsite.TabIndex = 2;
            this.linkLabelWebsite.TabStop = true;
            this.linkLabelWebsite.Text = "http://github.com/David-Engel/OutlookCalendarSync";
            this.linkLabelWebsite.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabelWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWebsite_LinkClicked);
            // 
            // labelAbout
            // 
            this.labelAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAbout.Location = new System.Drawing.Point(4, 49);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(732, 191);
            this.labelAbout.TabIndex = 1;
            this.labelAbout.Text = "David\'s Outlook Calendar Sync\r\n\r\nVersion {version}\r\n\r\n2018 by David Engel\r\n\r\nCred" +
    "it:\r\nOriginally OutlookGoogleSync by Zissis Siantidis";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "OutlookCalendarSync";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 814);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(766, 690);
            this.Name = "MainForm";
            this.Text = "David\'s Outlook Calendar Sync";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resized);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSync.ResumeLayout(false);
            this.tabPageSync.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.groupBoxCalendars.ResumeLayout(false);
            this.groupBoxAddReminders.ResumeLayout(false);
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxSyncRegularly.ResumeLayout(false);
            this.groupBoxSyncRegularly.PerformLayout();
            this.groupBoxSyncDateRange.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDaysInTheFuture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDaysInThePast)).EndInit();
            this.tabPageAbout.ResumeLayout(false);
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.CheckBox checkBoxAddReminders;
		private System.Windows.Forms.CheckBox checkBoxShowBubbleTooltips;
		private System.Windows.Forms.CheckBox checkBoxSyncEveryHour;
		private System.Windows.Forms.CheckBox checkBoxMinimizeToTray;
		private System.Windows.Forms.CheckBox checkBoxStartInTray;
		private System.Windows.Forms.GroupBox groupBoxOptions;
		private System.Windows.Forms.GroupBox groupBoxAddReminders;
		private System.Windows.Forms.LinkLabel linkLabelWebsite;
		private System.Windows.Forms.TabPage tabPageAbout;
		private System.Windows.Forms.TextBox textBoxMinuteOffsets;
		private System.Windows.Forms.GroupBox groupBoxSyncRegularly;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label labelAbout;
		private System.Windows.Forms.CheckBox checkBoxCreateFiles;
        private System.Windows.Forms.TextBox textBoxLogs;
		private System.Windows.Forms.Label labelDaysInThePast;
        private System.Windows.Forms.Label labelDaysInTheFuture;
		private System.Windows.Forms.GroupBox groupBoxSyncDateRange;
        private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.Button buttonSyncNow;
		private System.Windows.Forms.TabPage tabPageSync;
		private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.NumericUpDown numericUpDownDaysInTheFuture;
        private System.Windows.Forms.NumericUpDown numericUpDownDaysInThePast;
        private System.Windows.Forms.GroupBox groupBoxCalendars;
        private System.Windows.Forms.CheckedListBox checkedListBoxCalendars;
        private System.Windows.Forms.Button buttonDeleteAllSyncItems;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
