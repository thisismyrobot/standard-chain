using Newtonsoft.Json;
using StandardChain.Serialisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace StandardChain
{
    public static class BlockchainFactory<T>
    {
        public static Blockchain<T> FromJson(string chainJson, HashAlgorithm hashAlgorithm)
        {
            if (string.IsNullOrWhiteSpace(chainJson)) throw new ArgumentException("Missing chain JSON", nameof(chainJson));
            if (hashAlgorithm == null) throw new ArgumentNullException(nameof(hashAlgorithm));

            var serialisedBlocks = JsonConvert.DeserializeObject<IReadOnlyList<SerialisableBlock<T>>>(chainJson);
            if (serialisedBlocks == null) throw new ArgumentException("Invalid chain JSON", nameof(chainJson));

            var blockchain = new Blockchain<T>(hashAlgorithm);
            blockchain.RestoreBlockSequence(serialisedBlocks.Select(SerialisableBlockConverters<T>.ToBlock));
            return blockchain;
        }
    }
}
