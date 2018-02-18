/*
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonDeleteAllSyncItems = new System.Windows.Forms.Button();
            this.LogBox = new System.Windows.Forms.TextBox();
            this.bSyncNow = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBoxCalendars = new System.Windows.Forms.GroupBox();
            this.checkedListBoxCalendars = new System.Windows.Forms.CheckedListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbAddReminders = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.cbStartInTray = new System.Windows.Forms.CheckBox();
            this.cbCreateFiles = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbShowBubbleTooltips = new System.Windows.Forms.CheckBox();
            this.cbSyncEveryHour = new System.Windows.Forms.CheckBox();
            this.tbMinuteOffsets = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbDaysInTheFuture = new System.Windows.Forms.NumericUpDown();
            this.tbDaysInThePast = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.labelAbout = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBoxCalendars.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDaysInTheFuture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDaysInThePast)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(18, 18);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(742, 777);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonDeleteAllSyncItems);
            this.tabPage1.Controls.Add(this.LogBox);
            this.tabPage1.Controls.Add(this.bSyncNow);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(734, 744);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sync";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteAllSyncItems
            // 
            this.buttonDeleteAllSyncItems.Location = new System.Drawing.Point(491, 680);
            this.buttonDeleteAllSyncItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteAllSyncItems.Name = "buttonDeleteAllSyncItems";
            this.buttonDeleteAllSyncItems.Size = new System.Drawing.Size(233, 48);
            this.buttonDeleteAllSyncItems.TabIndex = 2;
            this.buttonDeleteAllSyncItems.Text = "Delete All Sync Items";
            this.buttonDeleteAllSyncItems.UseVisualStyleBackColor = true;
            this.buttonDeleteAllSyncItems.Click += new System.EventHandler(this.buttonDeleteAllSyncItems_Click);
            // 
            // LogBox
            // 
            this.LogBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogBox.Location = new System.Drawing.Point(10, 9);
            this.LogBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LogBox.MaxLength = 327670;
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(714, 659);
            this.LogBox.TabIndex = 1;
            // 
            // bSyncNow
            // 
            this.bSyncNow.Location = new System.Drawing.Point(6, 680);
            this.bSyncNow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSyncNow.Name = "bSyncNow";
            this.bSyncNow.Size = new System.Drawing.Size(147, 48);
            this.bSyncNow.TabIndex = 0;
            this.bSyncNow.Text = "Sync now";
            this.bSyncNow.UseVisualStyleBackColor = true;
            this.bSyncNow.Click += new System.EventHandler(this.SyncNow_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBoxCalendars);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.bSave);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(734, 744);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBoxCalendars
            // 
            this.groupBoxCalendars.Controls.Add(this.checkedListBoxCalendars);
            this.groupBoxCalendars.Location = new System.Drawing.Point(9, 9);
            this.groupBoxCalendars.Name = "groupBoxCalendars";
            this.groupBoxCalendars.Size = new System.Drawing.Size(712, 241);
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
            this.checkedListBoxCalendars.Size = new System.Drawing.Size(706, 216);
            this.checkedListBoxCalendars.TabIndex = 0;
            this.checkedListBoxCalendars.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxCalendars_ItemCheck);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbAddReminders);
            this.groupBox5.Location = new System.Drawing.Point(9, 399);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox5.Size = new System.Drawing.Size(712, 82);
            this.groupBox5.TabIndex = 15;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "When creating Sync Calendar Entries...   ";
            // 
            // cbAddReminders
            // 
            this.cbAddReminders.Location = new System.Drawing.Point(18, 29);
            this.cbAddReminders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAddReminders.Name = "cbAddReminders";
            this.cbAddReminders.Size = new System.Drawing.Size(208, 37);
            this.cbAddReminders.TabIndex = 8;
            this.cbAddReminders.Text = "Add Reminders";
            this.cbAddReminders.UseVisualStyleBackColor = true;
            this.cbAddReminders.CheckedChanged += new System.EventHandler(this.CbAddRemindersCheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbMinimizeToTray);
            this.groupBox4.Controls.Add(this.cbStartInTray);
            this.groupBox4.Controls.Add(this.cbCreateFiles);
            this.groupBox4.Location = new System.Drawing.Point(9, 491);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox4.Size = new System.Drawing.Size(712, 177);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Options";
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.Location = new System.Drawing.Point(18, 75);
            this.cbMinimizeToTray.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(156, 37);
            this.cbMinimizeToTray.TabIndex = 4;
            this.cbMinimizeToTray.Text = "Minimize to Tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            this.cbMinimizeToTray.CheckedChanged += new System.EventHandler(this.CbMinimizeToTrayCheckedChanged);
            // 
            // cbStartInTray
            // 
            this.cbStartInTray.Location = new System.Drawing.Point(18, 29);
            this.cbStartInTray.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbStartInTray.Name = "cbStartInTray";
            this.cbStartInTray.Size = new System.Drawing.Size(156, 37);
            this.cbStartInTray.TabIndex = 1;
            this.cbStartInTray.Text = "Start in Tray";
            this.cbStartInTray.UseVisualStyleBackColor = true;
            this.cbStartInTray.CheckedChanged += new System.EventHandler(this.CbStartInTrayCheckedChanged);
            // 
            // cbCreateFiles
            // 
            this.cbCreateFiles.Enabled = false;
            this.cbCreateFiles.Location = new System.Drawing.Point(18, 122);
            this.cbCreateFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCreateFiles.Name = "cbCreateFiles";
            this.cbCreateFiles.Size = new System.Drawing.Size(352, 37);
            this.cbCreateFiles.TabIndex = 7;
            this.cbCreateFiles.Text = "Create text files with found/identified entries";
            this.cbCreateFiles.UseVisualStyleBackColor = true;
            this.cbCreateFiles.CheckedChanged += new System.EventHandler(this.cbCreateFiles_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbShowBubbleTooltips);
            this.groupBox3.Controls.Add(this.cbSyncEveryHour);
            this.groupBox3.Controls.Add(this.tbMinuteOffsets);
            this.groupBox3.Location = new System.Drawing.Point(266, 258);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(456, 131);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sync Regularly";
            // 
            // cbShowBubbleTooltips
            // 
            this.cbShowBubbleTooltips.Location = new System.Drawing.Point(9, 75);
            this.cbShowBubbleTooltips.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbShowBubbleTooltips.Name = "cbShowBubbleTooltips";
            this.cbShowBubbleTooltips.Size = new System.Drawing.Size(388, 37);
            this.cbShowBubbleTooltips.TabIndex = 7;
            this.cbShowBubbleTooltips.Text = "Show Bubble Tooltip in Taskbar when Syncing";
            this.cbShowBubbleTooltips.UseVisualStyleBackColor = true;
            this.cbShowBubbleTooltips.CheckedChanged += new System.EventHandler(this.CbShowBubbleTooltipsCheckedChanged);
            // 
            // cbSyncEveryHour
            // 
            this.cbSyncEveryHour.Location = new System.Drawing.Point(9, 29);
            this.cbSyncEveryHour.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSyncEveryHour.Name = "cbSyncEveryHour";
            this.cbSyncEveryHour.Size = new System.Drawing.Size(332, 37);
            this.cbSyncEveryHour.TabIndex = 1;
            this.cbSyncEveryHour.Text = "Sync every hour at these Minute Offset(s)";
            this.cbSyncEveryHour.UseVisualStyleBackColor = true;
            this.cbSyncEveryHour.CheckedChanged += new System.EventHandler(this.CbSyncEveryHourCheckedChanged);
            // 
            // tbMinuteOffsets
            // 
            this.tbMinuteOffsets.Location = new System.Drawing.Point(346, 32);
            this.tbMinuteOffsets.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbMinuteOffsets.Name = "tbMinuteOffsets";
            this.tbMinuteOffsets.Size = new System.Drawing.Size(98, 26);
            this.tbMinuteOffsets.TabIndex = 5;
            this.tbMinuteOffsets.TextChanged += new System.EventHandler(this.TbMinuteOffsetsTextChanged);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(9, 680);
            this.bSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(112, 48);
            this.bSave.TabIndex = 8;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbDaysInTheFuture);
            this.groupBox1.Controls.Add(this.tbDaysInThePast);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 258);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(248, 131);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sync Date Range";
            // 
            // tbDaysInTheFuture
            // 
            this.tbDaysInTheFuture.Location = new System.Drawing.Point(158, 77);
            this.tbDaysInTheFuture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDaysInTheFuture.Name = "tbDaysInTheFuture";
            this.tbDaysInTheFuture.Size = new System.Drawing.Size(69, 26);
            this.tbDaysInTheFuture.TabIndex = 4;
            this.tbDaysInTheFuture.ValueChanged += new System.EventHandler(this.TbDaysInTheFutureTextChanged);
            // 
            // tbDaysInThePast
            // 
            this.tbDaysInThePast.Location = new System.Drawing.Point(158, 32);
            this.tbDaysInThePast.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDaysInThePast.Name = "tbDaysInThePast";
            this.tbDaysInThePast.Size = new System.Drawing.Size(69, 26);
            this.tbDaysInThePast.TabIndex = 3;
            this.tbDaysInThePast.ValueChanged += new System.EventHandler(this.TbDaysInThePastTextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "Days in the Future";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Days in the Past";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.labelAbout);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Size = new System.Drawing.Size(734, 744);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "About";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(8, 259);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(712, 35);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://github.com/David-Engel/OutlookCalendarSync";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1LinkClicked);
            // 
            // labelAbout
            // 
            this.labelAbout.Location = new System.Drawing.Point(4, 49);
            this.labelAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(722, 191);
            this.labelAbout.TabIndex = 1;
            this.labelAbout.Text = "OutlookCalendarSync\r\n\r\nVersion {version}\r\n\r\n2018 by David Engel\r\n\r\nCredit:\r\nOrigi" +
    "nally OutlookGoogleSync by Zissis Siantidis";
            this.labelAbout.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "OutlookCalendarSync";
            this.notifyIcon1.Click += new System.EventHandler(this.NotifyIcon1Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 814);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "OutlookCalendarSync";
            this.Resize += new System.EventHandler(this.MainFormResize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBoxCalendars.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbDaysInTheFuture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDaysInThePast)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.CheckBox cbAddReminders;
		private System.Windows.Forms.CheckBox cbShowBubbleTooltips;
		private System.Windows.Forms.CheckBox cbSyncEveryHour;
		private System.Windows.Forms.CheckBox cbMinimizeToTray;
		private System.Windows.Forms.CheckBox cbStartInTray;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TextBox tbMinuteOffsets;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label labelAbout;
		private System.Windows.Forms.CheckBox cbCreateFiles;
        private System.Windows.Forms.TextBox LogBox;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bSave;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button bSyncNow;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.NumericUpDown tbDaysInTheFuture;
        private System.Windows.Forms.NumericUpDown tbDaysInThePast;
        private System.Windows.Forms.GroupBox groupBoxCalendars;
        private System.Windows.Forms.CheckedListBox checkedListBoxCalendars;
        private System.Windows.Forms.Button buttonDeleteAllSyncItems;
		
	



		

		

	}
}
