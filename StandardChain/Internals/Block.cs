using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace StandardChain
{
    internal class Block<T> : IBlockchainRecord<T>
    {
        public DateTime Timestamp { get; }
        public T Payload { get; }
        internal BlockHash PreviousHash { get; }

        internal Block(T payload, DateTime timeStamp, BlockHash previousHash)
        {
            if (previousHash == null) throw new ArgumentNullException(nameof(previousHash));

            Payload = payload;
            Timestamp = timeStamp;
            PreviousHash = previousHash;
        }

        internal BlockHash Hash(HashAlgorithm hashAlgorithm)
        {
            var blockAsJson = JsonConvert.SerializeObject(this);
            var hashBytes = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(blockAsJson));
            return new BlockHash(hashBytes);
        }
    }
}
