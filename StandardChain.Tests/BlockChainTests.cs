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
            blockChain.AddBlockFromTransaction(transaction, new DateTime(2017, 01, 01));


            var jsonRepresentation = blockChain.Serialised();


            Assert.AreEqual(
                "[{\"TimeStamp\":\"2017-01-01T00:00:00\",\"Transaction\":{\"Amount\":23.2,\"Purchaser\":\"Robert\"},\"PreviousHash\":\"StandardChain\"}]",
                jsonRepresentation);
        }

        [TestMethod]
        public void TestBlockchainCanBeRestoredFromJson()
        {
            var serialisedJson = "[{\"TimeStamp\":\"2017-01-01T00:00:00\",\"Transaction\":{\"Amount\":23.2,\"Purchaser\":\"Robert\"},\"PreviousHash\":\"StandardChain\"}]";

            var restoredBlockchain = GivenABlockchain<TestTransaction>().FilledFromExistingChain(serialisedJson);
        }

        #region Givens

        private Blockchain<T> GivenABlockchain<T>()
        {
            return new Blockchain<T>(GivenAHasher());
        }

        private TestTransaction GivenATransactionToStore(decimal amount, string purchaser)
        {
            return new TestTransaction
            {
                Amount = amount,
                Purchaser = purchaser
            };
        }

        private HashAlgorithm GivenAHasher()
        {
            return SHA512.Create();
        }

        #endregion
    }
}
