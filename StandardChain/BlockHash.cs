using System;
using System.Collections.Generic;

namespace StandardChain
{
    public class BlockHash
    {
        internal static BlockHash FirstBlock = new BlockHash("StandardChain");

        internal string Value { get; }

        internal BlockHash(string hash)
        {
            if (string.IsNullOrEmpty(hash)) throw new ArgumentException("Hash cannot be null or empty", nameof(hash));
            Value = hash.Replace("-", "");
        }

        internal BlockHash(byte[] hashBytes) : this(BitConverter.ToString(hashBytes))
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