using StandardChain.Interfaces;
using System;

namespace StandardChain.Serialisation
{
    internal class SerialisableBlock<T> : IBlockchainRecord<T>
    {
        public DateTime TimeStamp { get; }
        public T Payload { get; }
        public string PreviousHashValue { get; }

        public SerialisableBlock(T payload, DateTime timeStamp, string previousHashValue)
        {
            Payload = payload;
            TimeStamp = timeStamp;
            PreviousHashValue = previousHashValue;
        }
    }
}