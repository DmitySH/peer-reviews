using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TaskManagerLibrary
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Challenge))]
    [KnownType(typeof(Project))]
    [KnownType(typeof(Story))]
    [KnownType(typeof(Task))]
    [KnownType(typeof(User))]
    [KnownType(typeof(Bug))]
    public class Epic : Challenge
    {
        [DataMember]
        public List<Challenge> SubChallenges { get; set; }

        /// <summary>
        /// Status of epic depends on sub challenges' status.
        /// </summary>
        [DataMember]
        public override string Status
        {
            get
            {
                if (SubChallenges.Count == 0)
                {
                    return "opened";
                }

                bool isClosed = true;
                foreach (var subChallenge in SubChallenges)
                {
                    if (subChallenge.Status != "closed")
                    {
                        isClosed = false;
                        break;
                    }
                }

                if (isClosed)
                {
                    return "closed";
                }

                bool isOpened = true;
                foreach (var subChallenge in SubChallenges)
                {
                    if (subChallenge.Status != "opened")
                    {
                        isOpened = false;
                        break;
                    }
                }

                if (isOpened)
                {
                    return "opened";
                }

                return "in progress";
            }
            set
            {
                if (value != "opened" && value != "closed" && value != "in progress")
                {
                    throw new ArgumentException("Not correct status");
                }

                status = value;
            }

        }

        public Epic(string name) : base(name)
        {
            SubChallenges = new List<Challenge>();
        }
        public override string ToString() => Name + " epic" + base.ToString();
    }

}
