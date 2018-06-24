using System;
using System.Security.Cryptography;
using Xunit;

namespace StandardChain.Tests
{
    public class BlockHashTests
    {
        [Fact]
        public void TestBlockHashesImplementEquals()
        {
            Assert.True(GivenBlockHashFrom("a").Equals(GivenBlockHashFrom("a")));
            Assert.False(GivenBlockHashFrom("a").Equals(GivenBlockHashFrom("b")));
        }

        [Fact]
        public void TestBlockHashesImplementEqualsOperator()
        {
            Assert.True(GivenBlockHashFrom("a") == GivenBlockHashFrom("a"));
            Assert.False(GivenBlockHashFrom("a") == GivenBlockHashFrom("b"));
        }

        [Fact]
        public void TestBlockHashesImplementNotEqualsOperator()
        {
            Assert.False(GivenBlockHashFrom("a") != GivenBlockHashFrom("a"));
            Assert.True(GivenBlockHashFrom("a") != GivenBlockHashFrom("b"));
        }

        #region Givens

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static BlockHash GivenBlockHashFrom(string value)
        {
            // BlockHash cannot be externally constructed, is still visible.
            return new Blockchain<string>(MD5.Create())
                .AddBlock(value, DateTime.MinValue)
                .LastBlockHash;
        }

        #endregion
    }
}
