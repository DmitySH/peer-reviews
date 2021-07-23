using System;
using System.Runtime.Serialization;

namespace PeerGrade4
{
    /// <summary>
    /// My exceptions for specific way to return from recursion.
    /// </summary>
    [Serializable]
    public class FoundException : Exception
    {
        public FoundException()
        {
        }

        public FoundException(string message) : base(message)
        {
        }

        public FoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}