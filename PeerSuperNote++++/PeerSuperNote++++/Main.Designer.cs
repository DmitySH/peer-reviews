
namespace PeerSuperNote____
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.file = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileTab = new System.Windows.Forms.ToolStripMenuItem();
            this.createFileWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileAs = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.closeApp = new System.Windows.Forms.ToolStripMenuItem();
            this.edit = new System.Windows.Forms.ToolStripMenuItem();
            this.format = new System.Windows.Forms.ToolStripMenuItem();
            this.italic = new System.Windows.Forms.ToolStripMenuItem();
            this.bold = new System.Windows.Forms.ToolStripMenuItem();
            this.underlined = new System.Windows.Forms.ToolStripMenuItem();
            this.crossed = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.settings = new System.Windows.Forms.ToolStripMenuItem();
            this.colorScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.autosave = new System.Windows.Forms.ToolStripMenuItem();
            this.noSave = new System.Windows.Forms.ToolStripMenuItem();
            this.sec15 = new System.Windows.Forms.ToolStripMenuItem();
            this.sec30 = new System.Windows.Forms.ToolStripMenuItem();
            this.sec60 = new System.Windows.Forms.ToolStripMenuItem();
            this.sec300 = new System.Windows.Forms.ToolStripMenuItem();
            this.sec600 = new System.Windows.Forms.ToolStripMenuItem();
            this.italicContext = new System.Windows.Forms.ToolStripMenuItem();
            this.boldContext = new System.Windows.Forms.ToolStripMenuItem();
            this.underlinedContext = new System.Windows.Forms.ToolStripMenuItem();
            this.crossedContext = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFormatContext = new System.Windows.Forms.ToolStripMenuItem();
            this.tabs = new System.Windows.Forms.TabControl();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.test = new System.Windows.Forms.Button();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file,
            this.edit,
            this.format,
            this.settings});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "Menu";
            // 
            // file
            // 
            this.file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createFileTab,
            this.createFileWindow,
            this.toolStripSeparator1,
            this.openFile,
            this.saveFile,
            this.saveFileAs,
            this.saveAllFiles,
            this.toolStripSeparator2,
            this.closeSelected,
            this.closeApp});
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(37, 20);
            this.file.Text = "File";
            // 
            // createFileTab
            // 
            this.createFileTab.Name = "createFileTab";
            this.createFileTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createFileTab.Size = new System.Drawing.Size(259, 22);
            this.createFileTab.Text = "Create new tab";
            this.createFileTab.Click += new System.EventHandler(this.createFileTab_Click);
            // 
            // createFileWindow
            // 
            this.createFileWindow.Name = "createFileWindow";
            this.createFileWindow.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.createFileWindow.Size = new System.Drawing.Size(259, 22);
            this.createFileWindow.Text = "Create new window";
            this.createFileWindow.Click += new System.EventHandler(this.createFileWindow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // openFile
            // 
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(259, 22);
            this.openFile.Text = "Open file";
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // saveFile
            // 
            this.saveFile.Name = "saveFile";
            this.saveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveFile.Size = new System.Drawing.Size(259, 22);
            this.saveFile.Text = "Save file";
            this.saveFile.Click += new System.EventHandler(this.saveFile_Click);
            // 
            // saveFileAs
            // 
            this.saveFileAs.Name = "saveFileAs";
            this.saveFileAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveFileAs.Size = new System.Drawing.Size(259, 22);
            this.saveFileAs.Text = "Save file as";
            this.saveFileAs.Click += new System.EventHandler(this.saveFileAs_Click);
            // 
            // saveAllFiles
            // 
            this.saveAllFiles.Name = "saveAllFiles";
            this.saveAllFiles.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.saveAllFiles.Size = new System.Drawing.Size(259, 22);
            this.saveAllFiles.Text = "Save all files";
            this.saveAllFiles.Click += new System.EventHandler(this.saveAllFiles_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(256, 6);
            // 
            // closeSelected
            // 
            this.closeSelected.Name = "closeSelected";
            this.closeSelected.Size = new System.Drawing.Size(259, 22);
            this.closeSelected.Text = "Close selected";
            this.closeSelected.Click += new System.EventHandler(this.closeSelected_Click);
            // 
            // closeApp
            // 
            this.closeApp.Name = "closeApp";
            this.closeApp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.closeApp.Size = new System.Drawing.Size(259, 22);
            this.closeApp.Text = "Close App";
            this.closeApp.Click += new System.EventHandler(this.closeApp_Click);
            // 
            // edit
            // 
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(39, 20);
            this.edit.Text = "Edit";
            // 
            // format
            // 
            this.format.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.italic,
            this.bold,
            this.underlined,
            this.crossed,
            this.toolStripSeparator3,
            this.removeFormat});
            this.format.Name = "format";
            this.format.Size = new System.Drawing.Size(57, 20);
            this.format.Text = "Format";
            this.format.Click += new System.EventHandler(this.format_Click);
            // 
            // italic
            // 
            this.italic.Name = "italic";
            this.italic.Size = new System.Drawing.Size(171, 22);
            this.italic.Text = "Italic";
            this.italic.Click += new System.EventHandler(this.italic_Click);
            // 
            // bold
            // 
            this.bold.Name = "bold";
            this.bold.Size = new System.Drawing.Size(171, 22);
            this.bold.Text = "Bold";
            this.bold.Click += new System.EventHandler(this.bold_Click);
            // 
            // underlined
            // 
            this.underlined.Name = "underlined";
            this.underlined.Size = new System.Drawing.Size(171, 22);
            this.underlined.Text = "Underlined";
            this.underlined.Click += new System.EventHandler(this.underlined_Click);
            // 
            // crossed
            // 
            this.crossed.Name = "crossed";
            this.crossed.Size = new System.Drawing.Size(171, 22);
            this.crossed.Text = "Crossed";
            this.crossed.Click += new System.EventHandler(this.crossed_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(168, 6);
            // 
            // removeFormat
            // 
            this.removeFormat.Name = "removeFormat";
            this.removeFormat.Size = new System.Drawing.Size(171, 22);
            this.removeFormat.Text = "Remove all format";
            this.removeFormat.Click += new System.EventHandler(this.removeFormat_Click);
            // 
            // settings
            // 
            this.settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorScheme,
            this.autosave});
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(61, 20);
            this.settings.Text = "Settings";
            // 
            // colorScheme
            // 
            this.colorScheme.Name = "colorScheme";
            this.colorScheme.Size = new System.Drawing.Size(147, 22);
            this.colorScheme.Text = "Color scheme";
            this.colorScheme.Click += new System.EventHandler(this.colorScheme_Click);
            // 
            // autosave
            // 
            this.autosave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noSave,
            this.sec15,
            this.sec30,
            this.sec60,
            this.sec300,
            this.sec600});
            this.autosave.Name = "autosave";
            this.autosave.Size = new System.Drawing.Size(147, 22);
            this.autosave.Text = "Autosave";
            this.autosave.Click += new System.EventHandler(this.autosave_Click);
            // 
            // noSave
            // 
            this.noSave.Name = "noSave";
            this.noSave.Size = new System.Drawing.Size(140, 22);
            this.noSave.Text = "No autosave";
            this.noSave.Click += new System.EventHandler(this.noSave_Click);
            // 
            // sec15
            // 
            this.sec15.Name = "sec15";
            this.sec15.Size = new System.Drawing.Size(140, 22);
            this.sec15.Text = "15 seconds";
            this.sec15.Click += new System.EventHandler(this.sec15_Click);
            // 
            // sec30
            // 
            this.sec30.Name = "sec30";
            this.sec30.Size = new System.Drawing.Size(140, 22);
            this.sec30.Text = "30 seconds";
            this.sec30.Click += new System.EventHandler(this.sec30_Click);
            // 
            // sec60
            // 
            this.sec60.Name = "sec60";
            this.sec60.Size = new System.Drawing.Size(140, 22);
            this.sec60.Text = "1 minute";
            this.sec60.Click += new System.EventHandler(this.sec60_Click);
            // 
            // sec300
            // 
            this.sec300.Name = "sec300";
            this.sec300.Size = new System.Drawing.Size(140, 22);
            this.sec300.Text = "5 minutes";
            this.sec300.Click += new System.EventHandler(this.sec300_Click);
            // 
            // sec600
            // 
            this.sec600.Name = "sec600";
            this.sec600.Size = new System.Drawing.Size(140, 22);
            this.sec600.Text = "10 minutes";
            this.sec600.Click += new System.EventHandler(this.sec600_Click);
            // 
            // italicContext
            // 
            this.italicContext.Name = "italicContext";
            this.italicContext.Size = new System.Drawing.Size(171, 22);
            this.italicContext.Text = "Italic";
            this.italicContext.Click += new System.EventHandler(this.italic_Click);
            // 
            // boldContext
            // 
            this.boldContext.Name = "boldContext";
            this.boldContext.Size = new System.Drawing.Size(171, 22);
            this.boldContext.Text = "Bold";
            this.boldContext.Click += new System.EventHandler(this.bold_Click);
            // 
            // underlinedContext
            // 
            this.underlinedContext.Name = "underlinedContext";
            this.underlinedContext.Size = new System.Drawing.Size(171, 22);
            this.underlinedContext.Text = "Underlined";
            this.underlinedContext.Click += new System.EventHandler(this.underlined_Click);
            // 
            // crossedContext
            // 
            this.crossedContext.Name = "crossedContext";
            this.crossedContext.Size = new System.Drawing.Size(171, 22);
            this.crossedContext.Text = "Crossed";
            this.crossedContext.Click += new System.EventHandler(this.crossed_Click);
            // 
            // removeFormatContext
            // 
            this.removeFormatContext.Name = "removeFormatContext";
            this.removeFormatContext.Size = new System.Drawing.Size(171, 22);
            this.removeFormatContext.Text = "Remove all format";
            this.removeFormatContext.Click += new System.EventHandler(this.removeFormat_Click);
            // 
            // tabs
            // 
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 24);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(800, 426);
            this.tabs.TabIndex = 1;
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(533, 0);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 2;
            this.test.Text = "TEST";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // timer
            // 
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.test);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.menu);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem file;
        private System.Windows.Forms.ToolStripMenuItem edit;
        private System.Windows.Forms.ToolStripMenuItem format;
        private System.Windows.Forms.ToolStripMenuItem settings;
        private System.Windows.Forms.ToolStripMenuItem createFileTab;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.ToolStripMenuItem closeSelected;
        private System.Windows.Forms.ToolStripMenuItem openFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem saveFile;
        private System.Windows.Forms.ToolStripMenuItem createFileWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        //
        private System.Windows.Forms.ToolStripMenuItem italic;
        private System.Windows.Forms.ToolStripMenuItem bold;
        private System.Windows.Forms.ToolStripMenuItem underlined;
        private System.Windows.Forms.ToolStripMenuItem crossed;
        private System.Windows.Forms.ToolStripMenuItem removeFormat;
        //
        private System.Windows.Forms.ToolStripMenuItem italicContext;
        private System.Windows.Forms.ToolStripMenuItem boldContext;
        private System.Windows.Forms.ToolStripMenuItem underlinedContext;
        private System.Windows.Forms.ToolStripMenuItem crossedContext;
        private System.Windows.Forms.ToolStripMenuItem removeFormatContext;
        //
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem colorScheme;
        private System.Windows.Forms.ToolStripMenuItem saveFileAs;
        private System.Windows.Forms.ToolStripMenuItem saveAllFiles;
        private System.Windows.Forms.ToolStripMenuItem closeApp;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem autosave;
        private System.Windows.Forms.ToolStripMenuItem noSave;
        private System.Windows.Forms.ToolStripMenuItem sec15;
        private System.Windows.Forms.ToolStripMenuItem sec30;
        private System.Windows.Forms.ToolStripMenuItem sec60;
        private System.Windows.Forms.ToolStripMenuItem sec300;
        private System.Windows.Forms.ToolStripMenuItem sec600;
    }
}

