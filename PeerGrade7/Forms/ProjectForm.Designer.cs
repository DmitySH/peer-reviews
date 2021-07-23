
namespace Forms
{
    partial class ProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.challengesTab = new System.Windows.Forms.TabControl();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.challengeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.addEpicTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.addStoryTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.addTaskTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.addBugTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeChallengeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.changeStatusTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toOpenedTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toInProgressTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toClosedTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.groupChallengesTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.openedTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.inProgressTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.closedTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // challengesTab
            // 
            this.challengesTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.challengesTab.Location = new System.Drawing.Point(0, 24);
            this.challengesTab.Name = "challengesTab";
            this.challengesTab.SelectedIndex = 0;
            this.challengesTab.Size = new System.Drawing.Size(800, 426);
            this.challengesTab.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.challengeTSMI,
            this.changeStatusTSMI,
            this.groupChallengesTSMI});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menu";
            // 
            // challengeTSMI
            // 
            this.challengeTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEpicTSMI,
            this.addStoryTSMI,
            this.addTaskTSMI,
            this.addBugTSMI,
            this.sep1,
            this.removeChallengeTSMI});
            this.challengeTSMI.Name = "challengeTSMI";
            this.challengeTSMI.Size = new System.Drawing.Size(115, 20);
            this.challengeTSMI.Text = "Project challenges";
            // 
            // addEpicTSMI
            // 
            this.addEpicTSMI.Name = "addEpicTSMI";
            this.addEpicTSMI.Size = new System.Drawing.Size(217, 22);
            this.addEpicTSMI.Text = "Add Epic";
            this.addEpicTSMI.Click += new System.EventHandler(this.addEpicTSMI_Click);
            // 
            // addStoryTSMI
            // 
            this.addStoryTSMI.Name = "addStoryTSMI";
            this.addStoryTSMI.Size = new System.Drawing.Size(217, 22);
            this.addStoryTSMI.Text = "Add Story";
            this.addStoryTSMI.Click += new System.EventHandler(this.addStoryTSMI_Click);
            // 
            // addTaskTSMI
            // 
            this.addTaskTSMI.Name = "addTaskTSMI";
            this.addTaskTSMI.Size = new System.Drawing.Size(217, 22);
            this.addTaskTSMI.Text = "Add Task";
            this.addTaskTSMI.Click += new System.EventHandler(this.addTaskTSMI_Click);
            // 
            // addBugTSMI
            // 
            this.addBugTSMI.Name = "addBugTSMI";
            this.addBugTSMI.Size = new System.Drawing.Size(217, 22);
            this.addBugTSMI.Text = "Add Bug";
            this.addBugTSMI.Click += new System.EventHandler(this.addBugTSMI_Click);
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(214, 6);
            // 
            // removeChallengeTSMI
            // 
            this.removeChallengeTSMI.Name = "removeChallengeTSMI";
            this.removeChallengeTSMI.Size = new System.Drawing.Size(217, 22);
            this.removeChallengeTSMI.Text = "Remove selected challenge";
            this.removeChallengeTSMI.Click += new System.EventHandler(this.removeChallengeTSMI_Click);
            // 
            // changeStatusTSMI
            // 
            this.changeStatusTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toOpenedTSMI,
            this.toInProgressTSMI,
            this.toClosedTSMI});
            this.changeStatusTSMI.Name = "changeStatusTSMI";
            this.changeStatusTSMI.Size = new System.Drawing.Size(208, 20);
            this.changeStatusTSMI.Text = "Change status of selected challenge";
            // 
            // toOpenedTSMI
            // 
            this.toOpenedTSMI.Name = "toOpenedTSMI";
            this.toOpenedTSMI.Size = new System.Drawing.Size(165, 22);
            this.toOpenedTSMI.Text = "Set to opened";
            this.toOpenedTSMI.Click += new System.EventHandler(this.toOpenedTSMI_Click);
            // 
            // toInProgressTSMI
            // 
            this.toInProgressTSMI.Name = "toInProgressTSMI";
            this.toInProgressTSMI.Size = new System.Drawing.Size(165, 22);
            this.toInProgressTSMI.Text = "Set to in progress";
            this.toInProgressTSMI.Click += new System.EventHandler(this.toInProgressTSMI_Click);
            // 
            // toClosedTSMI
            // 
            this.toClosedTSMI.Name = "toClosedTSMI";
            this.toClosedTSMI.Size = new System.Drawing.Size(165, 22);
            this.toClosedTSMI.Text = "Set to closed";
            this.toClosedTSMI.Click += new System.EventHandler(this.toClosedTSMI_Click);
            // 
            // groupChallengesTSMI
            // 
            this.groupChallengesTSMI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openedTSMI,
            this.inProgressTSMI,
            this.closedTSMI});
            this.groupChallengesTSMI.Name = "groupChallengesTSMI";
            this.groupChallengesTSMI.Size = new System.Drawing.Size(151, 20);
            this.groupChallengesTSMI.Text = "See groupped challenges";
            // 
            // openedTSMI
            // 
            this.openedTSMI.Name = "openedTSMI";
            this.openedTSMI.Size = new System.Drawing.Size(132, 22);
            this.openedTSMI.Text = "Opened";
            this.openedTSMI.Click += new System.EventHandler(this.openedTSMI_Click);
            // 
            // inProgressTSMI
            // 
            this.inProgressTSMI.Name = "inProgressTSMI";
            this.inProgressTSMI.Size = new System.Drawing.Size(132, 22);
            this.inProgressTSMI.Text = "In progress";
            this.inProgressTSMI.Click += new System.EventHandler(this.inProgressTSMI_Click);
            // 
            // closedTSMI
            // 
            this.closedTSMI.Name = "closedTSMI";
            this.closedTSMI.Size = new System.Drawing.Size(132, 22);
            this.closedTSMI.Text = "Closed";
            this.closedTSMI.Click += new System.EventHandler(this.closedTSMI_Click);
            // 
            // ProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.challengesTab);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "ProjectForm";
            this.Text = "ProjectForm";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TabControl challengesTab;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem challengeTSMI;
        private System.Windows.Forms.ToolStripMenuItem addEpicTSMI;
        private System.Windows.Forms.ToolStripMenuItem addStoryTSMI;
        private System.Windows.Forms.ToolStripMenuItem addTaskTSMI;
        private System.Windows.Forms.ToolStripMenuItem addBugTSMI;
        private System.Windows.Forms.ToolStripMenuItem changeStatusTSMI;
        private System.Windows.Forms.ToolStripMenuItem toOpenedTSMI;
        private System.Windows.Forms.ToolStripMenuItem toInProgressTSMI;
        private System.Windows.Forms.ToolStripMenuItem toClosedTSMI;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripMenuItem removeChallengeTSMI;
        private System.Windows.Forms.ToolStripMenuItem groupChallengesTSMI;
        private System.Windows.Forms.ToolStripMenuItem openedTSMI;
        private System.Windows.Forms.ToolStripMenuItem inProgressTSMI;
        private System.Windows.Forms.ToolStripMenuItem closedTSMI;
    }
}