using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using MenuControl;
using TaskManagerLibrary;

namespace Manager
{
    /// Documentation ++
    /// An inquisitive reader will notice that the code in this project
    /// does not look very clear, to put it mildly, and there are no explanations
    /// other than documentation.the reason for this is my custom menu.The bottom
    /// line is that without knowing the principle of operation of this one can even
    /// say my pattern is very difficult to understand the code, but understanding the
    /// principle itself, everything should become clearer, so instead of commenting on
    /// each piece of code, I will write this comment.My menu works like this: in Menu.Menus
    /// I put either another Menu or MenuItem, since they are inheritors of MenuParent.
    /// MenuItem-calls the method passed to the constructor.Next, an important point:
    /// in the signature of the delegate by which the method is called, there is a
    ///  MenuItem parameter, which, when the delegate invokes, takes this, that is,
    /// the current MenuItem.This allows you to know which menu I am currently in when
    /// creating methods.And this knowledge allows using the Back property, where the
    /// link to the previous menu is stored, to access any menu before that,
    /// and therefore its index, which means I can track in which project,
    /// in which task, and so on I am now.That is, in fact, this is a tree,
    /// on the nodes of which I run.Sometimes, for convenience, I store an object
    /// in the menu, which is then converted to the required type, so that there
    /// are fewer steps back using back. This is how this menu works. To understand
    /// the code completely, you need to separate the menu creation and methods from
    /// the very beginning, otherwise it is not entirely clear, probably, but you will
    /// not be able to tell more about it, since you need to see it:)
    /// Really hope it was enough for documentation.


    /// <summary>
    /// Main class.
    /// </summary>
    class Application
    {
        /// <summary>
        /// Safe user input for positive integer.
        /// </summary>
        /// <returns> Parsed int. </returns>
        public static int SafeParseInt()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num) || num <= 0)
            {
                MenuParent.PrintInColor("Incorrect input! Try again", ConsoleColor.Red);
            }

            return num;
        }

        /// <summary>
        /// Enter point.
        /// </summary>
        static void Main()
        {
            Console.CursorVisible = false;

            MenuItem addProject = new MenuItem("Add project", AddProject);
            MenuItem addUser = new MenuItem("Add user", AddUser);

            Menu projects = new Menu("Work with projects", new MenuParent[] { addProject }, UpdateProjectList);
            Menu users = new Menu("Work with users", new MenuParent[] { addUser });
            MenuItem close = new MenuItem("Close app with save", SaveAndExit);

            Menu startMenu = new Menu("Start menu", new MenuParent[] { users, projects });

            addProject.Back = projects;
            addUser.Back = users;
            projects.Back = startMenu;
            users.Back = startMenu;
            startMenu.Back = close;

            Load(projects, users);

            startMenu.Create();
        }

        /// <summary>
        /// Saves data and closes program.
        /// </summary>
        static void SaveAndExit()
        {
            try
            {
                using (FileStream fs = new FileStream("saveUserConsole.json", FileMode.Create))
                {
                    var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                    var ser = new DataContractSerializer(typeof(List<User>), dcss);

                    ser.WriteObject(fs, User.Users);
                }

                using (FileStream fs = new FileStream("saveProjectConsole.json", FileMode.Create))
                {
                    var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                    var ser = new DataContractSerializer(typeof(List<Project>), dcss);
                    ser.WriteObject(fs, Project.Projects);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Can't save data...");
                Console.ReadLine();
            }


            Environment.Exit(0);
        }

        /// <summary>
        /// Loads serialized data.
        /// </summary>
        /// <param name="projects"> Menu for projects. </param>
        /// <param name="users"> Menu for users. </param>
        static void Load(Menu projects, Menu users)
        {
            Deserealization();

            LoadUsers(users);

            foreach (var project in Project.Projects)
            {
                Menu seeChallenges = LoadProjects(projects, project, out var nextMenu);

                foreach (var projectChallenge in project.Challenges)
                {
                    if (projectChallenge is Epic epicChallenge)
                    {
                        MenuItem seeInfo = LoadEpicChallenge(projectChallenge, seeChallenges, out Menu seeSubChallenges);

                        foreach (var subChallenge in epicChallenge.SubChallenges)
                        {
                            Menu seeExecutors = LoadSubChallenges(subChallenge, seeInfo, seeSubChallenges);

                            LoadExecutors(subChallenge, seeExecutors);
                        }
                    }
                    else
                    {
                        Menu seeExecutors = LoadChallenge(projectChallenge, seeChallenges);

                        LoadExecutors(projectChallenge, seeExecutors);
                    }
                }

                projects.AddItem(nextMenu);
            }
        }

        /// <summary>
        /// Loads projects.
        /// </summary>
        /// <param name="projects"> Menu for projects. </param>
        /// <param name="project"> Current project. </param>
        /// <param name="nextMenu"> Project's sub menu. </param>
        /// <returns> Menu with challenges. </returns>
        static Menu LoadProjects(Menu projects, Project project, out Menu nextMenu)
        {
            MenuItem addBugChallenge = new MenuItem("Add Bug challenge", AddBugChallenge);
            MenuItem addTaskChallenge = new MenuItem("Add Task challenge", AddTaskChallenge);
            MenuItem addStoryChallenge = new MenuItem("Add Story challenge", AddStoryChallenge);
            MenuItem addEpicChallenge = new MenuItem("Add Epic challenge", AddEpicChallenge);

            Menu typeChallenge = new Menu("Add new challenge", new MenuParent[]
            {
                addBugChallenge, addTaskChallenge,
                addStoryChallenge, addEpicChallenge
            });

            MenuItem byOpened = new MenuItem("Group by opened", GroupByOpened);
            MenuItem byInProgress = new MenuItem("Group by in progress", GroupByInProgress);
            MenuItem byClosed = new MenuItem("Group by closed", GroupByClosed);

            Menu seeChallenges = new Menu("Work with challenges", new MenuParent[] { typeChallenge });
            MenuItem remove = new MenuItem("Remove this project", RemoveProject);
            MenuItem changeName = new MenuItem("Change project's name", ChangeName);
            Menu groupChallenge = new Menu("See grouped challenges", new MenuParent[] { byOpened, byInProgress, byClosed });

            nextMenu = new Menu("Project " + project.Name + ": " + project.Challenges.Count + " challenges",
                new MenuParent[] { seeChallenges, groupChallenge, changeName, remove });
            nextMenu.Object = project;

            byOpened.Back = groupChallenge;
            byClosed.Back = groupChallenge;
            byInProgress.Back = groupChallenge;
            groupChallenge.Back = nextMenu;

            addBugChallenge.Back = typeChallenge;
            addTaskChallenge.Back = typeChallenge;
            addStoryChallenge.Back = typeChallenge;
            addEpicChallenge.Back = typeChallenge;

            typeChallenge.Back = seeChallenges;
            seeChallenges.Back = nextMenu;
            changeName.Back = nextMenu;
            remove.Back = nextMenu;
            nextMenu.Back = projects;
            return seeChallenges;
        }

        /// <summary>
        /// Loads epic challenge.
        /// </summary>
        /// <param name="projectChallenge"> Current challenge. </param>
        /// <param name="seeChallenges"> Menu with challenges. </param>
        /// <param name="seeSubChallenges"> Menu with sub challenges. </param>
        /// <returns> Menu item for info menu. </returns>
        static MenuItem LoadEpicChallenge(Challenge projectChallenge, Menu seeChallenges, out Menu seeSubChallenges)
        {
            MenuItem addTaskSubChallenge = new MenuItem("Add Task challenge", AddTaskSubChallenge);
            MenuItem addStorySubChallenge = new MenuItem("Add Story challenge", AddStorySubChallenge);

            Menu addSubChallenge = new Menu("Add sub challenge",
                new MenuParent[] { addTaskSubChallenge, addStorySubChallenge });
            MenuItem seeInfo = new MenuItem("See information", SeeInfo);
            seeSubChallenges = new Menu("See sub challenges", new MenuParent[] { addSubChallenge });

            MenuItem removeChallenge = new MenuItem("Remove this challenge", RemoveChallenge);

            Menu nextMenuChallenge = new Menu($"Epic {projectChallenge.Name} ",
                new MenuParent[] { seeSubChallenges, seeInfo, removeChallenge });
            nextMenuChallenge.Object = projectChallenge;

            addSubChallenge.Back = seeSubChallenges;
            seeSubChallenges.Back = nextMenuChallenge;
            addTaskSubChallenge.Back = seeSubChallenges;
            addStorySubChallenge.Back = seeSubChallenges;

            seeInfo.Back = nextMenuChallenge;
            removeChallenge.Back = seeChallenges;
            nextMenuChallenge.Back = seeChallenges;

            seeChallenges.AddItem(nextMenuChallenge);
            return seeInfo;
        }

        /// <summary>
        /// Loads sub challenges.
        /// </summary>
        /// <param name="subChallenge"> Current sub challenge. </param>
        /// <param name="seeInfo"> Info menu. </param>
        /// <param name="seeSubChallenges"> Sub challenge menu. </param>
        /// <returns> Menu for executors. </returns>
        static Menu LoadSubChallenges(Challenge subChallenge, MenuItem seeInfo, Menu seeSubChallenges)
        {
            MenuItem toOpened = new MenuItem("Set to opened", ToOpened);
            MenuItem toInProgress = new MenuItem("Set to in progress", ToInProgress);
            MenuItem toClosed = new MenuItem("Set to closed", ToClosed);

            Menu addExecutorList = new Menu("Add executor from registered users", new MenuParent[] { }, UpdateUserList);

            MenuItem seeInfoSub = new MenuItem("See information", SeeInfo);
            Menu seeExecutors = new Menu("See executors", new MenuParent[] { addExecutorList }, UpdateExecutorList);
            Menu changeStatus = new Menu("Change status", new MenuParent[] { toOpened, toInProgress, toClosed });

            MenuItem removeSub = new MenuItem("Remove this challenge", RemoveSubChallenge);

            Menu nextMenuSub = new Menu($"{subChallenge.GetType().ToString().Split('.')[^1]} {subChallenge.Name} ",
                new MenuParent[] { seeExecutors, seeInfo, changeStatus, removeSub });
            nextMenuSub.Object = subChallenge;

            seeExecutors.Back = nextMenuSub;
            addExecutorList.Back = seeExecutors;

            toClosed.Back = changeStatus;
            toInProgress.Back = changeStatus;
            toOpened.Back = changeStatus;

            seeInfoSub.Back = nextMenuSub;
            changeStatus.Back = nextMenuSub;
            removeSub.Back = seeSubChallenges;
            nextMenuSub.Back = seeSubChallenges;

            seeSubChallenges.AddItem(nextMenuSub);
            return seeExecutors;
        }

        /// <summary>
        /// Loads challenge. 
        /// </summary>
        /// <param name="projectChallenge"> Current challenge. </param>
        /// <param name="seeChallenges"> Menu with challenges. </param>
        /// <returns> Menu for executors. </returns>
        static Menu LoadChallenge(Challenge projectChallenge, Menu seeChallenges)
        {
            MenuItem toOpened = new MenuItem("Set to opened", ToOpened);
            MenuItem toInProgress = new MenuItem("Set to in progress", ToInProgress);
            MenuItem toClosed = new MenuItem("Set to closed", ToClosed);

            Menu addExecutorList = new Menu("Add executor from registered users", new MenuParent[] { }, UpdateUserList);

            MenuItem seeInfo = new MenuItem("See information", SeeInfo);
            Menu seeExecutors = new Menu("See executors", new MenuParent[] { addExecutorList }, UpdateExecutorList);
            Menu changeStatus = new Menu("Change status", new MenuParent[] { toOpened, toInProgress, toClosed });

            MenuItem removeChallenge = new MenuItem("Remove this challenge", RemoveChallenge);

            Menu nextMenuChallenge = new Menu($"{projectChallenge.GetType().ToString().Split('.')[^1]} {projectChallenge.Name} ",
                new MenuParent[] { seeExecutors, seeInfo, changeStatus, removeChallenge });
            nextMenuChallenge.Object = projectChallenge;

            seeExecutors.Back = nextMenuChallenge;
            addExecutorList.Back = seeExecutors;

            toClosed.Back = changeStatus;
            toInProgress.Back = changeStatus;
            toOpened.Back = changeStatus;
            seeInfo.Back = nextMenuChallenge;
            changeStatus.Back = nextMenuChallenge;
            removeChallenge.Back = seeChallenges;
            nextMenuChallenge.Back = seeChallenges;
            seeChallenges.AddItem(nextMenuChallenge);
            return seeExecutors;
        }

        /// <summary>
        /// Loads executors.
        /// </summary>
        /// <param name="subChallenge"> Current sub challenge. </param>
        /// <param name="seeExecutors"> Executor's menu. </param>
        static void LoadExecutors(Challenge subChallenge, Menu seeExecutors)
        {
            foreach (var executor in (subChallenge as IAssignable).Executors)
            {
                MenuItem removeExecutor = new MenuItem("Remove executor", RemoveExecutor);

                Menu executorMenu = new Menu(executor.Name, new MenuParent[] { removeExecutor });
                seeExecutors.AddItem(executorMenu);

                executorMenu.Back = seeExecutors;
                removeExecutor.Back = executorMenu;
            }
        }

        /// <summary>
        /// Loads users.
        /// </summary>
        /// <param name="users"> Menu with users. </param>
        static void LoadUsers(Menu users)
        {
            foreach (var user in User.Users)
            {
                MenuItem remove = new MenuItem("Remove this user", RemoveUser);
                Menu nextMenu = new Menu("User " + user.Name, new MenuParent[] { remove });
                nextMenu.Object = user;

                remove.Back = nextMenu;
                nextMenu.Back = users;
                users.AddItem(nextMenu);
            }
        }

        /// <summary>
        /// Deserializes data.
        /// </summary>
        static void Deserealization()
        {
            try
            {
                using (FileStream fs = new FileStream("saveUserConsole.json", FileMode.Open))
                {
                    var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                    var ser = new DataContractSerializer(typeof(List<User>), dcss);
                    User.Users = ser.ReadObject(fs) as List<User>;
                }

                using (FileStream fs = new FileStream("saveProjectConsole.json", FileMode.Open))
                {
                    var dcss = new DataContractSerializerSettings { PreserveObjectReferences = true };

                    var ser = new DataContractSerializer(typeof(List<Project>), dcss);
                    Project.Projects = ser.ReadObject(fs) as List<Project>;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Could not load data...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Changes name of project. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void ChangeName(MenuParent menu)
        {
            Console.Clear();
            Console.Write("Enter new name: ");
            Project.Projects[menu.Back.Back.Index].Name = Console.ReadLine();
            menu.Back.Back.Menus[menu.Back.Back.Index].Text = $"Project {Project.Projects[menu.Back.Back.Index].Name}: " +
                                                              $"{Project.Projects[menu.Back.Back.Index].Challenges.Count} challenges";
        }

        /// <summary>
        /// Removes challenge.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void RemoveChallenge(MenuParent menu)
        {
            (menu.Back.Back.Object as Project).Challenges.RemoveAt(menu.Back.Index);
            menu.Back.Menus.RemoveAt(menu.Back.Index);
            menu.Back.Create();
        }

        /// <summary>
        /// Removes sub challenge.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void RemoveSubChallenge(MenuParent menu)
        {
            (menu.Back.Back.Object as Epic).SubChallenges.RemoveAt(menu.Back.Index);
            menu.Back.Menus.RemoveAt(menu.Back.Index);
            menu.Back.Create();
        }

        /// <summary>
        /// Set status to opened.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void ToOpened(MenuParent menu)
        {
            (menu.Back.Back.Object as Challenge).Status = "opened";
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Set status to in progress. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void ToInProgress(MenuParent menu)
        {
            (menu.Back.Back.Object as Challenge).Status = "in progress";
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Set status to closed. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void ToClosed(MenuParent menu)
        {
            (menu.Back.Back.Object as Challenge).Status = "closed";
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Gives info about challenge. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void SeeInfo(MenuParent menu)
        {
            Console.Clear();
            Console.WriteLine((menu.Back.Object as Challenge).ToString());
            Console.ReadLine();
        }

        /// <summary>
        /// Removes executor.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void RemoveExecutor(MenuParent menu)
        {
            (menu.Back.Back.Back.Object as IAssignable).Executors.RemoveAt(menu.Back.Back.Index);
            menu.Back.Back.Menus.RemoveAt(menu.Back.Back.Index);
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Add new executor. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddExecutor(MenuParent menu)
        {
            if ((menu.Back.Back.Back.Object as IAssignable).Executors.Count ==
                (menu.Back.Back.Back.Object as IAssignable).Size)
            {
                Console.Clear();
                MenuParent.PrintInColor("This challenge has maximum of executors", ConsoleColor.Red);
                Console.ReadLine();
                return;
            }

            foreach (var executor in (menu.Back.Back.Back.Object as IAssignable).Executors)
            {
                if (executor.Name.Equals((menu.Object as User).Name))
                {
                    Console.Clear();
                    MenuParent.PrintInColor("This executor is already attached to this task", ConsoleColor.Red);
                    Console.ReadLine();
                    return;
                }
            }

            MenuItem removeExecutor = new MenuItem("Remove executor", RemoveExecutor);

            Menu executorMenu = new Menu((menu.Object as User).Name, new MenuParent[] { removeExecutor });
            menu.Back.Back.AddItem(executorMenu);

            executorMenu.Back = menu.Back.Back;
            removeExecutor.Back = executorMenu;
            (menu.Back.Back.Back.Object as IAssignable).Executors.Add(menu.Object as User);
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Creates challenge's menu.
        /// </summary>
        /// <param name="menu"> Previous menu. </param>
        /// <param name="name"> Challenge's name. </param>
        static void CreateMenuForChallenge(MenuParent menu, string name)
        {
            MenuItem toOpened = new MenuItem("Set to opened", ToOpened);
            MenuItem toInProgress = new MenuItem("Set to in progress", ToInProgress);
            MenuItem toClosed = new MenuItem("Set to closed", ToClosed);

            Menu addExecutorList = new Menu("Add executor from registered users", new MenuParent[] { }, UpdateUserList);

            MenuItem seeInfo = new MenuItem("See information", SeeInfo);
            Menu seeExecutors = new Menu("See executors", new MenuParent[] { addExecutorList }, UpdateExecutorList);
            Menu changeStatus = new Menu("Change status", new MenuParent[] { toOpened, toInProgress, toClosed });

            MenuItem remove = new MenuItem("Remove this challenge", RemoveChallenge);

            Menu nextMenu = new Menu($"{name} ", new MenuParent[] { seeExecutors, seeInfo, changeStatus, remove });
            nextMenu.Object = (menu.Back.Back.Back.Object as Project).Challenges[^1];

            seeExecutors.Back = nextMenu;
            addExecutorList.Back = seeExecutors;

            toClosed.Back = changeStatus;
            toInProgress.Back = changeStatus;
            toOpened.Back = changeStatus;
            seeInfo.Back = nextMenu;
            changeStatus.Back = nextMenu;
            remove.Back = menu.Back.Back;
            nextMenu.Back = menu.Back.Back;
            menu.Back.Back.AddItem(nextMenu);
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Updates user list every rendering.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void UpdateUserList(MenuParent menu)
        {
            if (User.Users.Count == 0)
            {
                Console.Clear();
                MenuParent.PrintInColor("There is no registered users", ConsoleColor.Red);
                Console.ReadLine();
                menu.Back.Create();
            }

            menu.Menus = new List<MenuParent>();
            foreach (var user in User.Users)
            {
                MenuItem addExecutor = new MenuItem(user.Name, AddExecutor);
                addExecutor.Object = user;
                addExecutor.Back = menu;
                menu.Menus.Add(addExecutor);
            }

            if (menu.Index > menu.Menus.Count || menu.Index < 0)
            {
                menu.Index = 0;
            }
        }

        /// <summary>
        /// Updates project list every rendering.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void UpdateProjectList(MenuParent menu)
        {
            for (int i = 0; i < menu.Menus.Count - 1; i++)
            {
                menu.Menus[i].Text = $"Project {Project.Projects[i].Name}: {Project.Projects[i].Challenges.Count} challenges";
            }

        }

        /// <summary>
        /// Updates executor list every rendering. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void UpdateExecutorList(MenuParent menu)
        {
            for (int i = 0; i < menu.Menus.Count; i++)
            {
                bool exists = menu.Menus[i].Text.Equals("Add executor from registered users");

                foreach (var user in User.Users)
                {
                    if (menu.Menus[i].Text.Equals(user.Name) ||
                        menu.Menus[i].Text.Equals("Add executor from registered users"))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    menu.Menus.RemoveAt(i);
                    if (menu.Index > menu.Menus.Count || menu.Index < 0)
                    {
                        menu.Index = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Adds bug challenge.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddBugChallenge(MenuParent menu)
        {
            if (MaxChallenges(menu))
            {
                return;
            }

            Console.Clear();
            Console.Write("Enter name for Bug: ");
            string name = Console.ReadLine();
            (menu.Back.Back.Back.Object as Project).AddBugChallenge(name);
            name = "Bug " + name;

            CreateMenuForChallenge(menu, name);
        }

        /// <summary>
        /// Adds task challenge. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddTaskChallenge(MenuParent menu)
        {
            if (MaxChallenges(menu))
            {
                return;
            }

            Console.Clear();
            Console.Write("Enter name for Task: ");
            string name = Console.ReadLine();
            (menu.Back.Back.Back.Object as Project).AddTaskChallenge(name);
            name = "Task " + name;
            CreateMenuForChallenge(menu, name);
        }

        /// <summary>
        /// Adds story challenge.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddStoryChallenge(MenuParent menu)
        {
            if (MaxChallenges(menu))
            {
                return;
            }

            Console.Clear();
            Console.Write("Enter name for Story: ");
            string name = Console.ReadLine();
            Console.Write("Enter number of executors: ");
            (menu.Back.Back.Back.Object as Project).AddStoryChallenge(SafeParseInt(), name);
            name = "Story " + name;
            CreateMenuForChallenge(menu, name);
        }

        /// <summary>
        /// Creates sub challenge's menu.
        /// </summary>
        /// <param name="menu"> Previous menu. </param>
        /// <param name="name"> Name of sub challenge. </param>
        static void CreateMenuForSubChallenge(MenuParent menu, string name)
        {
            MenuItem toOpened = new MenuItem("Set to opened", ToOpened);
            MenuItem toInProgress = new MenuItem("Set to in progress", ToInProgress);
            MenuItem toClosed = new MenuItem("Set to closed", ToClosed);

            Menu addExecutorList = new Menu("Add executor from registered users", new MenuParent[] { }, UpdateUserList);

            MenuItem seeInfo = new MenuItem("See information", SeeInfo);
            Menu seeExecutors = new Menu("See executors", new MenuParent[] { addExecutorList }, UpdateExecutorList);
            Menu changeStatus = new Menu("Change status", new MenuParent[] { toOpened, toInProgress, toClosed });

            MenuItem remove = new MenuItem("Remove this challenge", RemoveSubChallenge);

            Menu nextMenu = new Menu($"{name} ", new MenuParent[] { seeExecutors, seeInfo, changeStatus, remove });
            nextMenu.Object = (menu.Back.Back.Object as Epic).SubChallenges[^1];

            seeExecutors.Back = nextMenu;
            addExecutorList.Back = seeExecutors;

            toClosed.Back = changeStatus;
            toInProgress.Back = changeStatus;
            toOpened.Back = changeStatus;
            seeInfo.Back = nextMenu;
            changeStatus.Back = nextMenu;
            remove.Back = menu.Back;
            nextMenu.Back = menu.Back;

            menu.Back.AddItem(nextMenu);
            menu.Back.Create();
        }

        /// <summary>
        /// Adds task sub challenge. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddTaskSubChallenge(MenuParent menu)
        {
            Console.Clear();
            Console.Write("Enter name for Task: ");
            string name = Console.ReadLine();
            (menu.Back.Back.Object as Epic).SubChallenges.Add(new TaskManagerLibrary.Task(name));
            name = "Task " + name;

            CreateMenuForSubChallenge(menu, name);
        }

        /// <summary>
        /// Adds story sub challenge. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddStorySubChallenge(MenuParent menu)
        {
            Console.Clear();
            Console.Write("Enter name for Story: ");
            string name = Console.ReadLine();
            Console.Write("Enter number of executors: ");
            (menu.Back.Back.Object as Epic).SubChallenges.Add(new Story(name, SafeParseInt()));
            name = "Story " + name;

            CreateMenuForSubChallenge(menu, name);
        }

        /// <summary>
        /// Adds epic challenge.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddEpicChallenge(MenuParent menu)
        {
            if (MaxChallenges(menu))
            {
                return;
            }

            Console.Clear();
            Console.Write("Enter name for Epic: ");
            string name = Console.ReadLine();
            (menu.Back.Back.Back.Object as Project).AddEpicChallenge(name);
            name = "Epic " + name;

            MenuItem addTaskSubChallenge = new MenuItem("Add Task challenge", AddTaskSubChallenge);
            MenuItem addStorySubChallenge = new MenuItem("Add Story challenge", AddStorySubChallenge);

            Menu addSubChallenge = new Menu("Add sub challenge",
                new MenuParent[] { addTaskSubChallenge, addStorySubChallenge });

            MenuItem seeInfo = new MenuItem("See information", SeeInfo);
            Menu seeSubChallenges = new Menu("See sub challenges", new MenuParent[] { addSubChallenge });

            MenuItem remove = new MenuItem("Remove this challenge", RemoveChallenge);

            Menu nextMenu = new Menu($"{name} ", new MenuParent[] { seeSubChallenges, seeInfo, remove });
            nextMenu.Object = (menu.Back.Back.Back.Object as Project).Challenges[^1];

            addSubChallenge.Back = seeSubChallenges;
            seeSubChallenges.Back = nextMenu;
            addTaskSubChallenge.Back = seeSubChallenges;
            addStorySubChallenge.Back = seeSubChallenges;

            seeInfo.Back = nextMenu;
            remove.Back = menu.Back.Back;
            nextMenu.Back = menu.Back.Back;
            menu.Back.Back.AddItem(nextMenu);
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Checks count of challenges.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        /// <returns> True if there is maximum of challenges. </returns>
        static bool MaxChallenges(MenuParent menu)
        {
            if ((menu.Back.Back.Back.Object as Project).Size ==
                (menu.Back.Back.Back.Object as Project).Challenges.Count)
            {
                Console.Clear();
                MenuParent.PrintInColor("This project has maximum of challenges", ConsoleColor.Red);
                Console.ReadLine();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Prints opened challeges.
        /// </summary>
        /// <param name="menu"></param>
        static void GroupByOpened(MenuParent menu)
        {
            Console.WriteLine(Group("opened", menu.Back.Back.Object as Project));
            Console.ReadLine();
        }

        /// <summary>
        /// Prints in progress challenges.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void GroupByInProgress(MenuParent menu)
        {
            Console.WriteLine(Group("in progress", menu.Back.Back.Object as Project));
            Console.ReadLine();
        }

        /// <summary>
        /// Prints closed challenges.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void GroupByClosed(MenuParent menu)
        {
            Console.WriteLine(Group("closed", menu.Back.Back.Object as Project));
            Console.ReadLine();
        }

        /// <summary>
        /// Finds all challenges with status.
        /// </summary>
        /// <param name="status"> Status to find. </param>
        /// <param name="project"> Project where to find. </param>
        /// <returns> String with all challenges with status. </returns>
        static string Group(string status, Project project)
        {
            string info = $"All {status} challenges in this project:{Environment.NewLine}";
            foreach (var challenge in project.Challenges)
            {
                if (challenge.Status.Equals(status))
                {
                    info += challenge.Name + Environment.NewLine;
                }
            }

            if (info.Equals($"All {status} challenges in this project:{Environment.NewLine}"))
            {
                return $"There is no {status} challenges in this project";
            }
            else
            {
                return info;
            }
        }

        /// <summary>
        /// Adds new project. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddProject(MenuParent menu)
        {
            Console.Clear();
            Console.Write("Enter project's name: ");
            string name = Console.ReadLine();
            Console.Write("Enter number of challenges for this project: ");
            Project.Projects.Add(new Project(SafeParseInt(), name));

            MenuItem addBugChallenge = new MenuItem("Add Bug challenge", AddBugChallenge);
            MenuItem addTaskChallenge = new MenuItem("Add Task challenge", AddTaskChallenge);
            MenuItem addStoryChallenge = new MenuItem("Add Story challenge", AddStoryChallenge);
            MenuItem addEpicChallenge = new MenuItem("Add Epic challenge", AddEpicChallenge);

            Menu typeChallenge = new Menu("Add new challenge", new MenuParent[]{addBugChallenge,addTaskChallenge,
                addStoryChallenge, addEpicChallenge});
            MenuItem byOpened = new MenuItem("Group by opened", GroupByOpened);
            MenuItem byInProgress = new MenuItem("Group by in progress", GroupByInProgress);
            MenuItem byClosed = new MenuItem("Group by closed", GroupByClosed);

            Menu seeChallenges = new Menu("Work with challenges", new MenuParent[] { typeChallenge });
            MenuItem remove = new MenuItem("Remove this project", RemoveProject);
            MenuItem changeName = new MenuItem("Change project's name", ChangeName);
            Menu groupChallenge = new Menu("See grouped challenges", new MenuParent[] { byOpened, byInProgress, byClosed });

            Menu nextMenu = new Menu($"Project {Project.Projects[^1].Name}: {Project.Projects[^1].Challenges.Count} challenges"
                , new MenuParent[] { seeChallenges, groupChallenge, changeName, remove });
            nextMenu.Object = Project.Projects[^1];

            byOpened.Back = groupChallenge;
            byClosed.Back = groupChallenge;
            byInProgress.Back = groupChallenge;
            groupChallenge.Back = nextMenu;

            addBugChallenge.Back = typeChallenge;
            addTaskChallenge.Back = typeChallenge;
            addStoryChallenge.Back = typeChallenge;
            addEpicChallenge.Back = typeChallenge;

            typeChallenge.Back = seeChallenges;
            seeChallenges.Back = nextMenu;
            changeName.Back = nextMenu;
            remove.Back = nextMenu;
            nextMenu.Back = menu.Back;

            menu.Back.AddItem(nextMenu);
        }

        /// <summary>
        /// Removes project. 
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void RemoveProject(MenuParent menu)
        {
            Project.Projects.RemoveAt(menu.Back.Back.Index);
            menu.Back.Back.Menus.RemoveAt(menu.Back.Back.Index);
            menu.Back.Back.Create();
        }

        /// <summary>
        /// Adds new user.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void AddUser(MenuParent menu)
        {
            Console.Clear();
            Console.Write("Enter user's name: ");
            string name = Console.ReadLine();
            foreach (var user in User.Users)
            {
                if (user.Name.Equals(name))
                {
                    MenuParent.PrintInColor("User with this name is already exists", ConsoleColor.Red);
                    Console.ReadLine();
                    return;
                }
            }

            User.Users.Add(new User(name));

            MenuItem remove = new MenuItem("Remove this user", RemoveUser);
            Menu nextMenu = new Menu("User " + User.Users[^1].Name, new MenuParent[] { remove });
            nextMenu.Object = User.Users[^1];

            remove.Back = nextMenu;
            nextMenu.Back = menu.Back;
            menu.Back.AddItem(nextMenu);
        }

        /// <summary>
        /// Removes user.
        /// </summary>
        /// <param name="menu"> Current menu. </param>
        static void RemoveUser(MenuParent menu)
        {
            foreach (var project in Project.Projects)
            {
                foreach (var projectChallenge in project.Challenges)
                {
                    if (projectChallenge is Epic epicProjectChallenge)
                    {
                        foreach (var subChallenge in epicProjectChallenge.SubChallenges)
                        {
                            for (int i = 0; i < (subChallenge as IAssignable).Executors.Count; i++)
                            {
                                if ((subChallenge as IAssignable).Executors[i].Name
                                    .Equals(User.Users[menu.Back.Back.Index].Name))
                                {
                                    (subChallenge as IAssignable).Executors.RemoveAt(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < (projectChallenge as IAssignable).Executors.Count; i++)
                        {
                            if ((projectChallenge as IAssignable).Executors[i].Name
                                .Equals(User.Users[menu.Back.Back.Index].Name))
                            {
                                (projectChallenge as IAssignable).Executors.RemoveAt(i);
                            }
                        }
                    }
                }
            }

            User.Users.RemoveAt(menu.Back.Back.Index);
            menu.Back.Back.Menus.RemoveAt(menu.Back.Back.Index);
            menu.Back.Back.Create();
        }
    }
}