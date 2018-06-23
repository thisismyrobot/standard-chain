using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace StandardChain
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class Block<T>
    {
        [JsonProperty]
        internal DateTime TimeStamp { get; }

        [JsonProperty]
        internal T Transaction { get; }

        [JsonProperty]
        internal string PreviousHash { get; }

        internal Block(T transaction, DateTime timeStamp, BlockHash previousHash)
        {
            if (previousHash == null) throw new ArgumentNullException(nameof(previousHash));

            TimeStamp = timeStamp;
            Transaction = transaction;
            PreviousHash = previousHash.Value;
        }

        [JsonConstructor]
        internal Block(T transaction, DateTime timeStamp, string previousHash) :
            this(transaction, timeStamp, new BlockHash(previousHash))
        {
        }

        internal BlockHash Hash(HashAlgorithm hasher)
        {
            var blockAsJson = JsonConvert.SerializeObject(this);
            var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(blockAsJson));
            return new BlockHash(hashBytes);
        }
    }
}
