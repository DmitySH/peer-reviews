
namespace Forms
{
    partial class TaskManagerForm
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
            this.userList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.registerUserButton = new System.Windows.Forms.Button();
            this.labelUsers = new System.Windows.Forms.Label();
            this.removeUserButton = new System.Windows.Forms.Button();
            this.projectList = new System.Windows.Forms.ListBox();
            this.labelProjects = new System.Windows.Forms.Label();
            this.removeProjectButton = new System.Windows.Forms.Button();
            this.addProjectButton = new System.Windows.Forms.Button();
            this.renameProjectButton = new System.Windows.Forms.Button();
            this.newProjectName = new System.Windows.Forms.TextBox();
            this.openProjectButton = new System.Windows.Forms.Button();
            this.openWorkspaceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userList
            // 
            this.userList.FormattingEnabled = true;
            this.userList.ItemHeight = 20;
            this.userList.Location = new System.Drawing.Point(3, 31);
            this.userList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userList.Name = "userList";
            this.userList.Size = new System.Drawing.Size(202, 384);
            this.userList.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // registerUserButton
            // 
            this.registerUserButton.Location = new System.Drawing.Point(3, 439);
            this.registerUserButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.registerUserButton.Name = "registerUserButton";
            this.registerUserButton.Size = new System.Drawing.Size(202, 31);
            this.registerUserButton.TabIndex = 2;
            this.registerUserButton.Text = "Register new user";
            this.registerUserButton.UseVisualStyleBackColor = true;
            this.registerUserButton.Click += new System.EventHandler(this.registerUser_Click);
            // 
            // labelUsers
            // 
            this.labelUsers.AutoSize = true;
            this.labelUsers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelUsers.Location = new System.Drawing.Point(25, -1);
            this.labelUsers.Name = "labelUsers";
            this.labelUsers.Size = new System.Drawing.Size(153, 28);
            this.labelUsers.TabIndex = 4;
            this.labelUsers.Text = "Registered users";
            // 
            // removeUserButton
            // 
            this.removeUserButton.Location = new System.Drawing.Point(3, 477);
            this.removeUserButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.removeUserButton.Name = "removeUserButton";
            this.removeUserButton.Size = new System.Drawing.Size(202, 31);
            this.removeUserButton.TabIndex = 5;
            this.removeUserButton.Text = "Remove selected user";
            this.removeUserButton.UseVisualStyleBackColor = true;
            this.removeUserButton.Click += new System.EventHandler(this.removeUser_Click);
            // 
            // projectList
            // 
            this.projectList.FormattingEnabled = true;
            this.projectList.ItemHeight = 20;
            this.projectList.Location = new System.Drawing.Point(288, 31);
            this.projectList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.projectList.Name = "projectList";
            this.projectList.Size = new System.Drawing.Size(219, 384);
            this.projectList.TabIndex = 7;
            // 
            // labelProjects
            // 
            this.labelProjects.AutoSize = true;
            this.labelProjects.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelProjects.Location = new System.Drawing.Point(327, 0);
            this.labelProjects.Name = "labelProjects";
            this.labelProjects.Size = new System.Drawing.Size(151, 28);
            this.labelProjects.TabIndex = 8;
            this.labelProjects.Text = "Current Projects";
            // 
            // removeProjectButton
            // 
            this.removeProjectButton.Location = new System.Drawing.Point(288, 477);
            this.removeProjectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.removeProjectButton.Name = "removeProjectButton";
            this.removeProjectButton.Size = new System.Drawing.Size(219, 31);
            this.removeProjectButton.TabIndex = 10;
            this.removeProjectButton.Text = "Remove selected project";
            this.removeProjectButton.UseVisualStyleBackColor = true;
            this.removeProjectButton.Click += new System.EventHandler(this.removeProjectButton_Click);
            // 
            // addProjectButton
            // 
            this.addProjectButton.Location = new System.Drawing.Point(288, 439);
            this.addProjectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addProjectButton.Name = "addProjectButton";
            this.addProjectButton.Size = new System.Drawing.Size(219, 31);
            this.addProjectButton.TabIndex = 9;
            this.addProjectButton.Text = "Add new project";
            this.addProjectButton.UseVisualStyleBackColor = true;
            this.addProjectButton.Click += new System.EventHandler(this.addProjectButton_Click);
            // 
            // renameProjectButton
            // 
            this.renameProjectButton.Location = new System.Drawing.Point(288, 517);
            this.renameProjectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.renameProjectButton.Name = "renameProjectButton";
            this.renameProjectButton.Size = new System.Drawing.Size(82, 80);
            this.renameProjectButton.TabIndex = 11;
            this.renameProjectButton.Text = "Rename selected project";
            this.renameProjectButton.UseVisualStyleBackColor = true;
            this.renameProjectButton.Click += new System.EventHandler(this.renameProjectButton_Click);
            // 
            // newProjectName
            // 
            this.newProjectName.Location = new System.Drawing.Point(371, 544);
            this.newProjectName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newProjectName.Name = "newProjectName";
            this.newProjectName.Size = new System.Drawing.Size(135, 27);
            this.newProjectName.TabIndex = 12;
            this.newProjectName.Text = "New project name";
            this.newProjectName.Click += new System.EventHandler(this.newProjectName_Click);
            // 
            // openProjectButton
            // 
            this.openProjectButton.Location = new System.Drawing.Point(514, 189);
            this.openProjectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.openProjectButton.Name = "openProjectButton";
            this.openProjectButton.Size = new System.Drawing.Size(112, 67);
            this.openProjectButton.TabIndex = 13;
            this.openProjectButton.Text = "Open selected project";
            this.openProjectButton.UseVisualStyleBackColor = true;
            this.openProjectButton.Click += new System.EventHandler(this.openProjectButton_Click);
            // 
            // openWorkspaceButton
            // 
            this.openWorkspaceButton.Location = new System.Drawing.Point(765, 67);
            this.openWorkspaceButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.openWorkspaceButton.Name = "openWorkspaceButton";
            this.openWorkspaceButton.Size = new System.Drawing.Size(86, 105);
            this.openWorkspaceButton.TabIndex = 14;
            this.openWorkspaceButton.Text = "Open workspace";
            this.openWorkspaceButton.UseVisualStyleBackColor = true;
            this.openWorkspaceButton.Click += new System.EventHandler(this.openWorkspaceButton_Click);
            // 
            // TaskManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.openWorkspaceButton);
            this.Controls.Add(this.openProjectButton);
            this.Controls.Add(this.newProjectName);
            this.Controls.Add(this.renameProjectButton);
            this.Controls.Add(this.removeProjectButton);
            this.Controls.Add(this.addProjectButton);
            this.Controls.Add(this.labelProjects);
            this.Controls.Add(this.projectList);
            this.Controls.Add(this.removeUserButton);
            this.Controls.Add(this.labelUsers);
            this.Controls.Add(this.registerUserButton);
            this.Controls.Add(this.userList);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TaskManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskManagerForm_FormClosing);
            this.Load += new System.EventHandler(this.TaskManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox userList;
        private System.Windows.Forms.Button registerUserButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label labelUsers;
        private System.Windows.Forms.Button removeUserButton;
        private System.Windows.Forms.ListBox projectList;
        private System.Windows.Forms.Label labelProjects;
        private System.Windows.Forms.Button removeProjectButton;
        private System.Windows.Forms.Button addProjectButton;
        private System.Windows.Forms.Button renameProjectButton;
        private System.Windows.Forms.TextBox newProjectName;
        private System.Windows.Forms.Button openProjectButton;
        private System.Windows.Forms.Button openWorkspaceButton;
    }
}

