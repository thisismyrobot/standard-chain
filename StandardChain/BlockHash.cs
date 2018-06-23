using System;
using System.Collections.Generic;

namespace StandardChain
{
    public class BlockHash
    {
        public string Value { get; }
        
        public BlockHash(byte[] hashBytes)
        {
            if (hashBytes == null) throw new ArgumentNullException(nameof(hashBytes));
            if (hashBytes.Length == 0) throw new ArgumentException("Cannot have empty hash", nameof(hashBytes));

            Value = BitConverter.ToString(hashBytes);
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