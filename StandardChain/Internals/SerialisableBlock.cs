using System;

namespace StandardChain
{
    internal class SerialisableBlock<T> : IBlockchainRecord<T>
    {
        public DateTime Timestamp { get; }
        public T Payload { get; }
        public string PreviousHashValue { get; }

        public SerialisableBlock(T payload, DateTime timeStamp, string previousHashValue)
        {
            Payload = payload;
            Timestamp = timeStamp;
            PreviousHashValue = previousHashValue;
        }
    }
}