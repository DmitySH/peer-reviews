using System;
using System.Drawing;
using System.Windows.Forms;
using TaskManagerLibrary;

namespace Forms
{
    /// <summary>
    /// Custom page for challenges.
    /// </summary>
    class CustomTabPage : TabPage
    {
        private readonly TaskManagerForm taskForm;
        private readonly ProjectForm projectForm;
        public CustomTabPage(string text, TaskManagerForm taskForm, ProjectForm projectForm) : base(text)
        {
            taskForm.UserChanged += UserListChange;
            this.taskForm = taskForm;
            this.projectForm = projectForm;

            Initialization(taskForm);
        }

        /// <summary>
        /// Initializes components.
        /// </summary>
        /// <param name="taskForm"> Previous form. </param>
        private void Initialization(TaskManagerForm taskForm)
        {
            AddExecutorButton = new Button();
            AddExecutorButton.Text = "Make selected user executor";
            AddExecutorButton.Click += AddExecutor;
            AddExecutorButton.Location = new Point(20, 270);
            AddExecutorButton.Size = new Size(100, 60);

            RemoveExecutorButton = new Button();
            RemoveExecutorButton.Text = "Remove selected executor";
            RemoveExecutorButton.Click += RemoveExecutor;
            RemoveExecutorButton.Location = new Point(235, 270);
            RemoveExecutorButton.Size = new Size(100, 60);

            UsersBox = new ListBox();
            UsersBox.Items.AddRange(taskForm.userList.Items);
            UsersBox.Location = new Point(1, 35);
            UsersBox.Size = new Size(150, 230);

            ExecutorsBox = new ListBox();
            ExecutorsBox.Location = new Point(200, 35);
            ExecutorsBox.Size = new Size(150, 230);

            CreationDate = new Label();
            CreationDate.Size = new Size(190, 30);
            CreationDate.Location = new Point(400, 70);

            Status = new Label();
            Status.Size = new Size(150, 30);
            Status.Location = new Point(400, 31);

            UserLabel = new Label();
            UserLabel.Text = "List of users";
            UserLabel.Size = new Size(100, 20);
            UserLabel.Location = new Point(45, 10);

            ExecutorLabel = new Label();
            ExecutorLabel.Text = "List of executors";
            ExecutorLabel.Size = new Size(100, 20);
            ExecutorLabel.Location = new Point(230, 10);

            this.Controls.Add(Status);
            this.Controls.Add(CreationDate);
            this.Controls.Add(UsersBox);
            this.Controls.Add(ExecutorsBox);
            this.Controls.Add(AddExecutorButton);
            this.Controls.Add(RemoveExecutorButton);
            this.Controls.Add(UserLabel);
            this.Controls.Add(ExecutorLabel);
        }

        /// <summary>
        /// Updates users' list.
        /// </summary>
        public void UserListChange()
        {
            this.Controls.Remove(UsersBox);
            UsersBox = new ListBox();
            UsersBox.Items.AddRange(taskForm.userList.Items);
            UsersBox.Location = new Point(0, 35);
            UsersBox.Size = new Size(150, 230);
            this.Controls.Add(UsersBox);
        }

        /// <summary>
        /// Adds executor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddExecutor(object sender, EventArgs e)
        {
            if (UsersBox.SelectedIndex == -1 || projectForm.challengesTab.SelectedIndex < 0)
            {
                MessageBox.Show("User is not selected");
                return;
            }

            foreach (var item in ExecutorsBox.Items)
            {
                if (item == UsersBox.Items[UsersBox.SelectedIndex])
                {
                    MessageBox.Show("Executor with this name is already exists");
                    return;
                }
            }

            if (projectForm.Project.Challenges[projectForm.challengesTab.SelectedIndex] is IAssignable challenge)
            {
                if (challenge.Executors.Count == challenge.Size)
                {
                    MessageBox.Show("There is maximum of executors for this challenge");
                    return;
                }
                challenge.AddExecutor(User.Users[UsersBox.SelectedIndex]);
                User.Users[UsersBox.SelectedIndex].Challenges.Add((Challenge)challenge);
                ExecutorsBox.Items.Add(UsersBox.Items[UsersBox.SelectedIndex]);
            }
            else if (projectForm.Project.Challenges[projectForm.challengesTab.SelectedIndex] is Epic challengeEpic)
            {
                if ((challengeEpic.SubChallenges[(projectForm.challengesTab.SelectedTab as EpicCustomTab).
                    Tabs.SelectedIndex] as IAssignable ).Executors.Count == (challengeEpic.
                    SubChallenges[(projectForm.challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex] as IAssignable).Size)
                {
                    MessageBox.Show("There is maximum of executors for this challenge");
                    return;
                }

                (challengeEpic.SubChallenges[(projectForm.challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex]
                    as IAssignable).AddExecutor(User.Users[UsersBox.SelectedIndex]);
                User.Users[UsersBox.SelectedIndex].Challenges.Add((challengeEpic.SubChallenges[(projectForm.challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex]));
                ExecutorsBox.Items.Add(UsersBox.Items[UsersBox.SelectedIndex]);
            }
        }
        
        /// <summary>
        /// Removes executor.
        /// </summary>
        public void RemoveExecutor(object sender, EventArgs e)
        {
            if (ExecutorsBox.SelectedIndex == -1 || projectForm.challengesTab.SelectedIndex < 0)
            {
                MessageBox.Show("Executor is not selected");
                return;
            }

            if (projectForm.Project.Challenges[projectForm.challengesTab.SelectedIndex] is IAssignable challenge)
            {
                TaskManagerForm.OnChallengeDeleted(challenge as Challenge);
                challenge.RemoveExecutor(challenge.Executors[ExecutorsBox.SelectedIndex]);
                ExecutorsBox.Items.RemoveAt(ExecutorsBox.SelectedIndex);
            }
            else if (projectForm.Project.Challenges[projectForm.challengesTab.SelectedIndex] is Epic challengeEpic)
            {
                TaskManagerForm.OnChallengeDeleted(challengeEpic.SubChallenges
                    [(projectForm.challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex]);

                (challengeEpic.SubChallenges[(projectForm.challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex]
                    as IAssignable).RemoveExecutor((challengeEpic.SubChallenges[(projectForm.challengesTab.SelectedTab as 
                        EpicCustomTab).Tabs.SelectedIndex] as IAssignable).Executors[ExecutorsBox.SelectedIndex]);

                ExecutorsBox.Items.RemoveAt(ExecutorsBox.SelectedIndex);
            }
        }

        public ListBox UsersBox { get; set; }
        public ListBox ExecutorsBox { get; set; }
        public Label Status { get; set; }
        public Label CreationDate { get; set; }
        public Label ExecutorLabel { get; set; }
        public Label UserLabel { get; set; }
        public Button AddExecutorButton { get; set; }
        public Button RemoveExecutorButton { get; set; }
    }
}
