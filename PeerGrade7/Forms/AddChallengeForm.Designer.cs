
namespace Forms
{
    partial class AddChallengeForm
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
            this.challengeName = new System.Windows.Forms.TextBox();
            this.addChallenge = new System.Windows.Forms.Button();
            this.numberOfExecutors = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // challengeName
            // 
            this.challengeName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.challengeName.Location = new System.Drawing.Point(12, 65);
            this.challengeName.Name = "challengeName";
            this.challengeName.Size = new System.Drawing.Size(162, 29);
            this.challengeName.TabIndex = 9;
            this.challengeName.Text = "Enter challenge name";
            this.challengeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.challengeName.Click += new System.EventHandler(this.challengeName_Click);
            // 
            // addChallenge
            // 
            this.addChallenge.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addChallenge.Location = new System.Drawing.Point(12, 12);
            this.addChallenge.Name = "addChallenge";
            this.addChallenge.Size = new System.Drawing.Size(162, 47);
            this.addChallenge.TabIndex = 8;
            this.addChallenge.Text = "Add challenge";
            this.addChallenge.UseVisualStyleBackColor = true;
            this.addChallenge.Click += new System.EventHandler(this.addChallenge_Click);
            // 
            // numberOfExecutors
            // 
            this.numberOfExecutors.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numberOfExecutors.Location = new System.Drawing.Point(12, 103);
            this.numberOfExecutors.Name = "numberOfExecutors";
            this.numberOfExecutors.Size = new System.Drawing.Size(162, 25);
            this.numberOfExecutors.TabIndex = 10;
            this.numberOfExecutors.Text = "Enter number of executors";
            this.numberOfExecutors.Click += new System.EventHandler(this.numberOfExecutors_Click);
            // 
            // AddChallengeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.numberOfExecutors);
            this.Controls.Add(this.challengeName);
            this.Controls.Add(this.addChallenge);
            this.Name = "AddChallengeForm";
            this.Text = "Add challenge";
            this.Load += new System.EventHandler(this.AddChallengeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox challengeName;
        private System.Windows.Forms.Button addChallenge;
        private System.Windows.Forms.TextBox numberOfExecutors;
    }
}