using System;
using System.Runtime.Serialization;


namespace PeerGrade3
{
    [Serializable]
    public class MatrixException : Exception
    {
        public MatrixException()
        {
        }

        public MatrixException(string message) : base(message)
        {
        }

        public MatrixException(string message, Exception inner) : base(message, inner)
        {
        }


        protected MatrixException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}