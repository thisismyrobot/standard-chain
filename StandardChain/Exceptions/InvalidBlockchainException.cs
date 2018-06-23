using System;

namespace StandardChain.Exceptions
{
    [Serializable]
    internal class InvalidBlockchainException : Exception
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