
namespace Forms
{
    partial class AddProjectForm
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
            this.projectName = new System.Windows.Forms.TextBox();
            this.addProjectButton = new System.Windows.Forms.Button();
            this.numberChallenges = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // projectName
            // 
            this.projectName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.projectName.Location = new System.Drawing.Point(12, 12);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(169, 32);
            this.projectName.TabIndex = 9;
            this.projectName.Text = "Enter project name";
            this.projectName.Click += new System.EventHandler(this.projectName_Click);
            // 
            // addProjectButton
            // 
            this.addProjectButton.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addProjectButton.Location = new System.Drawing.Point(12, 106);
            this.addProjectButton.Name = "addProjectButton";
            this.addProjectButton.Size = new System.Drawing.Size(169, 47);
            this.addProjectButton.TabIndex = 8;
            this.addProjectButton.Text = "Add project";
            this.addProjectButton.UseVisualStyleBackColor = true;
            this.addProjectButton.Click += new System.EventHandler(this.addProjectButton_Click);
            // 
            // numberChallenges
            // 
            this.numberChallenges.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numberChallenges.Location = new System.Drawing.Point(12, 50);
            this.numberChallenges.Name = "numberChallenges";
            this.numberChallenges.Size = new System.Drawing.Size(169, 25);
            this.numberChallenges.TabIndex = 10;
            this.numberChallenges.Text = "Enter number of challenges";
            this.numberChallenges.Click += new System.EventHandler(this.numberChallenges_Click);
            // 
            // AddProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numberChallenges);
            this.Controls.Add(this.projectName);
            this.Controls.Add(this.addProjectButton);
            this.Name = "AddProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add project";
            this.Load += new System.EventHandler(this.AddProjectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.Button addProjectButton;
        private System.Windows.Forms.TextBox numberChallenges;
    }
}