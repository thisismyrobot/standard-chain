using Newtonsoft.Json;
using StandardChain.Exceptions;
using StandardChain.Interfaces;
using StandardChain.Serialisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace StandardChain
{
    public class Blockchain<T>
    {
        private readonly BlockStack<T> _blockStack;
        private readonly HashAlgorithm _hashAlgorithm;

        public IReadOnlyList<IBlockchainRecord<T>> History => _blockStack.InCreationOrder.ToArray();
        public BlockHash LastBlockHash => _blockStack.LastBlock.Hash(_hashAlgorithm);
        public int Length => _blockStack.Length;

        public Blockchain(HashAlgorithm hashAlgorithm)
        {
            if (hashAlgorithm == null) throw new ArgumentNullException(nameof(hashAlgorithm));

            _hashAlgorithm = hashAlgorithm;
            _blockStack = new BlockStack<T>();
        }

        public void AddBlock(T payload, DateTime timeStamp)
        {
            var previousHash = _blockStack.Empty ? BlockHash.FirstBlock : _blockStack.LastBlock.Hash(_hashAlgorithm);
            var block = new Block<T>(payload, timeStamp, previousHash);

            _blockStack.AddBlock(block);
        }

        internal void RestoreBlockSequence(IEnumerable<Block<T>> blocks)
        {
            if (blocks == null) throw new ArgumentNullException(nameof(blocks));

            foreach (var block in blocks)
            {
                var expectedPreviousHash = _blockStack.Empty ? BlockHash.FirstBlock : _blockStack.LastBlock.Hash(_hashAlgorithm);
                if (!block.PreviousHash.Equals(expectedPreviousHash)) throw new InvalidBlockchainException();

                AddBlock(block.Payload, block.TimeStamp);
            }
        }

        public bool IsAuthorative(Blockchain<T> candidate)
        {
            if (candidate == null) throw new ArgumentNullException(nameof(candidate));

            if (Length > candidate.Length) return true;

            if (Length == candidate.Length && candidate.LastBlockHash.Equals(LastBlockHash)) return true;

            return false;
        }

        public string Serialised()
        {
            var orderedBlocks = _blockStack
                .InCreationOrder
                .Select(SerialisableBlockConverters<T>.FromBlock);
            return JsonConvert.SerializeObject(orderedBlocks);
        }
    }
}
