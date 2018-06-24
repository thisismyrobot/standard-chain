using System;
using System.Collections.Generic;
using System.Linq;

namespace StandardChain
{
    internal class BlockStack<T>
    {
        private readonly Stack<Block<T>> _blockStack;

        internal Block<T> LastBlock => Empty ? null : _blockStack.Peek();
        internal int Length => _blockStack.Count;
        internal bool Empty => Length == 0;
        internal IEnumerable<Block<T>> InCreationOrder => _blockStack.Reverse();

        internal BlockStack()
        {
            _blockStack = new Stack<Block<T>>();
        }

        internal void AddBlock(Block<T> block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));
            _blockStack.Push(block);
        }
    }
}