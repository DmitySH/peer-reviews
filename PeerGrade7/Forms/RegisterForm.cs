using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TaskManagerLibrary;

namespace Forms
{
    /// <summary>
    /// Form to add new user.
    /// </summary>
    public partial class RegisterForm : Form
    {
        private readonly TaskManagerForm taskform;
        public RegisterForm(TaskManagerForm taskForm)
        {
            InitializeComponent();
            this.taskform = taskForm;
        }

        /// <summary>
        /// Adds new user.
        /// </summary>
        private void registerUser_Click(object sender, EventArgs e)
        {
            if (!userName.Text.Equals("Enter user name") && !userName.Text.Equals(""))
            {
                foreach (var user in User.Users)
                {
                    if (user.Name == userName.Text)
                    {
                        MessageBox.Show("User with this name is already exists");
                        return;
                    }
                }
                User.CreateUser(userName.Text);
                TaskManagerForm.ChallengeDeleted+= User.Users[^1].RemoveChallenge;
                
                taskform.userList.Items.Add(userName.Text);
                userName.Text = "Enter user name";
                taskform.OnUserChanged();
                this.Close();
            }
            else
            {
                MessageBox.Show("Enter not empty and correct username");
            }
        }

        /// <summary>
        /// Makes textbox clear.
        /// </summary>
        private void userName_Click(object sender, EventArgs e) => userName.Text = string.Empty;

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            MinimumSize = new Size(215, 160);
            MaximumSize = new Size(215, 160);
            MaximizeBox = false;
            MinimizeBox = false;
        }
    }
}
