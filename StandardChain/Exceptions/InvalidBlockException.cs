using System;

namespace StandardChain
{
    [Serializable]
    public class InvalidBlockException : Exception
    {
        public InvalidBlockException()
        {
        }

        public InvalidBlockException(string message) : base(message)
        {
        }

        public InvalidBlockException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}