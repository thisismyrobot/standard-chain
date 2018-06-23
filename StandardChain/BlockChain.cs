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
        private readonly BlockList<T> _blockList;
        private readonly HashAlgorithm _hashAlgorithm;

        public IReadOnlyList<IBlockchainRecord<T>> History => (IReadOnlyList<IBlockchainRecord<T>>)_blockList.Blocks;
        public int Length => _blockList.Length;

        public Blockchain(HashAlgorithm hashAlgorithm)
        {
            if (hashAlgorithm == null) throw new ArgumentNullException(nameof(hashAlgorithm));

            _hashAlgorithm = hashAlgorithm;
            _blockList = new BlockList<T>();
        }

        public void AddBlock(T payload, DateTime timeStamp)
        {
            var previousHash = _blockList.Empty ? BlockHash.FirstBlock : _blockList.LastBlock.Hash(_hashAlgorithm);
            var block = new Block<T>(payload, timeStamp, previousHash);

            _blockList.AddBlock(block);
        }

        internal void RestoreBlockSequence(IEnumerable<Block<T>> blocks)
        {
            if (blocks == null) throw new ArgumentNullException(nameof(blocks));

            foreach (var block in blocks)
            {
                var expectedPreviousHash = _blockList.Empty ? BlockHash.FirstBlock : _blockList.LastBlock.Hash(_hashAlgorithm);
                if (!block.PreviousHash.Equals(expectedPreviousHash)) throw new InvalidBlockchainException();

                AddBlock(block.Payload, block.TimeStamp);
            }
        }

        public string Serialised()
        {
            return JsonConvert.SerializeObject(_blockList.Blocks.Select(SerialisableBlockConverters<T>.FromBlock));
        }
    }
}
