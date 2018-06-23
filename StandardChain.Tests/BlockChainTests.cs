using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChain.Tests.TestClasses;

namespace StandardChain.Tests
{
    [TestClass]
    public class BlockchainTests
    {
        [TestMethod]
        public void TestBlockchainCanBeSerialisedToJson()
        {
            var transaction = GivenATransactionToStore(23.2m, "Robert");
            var blockChain = GivenABlockchain<TestTransaction>();
            blockChain.AddBlock(transaction, new DateTime(2017, 01, 01));


            var jsonRepresentation = blockChain.Serialise();


            Assert.AreEqual("", jsonRepresentation);
        }

        #region Givens

        private Blockchain<T> GivenABlockchain<T>()
        {
            return new Blockchain<T>(SHA512.Create());
        }

        private TestTransaction GivenATransactionToStore(decimal amount, string purchaser)
        {
            return new TestTransaction
            {
                Amount = amount,
                Purchaser = purchaser
            };
        }

        #endregion
    }
}
