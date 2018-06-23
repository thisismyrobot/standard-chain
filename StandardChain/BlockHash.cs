using System;
using System.Collections.Generic;

namespace StandardChain
{
    public class BlockHash
    {
        public static BlockHash FirstBlock = new BlockHash("StandardChain");

        public string Value { get; }

        public BlockHash(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentException("Hash cannot be null or empty", nameof(hash));
            Value = hash.Replace("-", "");
        }

        public BlockHash(byte[] hashBytes) : this(BitConverter.ToString(hashBytes))
        {
        }

        public override bool Equals(object obj)
        {
            return obj is BlockHash hash && Value == hash.Value;
        }

        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(Value);
        }
    }
}