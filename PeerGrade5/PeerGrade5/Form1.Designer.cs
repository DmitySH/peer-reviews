
namespace PeerGrade5
{
    partial class Form1
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
            this.fractalList = new System.Windows.Forms.ListBox();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.angleLeftBox = new System.Windows.Forms.TextBox();
            this.flowLayoutMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.labelParams = new System.Windows.Forms.Label();
            this.buttonDrawIt = new System.Windows.Forms.Button();
            this.angleRightBox = new System.Windows.Forms.TextBox();
            this.koefBox = new System.Windows.Forms.TextBox();
            this.depthBox = new System.Windows.Forms.TextBox();
            this.lengthBox = new System.Windows.Forms.TextBox();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.distanceBox = new System.Windows.Forms.TextBox();
            this.colorDialogStart = new System.Windows.Forms.ColorDialog();
            this.colorDialogEnd = new System.Windows.Forms.ColorDialog();
            this.buttonColorEnd = new System.Windows.Forms.Button();
            this.buttonColorStart = new System.Windows.Forms.Button();
            this.depthLabel = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.leftBranchAngleLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.rightBranchAngleLabel = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.flowLayoutMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // fractalList
            // 
            this.fractalList.FormattingEnabled = true;
            this.fractalList.ItemHeight = 20;
            this.fractalList.Location = new System.Drawing.Point(179, 3);
            this.fractalList.Name = "fractalList";
            this.fractalList.Size = new System.Drawing.Size(150, 204);
            this.fractalList.TabIndex = 2;
            this.fractalList.SelectedIndexChanged += new System.EventHandler(this.fractalList_SelectedIndexChanged);
            // 
            // buttonChoose
            // 
            this.buttonChoose.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonChoose.Location = new System.Drawing.Point(3, 3);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(170, 203);
            this.buttonChoose.TabIndex = 3;
            this.buttonChoose.Text = "Choose this fractal";
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // angleLeftBox
            // 
            this.angleLeftBox.Location = new System.Drawing.Point(335, 180);
            this.angleLeftBox.Name = "angleLeftBox";
            this.angleLeftBox.Size = new System.Drawing.Size(131, 27);
            this.angleLeftBox.TabIndex = 4;
            this.angleLeftBox.Text = "Left branch angle";
            this.angleLeftBox.Click += new System.EventHandler(this.angleLeftBox_Click);
            // 
            // flowLayoutMenu
            // 
            this.flowLayoutMenu.Controls.Add(this.buttonChoose);
            this.flowLayoutMenu.Controls.Add(this.fractalList);
            this.flowLayoutMenu.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutMenu.Name = "flowLayoutMenu";
            this.flowLayoutMenu.Size = new System.Drawing.Size(331, 205);
            this.flowLayoutMenu.TabIndex = 5;
            this.flowLayoutMenu.WrapContents = false;
            // 
            // labelParams
            // 
            this.labelParams.AutoSize = true;
            this.labelParams.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelParams.Location = new System.Drawing.Point(339, 3);
            this.labelParams.Name = "labelParams";
            this.labelParams.Size = new System.Drawing.Size(78, 32);
            this.labelParams.TabIndex = 6;
            this.labelParams.Text = "label1";
            // 
            // buttonDrawIt
            // 
            this.buttonDrawIt.Location = new System.Drawing.Point(768, 12);
            this.buttonDrawIt.Name = "buttonDrawIt";
            this.buttonDrawIt.Size = new System.Drawing.Size(137, 29);
            this.buttonDrawIt.TabIndex = 8;
            this.buttonDrawIt.Text = "Draw It!";
            this.buttonDrawIt.UseVisualStyleBackColor = true;
            this.buttonDrawIt.Click += new System.EventHandler(this.buttonDrawIt_Click);
            // 
            // angleRightBox
            // 
            this.angleRightBox.Location = new System.Drawing.Point(335, 144);
            this.angleRightBox.Name = "angleRightBox";
            this.angleRightBox.Size = new System.Drawing.Size(131, 27);
            this.angleRightBox.TabIndex = 9;
            this.angleRightBox.Text = "Right branch angle";
            this.angleRightBox.Click += new System.EventHandler(this.angleRightBox_Click);
            // 
            // koefBox
            // 
            this.koefBox.Location = new System.Drawing.Point(335, 111);
            this.koefBox.Name = "koefBox";
            this.koefBox.Size = new System.Drawing.Size(131, 27);
            this.koefBox.TabIndex = 10;
            this.koefBox.Text = "Сoefficient";
            this.koefBox.Click += new System.EventHandler(this.koefBox_Click);
            // 
            // depthBox
            // 
            this.depthBox.Location = new System.Drawing.Point(335, 71);
            this.depthBox.Name = "depthBox";
            this.depthBox.Size = new System.Drawing.Size(131, 27);
            this.depthBox.TabIndex = 11;
            this.depthBox.Text = "Depth";
            this.depthBox.Click += new System.EventHandler(this.depthBox_Click);
            // 
            // lengthBox
            // 
            this.lengthBox.Location = new System.Drawing.Point(335, 37);
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.Size = new System.Drawing.Size(131, 27);
            this.lengthBox.TabIndex = 12;
            this.lengthBox.Text = "Length";
            this.lengthBox.Click += new System.EventHandler(this.lengthBox_Click);
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(335, 104);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(131, 27);
            this.heightBox.TabIndex = 13;
            this.heightBox.Text = "Height";
            this.heightBox.Click += new System.EventHandler(this.heightBox_Click);
            // 
            // distanceBox
            // 
            this.distanceBox.Location = new System.Drawing.Point(335, 137);
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.Size = new System.Drawing.Size(131, 27);
            this.distanceBox.TabIndex = 14;
            this.distanceBox.Text = "Distance";
            this.distanceBox.Click += new System.EventHandler(this.distanceBox_Click);
            // 
            // buttonColorEnd
            // 
            this.buttonColorEnd.Location = new System.Drawing.Point(768, 141);
            this.buttonColorEnd.Name = "buttonColorEnd";
            this.buttonColorEnd.Size = new System.Drawing.Size(137, 29);
            this.buttonColorEnd.TabIndex = 16;
            this.buttonColorEnd.Text = "Choose color end";
            this.buttonColorEnd.UseVisualStyleBackColor = true;
            this.buttonColorEnd.Click += new System.EventHandler(this.buttonColorEnd_Click);
            // 
            // buttonColorStart
            // 
            this.buttonColorStart.Location = new System.Drawing.Point(768, 93);
            this.buttonColorStart.Name = "buttonColorStart";
            this.buttonColorStart.Size = new System.Drawing.Size(137, 29);
            this.buttonColorStart.TabIndex = 17;
            this.buttonColorStart.Text = "Choose color start";
            this.buttonColorStart.UseVisualStyleBackColor = true;
            this.buttonColorStart.Click += new System.EventHandler(this.buttonColorStart_Click);
            // 
            // depthLabel
            // 
            this.depthLabel.AutoSize = true;
            this.depthLabel.Location = new System.Drawing.Point(487, 74);
            this.depthLabel.Name = "depthLabel";
            this.depthLabel.Size = new System.Drawing.Size(50, 20);
            this.depthLabel.TabIndex = 19;
            this.depthLabel.Text = "Depth";
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point(487, 40);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(54, 20);
            this.lengthLabel.TabIndex = 18;
            this.lengthLabel.Text = "Length";
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(487, 144);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(66, 20);
            this.distanceLabel.TabIndex = 21;
            this.distanceLabel.Text = "Distance";
            // 
            // leftBranchAngleLabel
            // 
            this.leftBranchAngleLabel.AutoSize = true;
            this.leftBranchAngleLabel.Location = new System.Drawing.Point(487, 187);
            this.leftBranchAngleLabel.Name = "leftBranchAngleLabel";
            this.leftBranchAngleLabel.Size = new System.Drawing.Size(124, 20);
            this.leftBranchAngleLabel.TabIndex = 22;
            this.leftBranchAngleLabel.Text = "Left branch angle";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(487, 111);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(54, 20);
            this.heightLabel.TabIndex = 20;
            this.heightLabel.Text = "Height";
            // 
            // rightBranchAngleLabel
            // 
            this.rightBranchAngleLabel.AutoSize = true;
            this.rightBranchAngleLabel.Location = new System.Drawing.Point(487, 151);
            this.rightBranchAngleLabel.Name = "rightBranchAngleLabel";
            this.rightBranchAngleLabel.Size = new System.Drawing.Size(134, 20);
            this.rightBranchAngleLabel.TabIndex = 23;
            this.rightBranchAngleLabel.Text = "Right branch angle";
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.SystemColors.Control;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelInfo.Location = new System.Drawing.Point(3, 235);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(581, 90);
            this.labelInfo.TabIndex = 24;
            this.labelInfo.Text = "To draw fractal just choose it from list and press Choose this fractal. Then ente" +
    "r all parameters and press Draw It! and wait a little bit.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 539);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.rightBranchAngleLabel);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.leftBranchAngleLabel);
            this.Controls.Add(this.distanceLabel);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.depthLabel);
            this.Controls.Add(this.buttonColorStart);
            this.Controls.Add(this.buttonColorEnd);
            this.Controls.Add(this.distanceBox);
            this.Controls.Add(this.heightBox);
            this.Controls.Add(this.lengthBox);
            this.Controls.Add(this.depthBox);
            this.Controls.Add(this.koefBox);
            this.Controls.Add(this.angleRightBox);
            this.Controls.Add(this.buttonDrawIt);
            this.Controls.Add(this.labelParams);
            this.Controls.Add(this.flowLayoutMenu);
            this.Controls.Add(this.angleLeftBox);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fractals";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox fractalList;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.TextBox angleLeftBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutMenu;
        private System.Windows.Forms.Label labelParams;
        private System.Windows.Forms.Button buttonDrawIt;
        private System.Windows.Forms.TextBox angleRightBox;
        private System.Windows.Forms.TextBox koefBox;
        private System.Windows.Forms.TextBox depthBox;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.TextBox distanceBox;
        public System.Windows.Forms.ColorDialog colorDialogStart;
        public System.Windows.Forms.ColorDialog colorDialogEnd;
        private System.Windows.Forms.Button buttonColorEnd;
        private System.Windows.Forms.Button buttonColorStart;
        private System.Windows.Forms.Label depthLabel;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Label leftBranchAngleLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label rightBranchAngleLabel;
        private System.Windows.Forms.Label labelInfo;
    }
}

