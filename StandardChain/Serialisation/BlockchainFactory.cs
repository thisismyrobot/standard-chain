using Newtonsoft.Json;
using StandardChain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace StandardChain.Serialisation
{
    public static class BlockchainFactory<T>
    {
        public static Blockchain<T> FromJson(string chainJson, HashAlgorithm hashAlgorithm)
        {
            if (string.IsNullOrWhiteSpace(chainJson)) throw new ArgumentException("Missing chain JSON", nameof(chainJson));
            if (hashAlgorithm == null) throw new ArgumentNullException(nameof(hashAlgorithm));

            var serialisedBlocks = JsonConvert.DeserializeObject<IReadOnlyList<SerialisableBlock<T>>>(chainJson);
            if (serialisedBlocks == null) throw new ArgumentException("Invalid chain JSON", nameof(chainJson));

            return FromBlockSequence(hashAlgorithm, serialisedBlocks);
        }

        /// <summary>
        /// Restore the blocks in order, checking that the resultant chain is the same as the serialised one.
        /// </summary>
        /// <param name="blockchain"></param>
        /// <param name="serialisedBlocks"></param>
        private static Blockchain<T> FromBlockSequence(HashAlgorithm hashAlgorithm, IReadOnlyList<SerialisableBlock<T>> serialisedBlocks)
        {
            var blockchain = new Blockchain<T>(hashAlgorithm);
            var sourceBlocks = serialisedBlocks.Select(SerialisableBlockConverters<T>.ToBlock);
            foreach (var block in sourceBlocks)
            {
                blockchain.AddExistingBlock(block);
            }

            // Check that the final block's hash also matches.
            if (!sourceBlocks.Last().Hash(hashAlgorithm).Equals(blockchain.LastBlockHash))
            {
                throw new InvalidBlockchainException("Json blockchain data did not build the same blockchain");
            }
            return blockchain;
        }
    }
}
