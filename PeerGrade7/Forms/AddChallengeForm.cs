using System;
using System.Drawing;
using System.Windows.Forms;
using TaskManagerLibrary;
using TabPage = System.Windows.Forms.TabPage;

namespace Forms
{
    /// <summary>
    /// Form to add challenge.
    /// </summary>
    public partial class AddChallengeForm : Form
    {
        private bool toEpic;
        private readonly ProjectForm form;
        private readonly string type;
        public static event Action<int> ChallengesChanged;
        public static void OnChallengesChanged(int i) => ChallengesChanged?.Invoke(i);
        public AddChallengeForm(ProjectForm form, string type, bool toEpic)
        {
            InitializeComponent();
            this.form = form;
            this.type = type;
            this.toEpic = toEpic;

            numberOfExecutors.Visible = type.Equals("Story");
        }

        private void AddChallengeForm_Load(object sender, EventArgs e)
        {
            MinimumSize = new Size(215, 180);
            MaximumSize = new Size(215, 180);
            MaximizeBox = false;
            MinimizeBox = false;
        }

        /// <summary>
        /// Clears textbox.
        /// </summary>
        private void challengeName_Click(object sender, EventArgs e) => challengeName.Text = string.Empty;

        /// <summary>
        /// Adds new challenge.
        /// </summary>
        private void addChallenge_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (!challengeName.Text.Equals("Enter challenge name") && !challengeName.Text.Equals("") &&
               ((!numberOfExecutors.Visible) || (!numberOfExecutors.Text.Equals("") &&
                int.TryParse(numberOfExecutors.Text, out num) && (num > 0))))
            {
                foreach (var tab in form.challengesTab.TabPages)
                {
                    if ((tab as TabPage).Text.Split(' ')[^1] == challengeName.Text)
                    {
                        MessageBox.Show("Challenge with this name is already exists");
                        return;
                    }
                }

                switch (type)
                {
                    case "Epic":
                        form.Project.AddEpicChallenge(challengeName.Text);
                        break;
                    case "Story":
                        if (!toEpic)
                            form.Project.AddStoryChallenge(num, challengeName.Text);
                        break;
                    case "Task":
                        if (!toEpic)
                            form.Project.AddTaskChallenge(challengeName.Text);
                        break;
                    case "Bug":
                        form.Project.AddBugChallenge(challengeName.Text);
                        break;
                }
                if (type.Equals("Epic"))
                {
                    EpicCustomTab epicTabPage = new EpicCustomTab($"{type} {challengeName.Text}", form.TaskForm, form);
                    epicTabPage.Status.Text = $"Epic is {form.Project.Challenges[^1].Status}";
                    epicTabPage.CreationDate.Text = $"Was created at {form.Project.Challenges[^1].DateOfCreation}";
                    form.challengesTab.TabPages.Add(epicTabPage);

                }
                else
                {
                    if (form.challengesTab.SelectedIndex >= 0 && toEpic
                        && form.challengesTab.TabPages[form.challengesTab.SelectedIndex].Text.Split(' ')[0].Equals("Epic"))
                    {
                        foreach (var tab in (form.challengesTab.TabPages[form.challengesTab.SelectedIndex] as EpicCustomTab)?.Tabs.TabPages)
                        {
                            if ((tab as TabPage).Text.Split(' ')[^1] == challengeName.Text)
                            {
                                MessageBox.Show("Challenge with this name is already exists");
                                return;
                            }
                        }

                        if (type == "Story")
                        {
                            (form.Project.Challenges[form.challengesTab.SelectedIndex] as Epic).SubChallenges.Add(new Story(challengeName.Text, num));
                        }
                        else
                        {
                            (form.Project.Challenges[form.challengesTab.SelectedIndex] as Epic).SubChallenges.Add(new Task(challengeName.Text));

                        }
                        CustomTabPage tabPage = new CustomTabPage($"{type} {challengeName.Text}", form.TaskForm, form);
                        tabPage.Size = new Size(399, 500);
                        tabPage.Status.Text = $"This challenge is {(form.Project.Challenges[form.challengesTab.SelectedIndex] as Epic).SubChallenges[^1].Status}";
                        tabPage.CreationDate.Text = $"Was created at {(form.Project.Challenges[form.challengesTab.SelectedIndex] as Epic).SubChallenges[^1].DateOfCreation}";
                        (form.challengesTab.TabPages[form.challengesTab.SelectedIndex] as EpicCustomTab)?.Tabs.TabPages.Add(tabPage);
                    }
                    else
                    {
                        CustomTabPage tabPage = new CustomTabPage($"{type} {challengeName.Text}", form.TaskForm, form);
                        tabPage.Status.Text = $"This challenge is {form.Project.Challenges[^1].Status}";
                        tabPage.CreationDate.Text = $"Was created at {form.Project.Challenges[^1].DateOfCreation}";
                        form.challengesTab.TabPages.Add(tabPage);
                    }
                }
                challengeName.Text = "Enter challenge name";
                numberOfExecutors.Text = "Enter number of executors";
                OnChallengesChanged(Project.Projects.IndexOf(form.Project));

                this.Close();
            }
            else
            {
                MessageBox.Show("Enter not empty and correct name and number of executors (if it is needed)");
            }
        }

        /// <summary>
        /// Clears textbox.
        /// </summary>
        private void numberOfExecutors_Click(object sender, EventArgs e) => numberOfExecutors.Text = string.Empty;
    }
}
