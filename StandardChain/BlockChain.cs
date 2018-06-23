using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace StandardChain
{
    public class Blockchain<T>
    {
        private readonly BlockList<T> _blockList;
        private readonly HashAlgorithm _hasher;

        public Blockchain(HashAlgorithm hasher)
        {
            if (hasher == null) throw new ArgumentNullException(nameof(hasher));

            _hasher = hasher;
            _blockList = new BlockList<T>();
        }

        public Blockchain<T> FilledFromExistingChain(string serialisedChain)
        {
            if (!_blockList.Empty) throw new InvalidBlockchainException("Cannot restore if already populated");

            var chain = JsonConvert.DeserializeObject<IList<Block<T>>>(serialisedChain);
            BlockHash lastHash = null;
            foreach (var block in chain)
            {
                var blockHash = block.Hash(_hasher);
                if (lastHash != null && blockHash != lastHash) throw new InvalidBlockchainException();

                _blockList.AddBlock(block);

                lastHash = blockHash;
            }
            return this;
        }

        public void AddBlockFromTransaction(T transaction, DateTime timeStamp)
        {
            var hash = _blockList.LastBlock?.Hash(_hasher) ?? BlockHash.Empty;
            var block = new Block<T>(transaction, timeStamp, hash);

            _blockList.AddBlock(block);
        }

        public string Serialised()
        {
            return JsonConvert.SerializeObject(_blockList.Blocks);
        }
    }
}
