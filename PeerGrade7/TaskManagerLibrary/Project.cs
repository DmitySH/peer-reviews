using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskManagerLibrary
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Challenge))]
    [KnownType(typeof(Epic))]
    [KnownType(typeof(Story))]
    [KnownType(typeof(Task))]
    [KnownType(typeof(User))]
    [KnownType(typeof(Bug))]
    public class Project
    {
        public Project()
        {

        }
        [DataMember]
        public static List<Project> Projects = new List<Project>();

        [DataMember]
        public List<Challenge> Challenges { get; set; }

        [DataMember]
        public int Size { get; set; }
        [DataMember]
        public string Name { get; set; }

        public Project(int size, string name)
        {
            Name = name;
            Size = size;
            Challenges = new List<Challenge>(Size);
        }

        /// <summary>
        /// Removes project with index.
        /// </summary>
        /// <param name="i"> Index. </param>
        public static void RemoveProject(int i)
        {
            Projects.RemoveAt(i);
        }

        /// <summary>
        /// Creates new project. 
        /// </summary>
        /// <param name="name"> Project's name. </param>
        /// <param name="numberOfChallenges"> Project's number of challenges. </param>
        public static void CreateProject(string name, int numberOfChallenges)
        {
            Projects.Add(new Project(numberOfChallenges, name));
        }

        /// <summary>
        /// Renames project. 
        /// </summary>
        /// <param name="name"> Project's new name. </param>
        /// <param name="i"> Index of project. </param>
        public static void RenameProject(string name, int i)
        {
            Projects[i].Name = name;
        }

        /// <summary>
        /// Adds epic challenge.
        /// </summary>
        /// <param name="name"> Challenge's name. </param>
        public void AddEpicChallenge(string name)
        {
            if (Size != Challenges.Count)
            {
                Challenges.Add(new Epic(name));
            }
        }

        /// <summary>
        /// Adds story challenge.
        /// </summary>
        /// <param name="number"> Max number of executors. </param>
        /// <param name="name"> Challenge's name. </param>
        public void AddStoryChallenge(int number, string name)
        {
            if (Size != Challenges.Count)
            {
                Challenges.Add(new Story(name, number));
            }
        }

        /// <summary>
        /// Add task challenge.
        /// </summary>
        /// <param name="name"> Challenge's name. </param>
        public void AddTaskChallenge(string name)
        {
            if (Size != Challenges.Count)
            {
                Challenges.Add(new Task(name));
            }
        }

        /// <summary>
        /// Add bug challenge. 
        /// </summary>
        /// <param name="name"> Challenge's name. </param>
        public void AddBugChallenge(string name)
        {
            if (Size != Challenges.Count)
            {
                Challenges.Add(new Bug(name));
            }
        }
    }
}
