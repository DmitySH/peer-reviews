using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskManagerLibrary
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Challenge))]
    [KnownType(typeof(Epic))]
    [KnownType(typeof(Project))]
    [KnownType(typeof(Story))]
    [KnownType(typeof(Task))]
    [KnownType(typeof(Bug))]
    public class User
    {
        [NonSerialized]
        public static List<User> Users = new List<User>();
        [DataMember]
        public List<Challenge> Challenges { get; set; }
        [DataMember]
        public string Name { get; set; }

        public User(string name)
        {
            Name = name;
            Challenges = new List<Challenge>();
        }

        /// <summary>
        /// Removes user with index.
        /// </summary>
        /// <param name="i"> Index which will be removed. </param>
        public static void RemoveUser(int i)
        {
            Users.RemoveAt(i);
        }

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="name"> User's name. </param>
        public static void CreateUser(string name)
        {
            Users.Add(new User(name));
        }

        public override string ToString() => Name;

        /// <summary>
        /// Removes challenge.
        /// </summary>
        /// <param name="challenge"> Challenge to remove. </param>
        public void RemoveChallenge(Challenge challenge)
        {
            foreach (var userChallenge in Challenges)
            {
                if (userChallenge.Name == challenge.Name)
                {
                    Challenges.Remove(userChallenge);
                    return;
                }
            }
        }
    }
}