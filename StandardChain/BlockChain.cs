using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace StandardChain
{
    public class BlockChain<T>
    {
        private Block<T> _lastBlock => _chain.LastOrDefault();

        private readonly IList<Block<T>> _chain;
        private readonly HashAlgorithm _hasher;

        public BlockChain(HashAlgorithm hasher)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _chain = new List<Block<T>>();
        }

        public BlockChain(string serialisedChain)
        {
            var chain = JsonConvert.DeserializeObject<IList<Block<T>>>(serialisedChain);
            string lastHash = null;
            foreach(var block in chain)
            {
                var blockHash = block.Hash(_hasher);
                if (lastHash != null && blockHash != lastHash) throw new InvalidBlockChainException();

                chain.Add(block);

                lastHash = blockHash;
            }
        }

        public void AddBlock(T transaction, DateTime timeStamp)
        {
            var hash = _lastBlock?.Hash(_hasher) ?? "";
            var block = new Block<T>(transaction, timeStamp, hash);
            _chain.Add(block);
        }

        public string Serialise()
        {
            return JsonConvert.SerializeObject(_chain);
        }
    }
}
