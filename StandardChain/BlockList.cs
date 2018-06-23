using System;
using System.Collections.Generic;

namespace StandardChain
{
    internal class BlockList<T>
    {
        private readonly IList<Block<T>> _blocks;

        internal Block<T> LastBlock { get; private set; }
        internal bool Empty { get; private set; } = true;
        internal IReadOnlyList<Block<T>> Blocks => (IReadOnlyList<Block<T>>)_blocks;

        internal BlockList()
        {
            _blocks = new List<Block<T>>();
        }

        internal void AddBlock(Block<T> block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            _blocks.Add(block);
            LastBlock = block;
            Empty = false;
        }
    }
}