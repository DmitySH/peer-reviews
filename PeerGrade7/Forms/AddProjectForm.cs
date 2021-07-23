using System;
using System.Drawing;
using System.Windows.Forms;
using TaskManagerLibrary;

namespace Forms
{
    /// <summary>
    /// Form to add new challenge.
    /// </summary>
    public partial class AddProjectForm : Form
    {
        private readonly ListBox list;
        public AddProjectForm(ListBox list)
        {
            InitializeComponent();
            this.list = list;
        }

        private void AddProjectForm_Load(object sender, EventArgs e)
        {
            MinimumSize = new Size(220, 200);
            MaximumSize = new Size(220, 200);
            MaximizeBox = false;
            MinimizeBox = false;
        }

        /// <summary>
        /// Clears textbox.
        /// </summary>
        private void numberChallenges_Click(object sender, EventArgs e) => numberChallenges.Text = string.Empty;

        /// <summary>
        /// Clears textbox.
        /// </summary>
        private void projectName_Click(object sender, EventArgs e) => projectName.Text = string.Empty;

        /// <summary>
        /// Button to add project.
        /// </summary>
        private void addProjectButton_Click(object sender, EventArgs e)
        {
            if (!projectName.Text.Equals("Enter project name") && !projectName.Text.Equals("") &&
                !numberChallenges.Text.Equals("") && !projectName.Text.Equals("Enter number of challenges")
                && int.TryParse(numberChallenges.Text, out int num) && num > 0)
            {
                foreach (var project in Project.Projects)
                {
                    if (project.Name == projectName.Text)
                    {
                        MessageBox.Show("Project with this name is already exists");
                        return;
                    }
                }
                Project.CreateProject(projectName.Text, num);
                list.Items.Add($"{projectName.Text} : {Project.Projects[^1].Challenges.Count} challenges");
                projectName.Text = "Enter project name";
                numberChallenges.Text = "Enter number of challenges";
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter not empty and correct project name and number of challenges");
            }
        }
    }
}
