using System;

namespace StandardChain
{
    [Serializable]
    public class InvalidBlockchainException : Exception
    {
        public InvalidBlockchainException()
        {
        }

        public InvalidBlockchainException(string message) : base(message)
        {
        }

        public InvalidBlockchainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}