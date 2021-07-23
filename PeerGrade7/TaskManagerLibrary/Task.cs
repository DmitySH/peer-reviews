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
    [KnownType(typeof(User))]
    [KnownType(typeof(Bug))]
    public class Task : Challenge, IAssignable
    {
        [DataMember]
        public List<User> Executors { get; set; }

        [DataMember]
        public int Size { get; set; }

        public Task(string name) : base(name)
        {
            Executors = new List<User>(1);
            Size = 1;
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

        public override string ToString() => Name + " task" + base.ToString();
    }
}