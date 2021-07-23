using System;
using System.Drawing;
using System.Windows.Forms;
using TaskManagerLibrary;

namespace Forms
{
    /// <summary>
    /// Custom page for epic challenge.
    /// </summary>
    class EpicCustomTab : TabPage
    {
        private readonly ProjectForm projectForm;
        public EpicCustomTab(string text, TaskManagerForm taskForm, ProjectForm projectForm) : base(text)
        {
            this.projectForm = projectForm;

            Tabs = new TabControl();
            Tabs.Size = new Size(599, 500);

            CreationDate = new Label();
            CreationDate.Size = new Size(190, 30);
            CreationDate.Location = new Point(600, 70);

            Status = new Label();
            Status.Size = new Size(150, 30);
            Status.Location = new Point(600, 31);

            AddStoryButton = new Button();
            AddStoryButton.Text = "Add Story";
            AddStoryButton.Click += (sender, e) => new AddChallengeForm(projectForm, "Story", true).Show();
            AddStoryButton.Location = new Point(600, 290);
            AddStoryButton.Size = new Size(100, 55);

            AddTaskButton = new Button();
            AddTaskButton.Text = "Add Task";
            AddTaskButton.Click += (sender, e) => new AddChallengeForm(projectForm, "Task", true).Show();
            AddTaskButton.Location = new Point(600, 200);
            AddTaskButton.Size = new Size(100, 55);

            RemoveChallengeButton = new Button();
            RemoveChallengeButton.Text = "Remove selected challenge";
            RemoveChallengeButton.Click += RemoveChallenge;
            RemoveChallengeButton.Location = new Point(600, 110);
            RemoveChallengeButton.Size = new Size(100, 55);

            this.Controls.Add(RemoveChallengeButton);
            this.Controls.Add(Tabs);
            this.Controls.Add(AddTaskButton);
            this.Controls.Add(AddStoryButton);
            this.Controls.Add(Status);
            this.Controls.Add(CreationDate);
        }

        public Button RemoveChallengeButton { get; set; }
        public Button AddTaskButton { get; set; }
        public Button AddStoryButton { get; set; }
        public TabControl Tabs { get; set; }
        public Label Status { get; set; }
        public Label CreationDate { get; set; }

        public void RemoveChallenge(object sender, EventArgs e)
        {
            if (Tabs.SelectedIndex < 0)
            {
                return;
            }

            foreach (var user in User.Users)
            {
                foreach (var userChallenge in user.Challenges)
                {
                    if (userChallenge.Name.Equals(
                        (projectForm.Project.Challenges[projectForm.challengesTab.SelectedIndex] as Epic)
                        .SubChallenges[Tabs.SelectedIndex].Name))
                    {
                        user.Challenges.Remove(userChallenge);
                        (projectForm.Project.Challenges[projectForm.challengesTab.SelectedIndex] as Epic)
                            .SubChallenges.RemoveAt(Tabs.SelectedIndex);
                        Tabs.TabPages.RemoveAt(Tabs.SelectedIndex);
                        return;
                    }
                }
                
            }

            (projectForm.Project.Challenges[projectForm.challengesTab.SelectedIndex] as Epic)
                .SubChallenges.RemoveAt(Tabs.SelectedIndex);
            Tabs.TabPages.RemoveAt(Tabs.SelectedIndex);
        }
    }
}
