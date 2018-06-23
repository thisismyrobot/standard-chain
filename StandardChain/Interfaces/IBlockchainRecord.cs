using System;

namespace StandardChain.Interfaces
{
    public interface IBlockchainRecord<T>
    {
        DateTime TimeStamp { get; }
        T Payload { get; }
    }
}