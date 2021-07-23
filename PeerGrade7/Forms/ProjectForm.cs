using System;
using System.Windows.Forms;
using TaskManagerLibrary;
namespace Forms
{
    /// <summary>
    /// Form of the project.
    /// </summary>
    public partial class ProjectForm : Form
    {
        public TaskManagerForm TaskForm { get; }
        public Project Project { get; }
        public ProjectForm(Project project, TaskManagerForm taskForm)
        {
            InitializeComponent();
            Project = project;
            TaskForm = taskForm;
            Text = Project.Name;

            foreach (var challenge in Project.Challenges)
            {
                if (challenge is IAssignable challengeIAssignable)
                {
                    CustomTabPage tabPage = new CustomTabPage($"{challenge.GetType().ToString().Split('.')[^1]} {challenge.Name}",
                    taskForm, this);
                    tabPage.Status.Text = $"This challenge is {challenge.Status}";
                    tabPage.CreationDate.Text = $"Was created at {challenge.DateOfCreation}";


                    foreach (var executor in challengeIAssignable.Executors)
                    {
                        tabPage.ExecutorsBox.Items.Add(executor);
                    }
                    challengesTab.TabPages.Add(tabPage);

                }
                else if (challenge is Epic challengeEpic)
                {
                    EpicCustomTab epicTabPage = new EpicCustomTab($"Epic {challenge.Name}",
                        taskForm, this);
                    epicTabPage.Status.Text = $"Epic is {challenge.Status}";
                    epicTabPage.CreationDate.Text = $"Was created at {challenge.DateOfCreation}";

                    foreach (var subChallenge in challengeEpic.SubChallenges)
                    {
                        CustomTabPage tabPage = new CustomTabPage($"{subChallenge.GetType().ToString().Split('.')[^1]} {subChallenge.Name}",
                            taskForm, this);
                        tabPage.Status.Text = $"This challenge is {subChallenge.Status}";
                        tabPage.CreationDate.Text = $"Was created at {subChallenge.DateOfCreation}";
                        foreach (var executor in (subChallenge as IAssignable).Executors)
                        {
                            tabPage.ExecutorsBox.Items.Add(executor);
                        }
                        epicTabPage.Tabs.TabPages.Add(tabPage);
                    }
                    challengesTab.TabPages.Add(epicTabPage);
                }
            }
        }

        /// <summary>
        /// Adds new bug.
        /// </summary>
        private void addBugTSMI_Click(object sender, EventArgs e)
        {
            if (Project.Size!= Project.Challenges.Count)
            {
                new AddChallengeForm(this, "Bug", false).Show();
            }
            else
            {
                MessageBox.Show("There is maximum of challenges for this project");
            }
        }

        /// <summary>
        /// Adds new task.
        /// </summary>
        private void addTaskTSMI_Click(object sender, EventArgs e)
        {
            if (Project.Size != Project.Challenges.Count)
            {
                new AddChallengeForm(this, "Task", false).Show();
            }
            else
            {
                MessageBox.Show("There is maximum of challenges for this project");
            }
        }

        /// <summary>
        /// Adds new story. 
        /// </summary>
        private void addStoryTSMI_Click(object sender, EventArgs e)
        {
            if (Project.Size != Project.Challenges.Count)
            {
                new AddChallengeForm(this, "Story", false).Show();
            }
            else
            {
                MessageBox.Show("There is maximum of challenges for this project");
            }
        }

        /// <summary>
        /// Adds new epic.
        /// </summary>
        private void addEpicTSMI_Click(object sender, EventArgs e)
        {
            if (Project.Size != Project.Challenges.Count)
            {
                new AddChallengeForm(this, "Epic", false).Show();
            }
            else
            {
                MessageBox.Show("There is maximum of challenges for this project");
            }
        }

        /// <summary>
        /// Changes status of challenge.
        /// </summary>
        /// <param name="newStatus"></param>
        private void ChangeStatus(string newStatus)
        {
            if (challengesTab.SelectedIndex < 0)
            {
                return;
            }

            if (Project.Challenges[challengesTab.SelectedIndex] is Epic)
            {
                if ((challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex < 0)
                {
                    MessageBox.Show("Epic status depends on status of it's sub challenges");
                    return;
                }

                (Project.Challenges[challengesTab.SelectedIndex] as Epic).SubChallenges
                    [(challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex].Status = newStatus;

                ((challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedTab as CustomTabPage).Status.Text =
                    $"This challenge is " +
                    $"{(Project.Challenges[challengesTab.SelectedIndex] as Epic).SubChallenges[(challengesTab.SelectedTab as EpicCustomTab).Tabs.SelectedIndex].Status}";

                (challengesTab.SelectedTab as EpicCustomTab).Status.Text = $"Epic is " +
                                                                           $"{(Project.Challenges[challengesTab.SelectedIndex] as Epic).Status}";
                return;
            }

            Project.Challenges[challengesTab.SelectedIndex].Status = newStatus;
            (challengesTab.TabPages[challengesTab.SelectedIndex] as CustomTabPage).Status.Text =
                $"This challenge is {Project.Challenges[challengesTab.SelectedIndex].Status}";
        }

        /// <summary>
        /// Makes challenges closed.
        /// </summary>
        private void toClosedTSMI_Click(object sender, EventArgs e) => ChangeStatus("closed");

        /// <summary>
        /// Makes challenge in progress.
        /// </summary>
        private void toInProgressTSMI_Click(object sender, EventArgs e) => ChangeStatus("in progress");

        /// <summary>
        /// Makes challenge opened.
        /// </summary>
        private void toOpenedTSMI_Click(object sender, EventArgs e) => ChangeStatus("opened");

        /// <summary>
        /// Changes challenge's name.
        /// </summary>
        /// <param name="status"> New name. </param>
        public void ChangeName(string newName)
        {
            foreach (var project in Project.Projects)
            {
                if (project.Name == this.Text)
                {
                    return;
                }
            }
            this.Text = newName;
        }

        /// <summary>
        /// Removes selected challenge.
        /// </summary>
        private void removeChallengeTSMI_Click(object sender, EventArgs e)
        {
            if (challengesTab.SelectedIndex < 0)
            {
                return;
            }

            foreach (var user in User.Users)
            {
                foreach (var userChallenge in user.Challenges)
                {
                    if (userChallenge.Name.Equals(Project.Challenges[challengesTab.SelectedIndex].Name))
                    {
                        user.Challenges.Remove(userChallenge);
                        Project.Challenges.RemoveAt(challengesTab.SelectedIndex);
                        challengesTab.TabPages.RemoveAt(challengesTab.SelectedIndex);
                        AddChallengeForm.OnChallengesChanged(Project.Projects.IndexOf(Project));
                        return;
                    }
                }
            }
            Project.Challenges.RemoveAt(challengesTab.SelectedIndex);
            challengesTab.TabPages.RemoveAt(challengesTab.SelectedIndex);
            AddChallengeForm.OnChallengesChanged(Project.Projects.IndexOf(Project));
        }

        /// <summary>
        /// Checks status.
        /// </summary>
        /// <param name="status"> Status. </param>
        private void CheckStatus(string status)
        {
            string show = $"All {status} challenges:{Environment.NewLine}";
            foreach (var projectChallenge in Project.Challenges)
            {
                if (projectChallenge.Status.Equals(status))
                {
                    show += projectChallenge.Name + Environment.NewLine;
                }
            }

            if (show == $"All {status} challenges:{Environment.NewLine}")
            {
                MessageBox.Show($"This project doesn't have {status} challenges");
            }
            else
            {
                MessageBox.Show(show);
            }
        }

        /// <summary>
        /// Shows opened challenges.
        /// </summary>
        private void openedTSMI_Click(object sender, EventArgs e) => CheckStatus("opened");

        /// <summary>
        /// Shows in progress challenges.
        /// </summary>
        private void inProgressTSMI_Click(object sender, EventArgs e) => CheckStatus("in progress");

        /// <summary>
        /// Shows closed challenges.
        /// </summary>
        private void closedTSMI_Click(object sender, EventArgs e) => CheckStatus("closed");
    }
}
