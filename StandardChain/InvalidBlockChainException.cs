using System;

namespace StandardChain
{
    [Serializable]
    internal class InvalidBlockChainException : Exception
    {
        public InvalidBlockChainException()
        {
        }

        public InvalidBlockChainException(string message) : base(message)
        {
        }

        public InvalidBlockChainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}