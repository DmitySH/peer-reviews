using System;
using System.Runtime.Serialization;

namespace PeerGrade4
{
    /// <summary>
    /// My exceptions for files.
    /// </summary>
    [Serializable]
    public class FileException : Exception
    {
        public FileException()
        {
        }

        public FileException(string message) : base(message)
        {
        }

        public FileException(string message, Exception inner) : base(message, inner)
        {
        }

        protected FileException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}