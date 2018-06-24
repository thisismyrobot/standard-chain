using System;

namespace StandardChain
{
    public interface IBlockchainRecord<T>
    {
        DateTime Timestamp { get; }
        T Payload { get; }
    }
}