using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskManagerLibrary
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Challenge))]
    [KnownType(typeof(Epic))]
    [KnownType(typeof(Project))]
    [KnownType(typeof(Task))]
    [KnownType(typeof(User))]
    [KnownType(typeof(Bug))]
    public class Story : Challenge, IAssignable
    {
        [DataMember]
        public List<User> Executors { get; set; }

        [DataMember]
        public int Size { get; set; }

        public Story(string name, int numberOfExecutors) : base(name)
        {
            Executors = new List<User>(numberOfExecutors);
            Size = numberOfExecutors;
        }

        /// <summary>
        /// Adds executor.
        /// </summary>
        /// <param name="executor"> User to become executor. </param>
        public void AddExecutor(User executor)
        {
            if (Executors.Contains(executor))
            {
                return;
            }
            Executors.Add(executor);
        }

        /// <summary>
        /// Removes executor.
        /// </summary>
        /// <param name="executor"> User to remove. </param>
        public void RemoveExecutor(User executor)
        {
            Executors.Remove(executor);
        }

        public override string ToString() => Name + " story" + base.ToString();
    }
}