using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TaskManagerLibrary;
using System.Runtime.Serialization;

namespace Forms
{
    /// Another Documentation+
    /// Here the idea is similar to a menu, however, here in the constructor of each of
    /// the forms I pass a link to the previous one, which makes a single-linked list.
    /// Again, a lot of incomprehensible long code, but most of it is a reference many
    /// times ago to find the current project or the like.

    /// <summary>
    /// Start form.
    /// </summary>
    public partial class TaskManagerForm : Form
    {

        public event Action UserChanged;
        public void OnUserChanged() => UserChanged?.Invoke();
        public event Action<string> NameChanged;
        public void OnNameChanged(string name) => NameChanged?.Invoke(name);

        public static event Action<Challenge> ChallengeDeleted;
        public static void OnChallengeDeleted(Challenge challenge) => ChallengeDeleted?.Invoke(challenge);

        private WorkspaceForm workspaceForm;

        public TaskManagerForm()
        {
            InitializeComponent();
            AddChallengeForm.ChallengesChanged += ChangeCountOfChallenges;

            // Deserialization.
            try
            {
                using (FileStream fs = new FileStream("saveUser.json", FileMode.Open))
                {
                    var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                    var ser = new DataContractSerializer(typeof(List<User>), dcss);
                    User.Users = ser.ReadObject(fs) as List<User>;
                    foreach (var user in User.Users)
                    {
                        ChallengeDeleted += user.RemoveChallenge;
                        userList.Items.Add(user.Name);
                    }
                }

                using (FileStream fs = new FileStream("saveProject.json", FileMode.Open))
                {
                    var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                    var ser = new DataContractSerializer(typeof(List<Project>), dcss);
                    Project.Projects = ser.ReadObject(fs) as List<Project>;
                    foreach (var project in Project.Projects)
                    {
                        projectList.Items.Add(project.Name+": "+ project.Challenges.Count + " challenges");
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ARGHH");
            }
        }

        /// <summary>
        /// Sets new count of challenges of project.
        /// </summary>
        /// <param name="i"> Index. </param>
        public void ChangeCountOfChallenges(int i)
        {
            projectList.Items[i] =
                projectList.Items[i].ToString().Split(':')[0] + ": " + Project.Projects[i].Challenges.Count + " challenges";
        }

        private void TaskManagerForm_Load(object sender, EventArgs e)
        {
            MinimumSize = new Size(900, 500);
        }

        /// <summary>
        /// Opens form to add user.
        /// </summary>
        private void registerUser_Click(object sender, EventArgs e)
        {
            new RegisterForm(this).Show();
        }

        /// <summary>
        /// Removes selected user.
        /// </summary>
        private void removeUser_Click(object sender, EventArgs e)
        {
            if (userList.SelectedIndex == -1)
            {
                MessageBox.Show("User is not selected");
                return;
            }

            if (User.Users[userList.SelectedIndex].Challenges.Count == 0)
            {
                User.RemoveUser(userList.SelectedIndex);
                userList.Items.RemoveAt(userList.SelectedIndex);
                OnUserChanged();
            }
            else
            {
                MessageBox.Show("User has some challenges and can't be deleted");
            }
        }

        /// <summary>
        /// Opens form to add project.
        /// </summary>
        private void addProjectButton_Click(object sender, EventArgs e) => new AddProjectForm(projectList).Show();

        /// <summary>
        /// Removes selected project.
        /// </summary>
        private void removeProjectButton_Click(object sender, EventArgs e)
        {
            if (projectList.SelectedIndex == -1)
            {
                MessageBox.Show("Project is not selected");
                return;
            }

            foreach (var user in User.Users)
            {
                foreach (var challenge in Project.Projects[projectList.SelectedIndex].Challenges)
                {
                    if (user.Challenges.Contains(challenge))
                    {
                        user.Challenges.Remove(challenge);
                    }
                }
            }

            if (workspaceForm != null)
            {
                foreach (var child in workspaceForm.MdiChildren)
                {
                    if ((child as ProjectForm).Project.Name == Project.Projects[projectList.SelectedIndex].Name)
                    {
                        child.Close();
                    }
                }

                workspaceForm.LayoutMdi(MdiLayout.TileHorizontal);
            }
            
            Project.RemoveProject(projectList.SelectedIndex);
            projectList.Items.RemoveAt(projectList.SelectedIndex);
        }

        /// <summary>
        /// Renames selected project.
        /// </summary>
        private void renameProjectButton_Click(object sender, EventArgs e)
        {
            if (projectList.SelectedIndex == -1)
            {
                MessageBox.Show("Project is not selected");
                return;
            }
            if (!newProjectName.Text.Equals("New project name") && !newProjectName.Text.Equals(""))
            {
                projectList.Items[projectList.SelectedIndex] = newProjectName.Text + " : "
                    + Project.Projects[projectList.SelectedIndex].Challenges.Count + " challenges";
                Project.RenameProject(newProjectName.Text, projectList.SelectedIndex);
                OnNameChanged(newProjectName.Text);
                newProjectName.Text = "New project name";
            }
            else
            {
                MessageBox.Show("Enter not empty and correct project name");
            }
        }

        /// <summary>
        /// Clears textbox.
        /// </summary>
        private void newProjectName_Click(object sender, EventArgs e) => newProjectName.Text = string.Empty;

        /// <summary>
        /// Opens selected project.
        /// </summary>
        private void openProjectButton_Click(object sender, EventArgs e)
        {
            if (workspaceForm == null)
            {
                MessageBox.Show("Workspace is not opened");
                return;
            }

            if (projectList.SelectedIndex == -1)
            {
                MessageBox.Show("Project is not selected");
                return;
            }
            ProjectForm form = new ProjectForm(Project.Projects[projectList.SelectedIndex], this);

            foreach (var child in workspaceForm.MdiChildren)
            {
                if ((child as ProjectForm).Project.Name == form.Project.Name)
                {
                    MessageBox.Show("This project is already opened");
                    return;
                }
            }

            NameChanged += form.ChangeName;
            form.MdiParent = workspaceForm;
            form.Show();
            workspaceForm.LayoutMdi(MdiLayout.TileHorizontal);
        }

        /// <summary>
        /// Opens workspace.
        /// </summary>
        private void openWorkspaceButton_Click(object sender, EventArgs e)
        {
            workspaceForm ??= new WorkspaceForm();
            workspaceForm.Show();
        }

        /// <summary>
        /// Serializes data.
        /// </summary>
        private void TaskManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (FileStream fs = new FileStream("saveUser.json", FileMode.Create))
            {
                var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                var ser = new DataContractSerializer(typeof(List<User>), dcss);
                
                ser.WriteObject(fs, User.Users);
            }

            using (FileStream fs = new FileStream("saveProject.json", FileMode.Create))
            {
                var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                var ser = new DataContractSerializer(typeof(List<Project>), dcss);
                ser.WriteObject(fs, Project.Projects);
            }
        }
    }
}
