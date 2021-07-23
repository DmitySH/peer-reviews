using System;
using System.Runtime.Serialization;

namespace TaskManagerLibrary
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Epic))]
    [KnownType(typeof(Project))]
    [KnownType(typeof(Story))]
    [KnownType(typeof(Task))]
    [KnownType(typeof(User))]
    [KnownType(typeof(Bug))]
    public abstract class Challenge
    {
        [DataMember]
        public string DateOfCreation { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        protected string status;
        [DataMember]
        public virtual string Status
        {
            get => status;
            set
            {
                if (value != "opened" && value != "closed" && value != "in progress")
                {
                    throw new ArgumentException("Not correct status");
                }

                status = value;
            }
        }


        protected Challenge(string name)
        {
            Name = name;
            DateOfCreation = $"{DateTime.Today.ToString().Split(' ')[0]} | {DateTime.Now.Hour}:{DateTime.Now.Minute}";
            Status = "opened";
        }

        public override string ToString() => $" challenge was created at {DateOfCreation} is {Status}";
       
    }
}