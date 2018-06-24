using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;

namespace StandardChain
{
    public class Blockchain<T>
    {
        private readonly BlockStack<T> _blockStack;
        private readonly HashAlgorithm _hashAlgorithm;

        internal IReadOnlyList<Block<T>> BlocksInCreationOrder => _blockStack.InCreationOrder.ToArray();

        [SuppressMessage("Microsoft.Design", "CA1006", Justification = "Not sure of non-generic alternative")]
        public IReadOnlyList<IBlockchainRecord<T>> HistoryInCreationOrder => _blockStack.InCreationOrder.ToArray();
        public BlockHash LastBlockHash => _blockStack.LastBlock?.Hash(_hashAlgorithm);
        public int Length => _blockStack.Length;
        public bool Empty => _blockStack.Empty;

        public Blockchain(HashAlgorithm hashAlgorithm)
        {
            if (hashAlgorithm == null) throw new ArgumentNullException(nameof(hashAlgorithm));

            _hashAlgorithm = hashAlgorithm;
            _blockStack = new BlockStack<T>();
        }

        public Blockchain<T> AddBlock(T payload, DateTime timestamp)
        {
            var previousHash = _blockStack.Empty ? BlockHash.FirstBlock : _blockStack.LastBlock.Hash(_hashAlgorithm);
            var block = new Block<T>(payload, timestamp, previousHash);

            _blockStack.AddBlock(block);
            return this;
        }

        public bool IsAuthoritative(Blockchain<T> candidate)
        {
            if (candidate == null) throw new ArgumentNullException(nameof(candidate));

            if (Length > candidate.Length) return true;

            if (Length == candidate.Length && candidate.LastBlockHash.Equals(LastBlockHash)) return true;

            return false;
        }

        internal void AddExistingBlock(Block<T> block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            // Each new block should have a "previous hash" that matches the hash of the blockchain's last
            // added block. If it doesn't it isn't a legitimate existing block.
            var expectedPreviousHash = Empty ? BlockHash.FirstBlock : LastBlockHash;
            if (!block.PreviousHash.Equals(expectedPreviousHash)) throw new InvalidBlockException();

            AddBlock(block.Payload, block.Timestamp);
        }
    }
}
