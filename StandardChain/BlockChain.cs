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

        public BlockChain()
        {
            _chain = new List<Block<T>>();
            _hasher = SHA512.Create();
        }

        public void AddBlock(T transaction, DateTime timeStamp)
        {
            var hash = _lastBlock?.Hash(_hasher) ?? "";
            var block = new Block<T>(transaction, timeStamp, hash);
            _chain.Add(block);
        }
    }
}
