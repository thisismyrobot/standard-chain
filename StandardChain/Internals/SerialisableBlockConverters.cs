using System;

namespace StandardChain
{
    internal static class SerialisableBlockConverters<T>
    {
        public static SerialisableBlock<T> FromBlock(Block<T> block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            return new SerialisableBlock<T>(
                block.Payload,
                block.Timestamp,
                block.PreviousHash.Value);
        }

        public static Block<T> ToBlock(SerialisableBlock<T> serialisableBlock)
        {
            if (serialisableBlock == null) throw new ArgumentNullException(nameof(serialisableBlock));

            return new Block<T>(
                serialisableBlock.Payload,
                serialisableBlock.Timestamp,
                new BlockHash(serialisableBlock.PreviousHashValue));
        }
    }
}
