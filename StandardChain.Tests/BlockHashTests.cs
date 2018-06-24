using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandardChain.Tests
{
    [TestClass]
    public class BlockHashTests
    {
        [TestMethod]
        public void TestBlockHashesImplementEquals()
        {
            Assert.IsTrue(GivenBlockHashFrom("a").Equals(GivenBlockHashFrom("a")));
            Assert.IsFalse(GivenBlockHashFrom("a").Equals(GivenBlockHashFrom("b")));
        }

        [TestMethod]
        public void TestBlockHashesImplementEqualsOperator()
        {
            Assert.IsTrue(GivenBlockHashFrom("a") == GivenBlockHashFrom("a"));
            Assert.IsFalse(GivenBlockHashFrom("a") == GivenBlockHashFrom("b"));
        }

        [TestMethod]
        public void TestBlockHashesImplementNotEqualsOperator()
        {
            Assert.IsFalse(GivenBlockHashFrom("a") != GivenBlockHashFrom("a"));
            Assert.IsTrue(GivenBlockHashFrom("a") != GivenBlockHashFrom("b"));
        }

        #region Givens

        // BlockHash cannot be externally constructed, is still visible.
        public BlockHash GivenBlockHashFrom(string value)
        {
            return new Blockchain<string>(SHA512.Create())
                .AddBlock(value, DateTime.MinValue)
                .LastBlockHash;
        }

        #endregion
    }
}
