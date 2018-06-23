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

        public Block(T transaction, DateTime timeStamp, string hash)
        {
            var blockDto = new BlockDto
            {
                TimeStamp = timeStamp,
                Transaction = JsonConvert.SerializeObject(transaction),
                Hash = hash,
            };
            _blockString = JsonConvert.SerializeObject(blockDto);
        }

        public string Hash(HashAlgorithm hasher)
        {
            var hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(_blockString));
            return BitConverter.ToString(hashBytes);
        }

        public string Serialise()
        {
            return _blockString;
        }
    }
}
