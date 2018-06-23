using System;

namespace StandardChain.Dto
{
    internal class BlockDto
    {
        public DateTime TimeStamp { get; set; }
        public string Transaction { get; set; }
        public string Hash { get; set; }
    }
}
