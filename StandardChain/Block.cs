using Newtonsoft.Json;
using StandardChain.Dto;
using System;
using System.Security.Cryptography;
using System.Text;

namespace StandardChain
{
    internal class Block<T>
    {
        private readonly string _blockString;

        public Block(T transaction, DateTime timeStamp, BlockHash hash)
        {
            if (hash == null) throw new ArgumentNullException(nameof(hash));

            var blockDto = new BlockDto
            {
                TimeStamp = timeStamp,
                Transaction = JsonConvert.SerializeObject(transaction),
                Hash = hash.Value,
            };
            _blockString = JsonConvert.SerializeObject(blockDto);
        }

        public BlockHash Hash(HashAlgorithm hasher)
        {
            var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(_blockString));
            return new BlockHash(hashBytes);
        }

        public string Serialise()
        {
            return _blockString;
        }
    }
}
