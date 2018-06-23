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


            var jsonRepresentation = blockChain.Serialised();


            Assert.AreEqual(
                "[{\"TimeStamp\":\"2017-01-01T00:00:00\",\"Payload\":{\"Amount\":23.2,\"Purchaser\":\"Robert\"},\"PreviousHashValue\":\"StandardChain\"}]",
                jsonRepresentation);
        }

        [TestMethod]
        public void TestBlockchainCanHaveMultipleTransactions()
        {
            var blockChain = GivenABlockchain<TestTransaction>();
            blockChain.AddBlock(GivenATransactionToStore(23.2m, "Robert"), new DateTime(2017, 01, 01));
            blockChain.AddBlock(GivenATransactionToStore(12.6m, "Dave"), new DateTime(2017, 01, 02));
            blockChain.AddBlock(GivenATransactionToStore(47.2m, "Robert"), new DateTime(2017, 01, 03));

            
            var jsonRepresentation = blockChain.Serialised();


            Assert.AreEqual(blockChain.Length, 3);
        }
        
        [TestMethod]
        public void TestBlockchainCanBeRestoredFromJson()
        {
            var serialisedJson = "[{\"TimeStamp\":\"2017-01-01T00:00:00\",\"Payload\":{\"Amount\":23.2,\"Purchaser\":\"Robert\"},\"PreviousHashValue\":\"StandardChain\"}]";

            var restoredBlockchain = GivenABlockchainFrom<TestTransaction>(serialisedJson);

            Assert.AreEqual(1, restoredBlockchain.Length);
        }

        #region Givens

        private Blockchain<T> GivenABlockchain<T>()
        {
            return new Blockchain<T>(GivenAHashAlgorithm());
        }

        private Blockchain<T> GivenABlockchainFrom<T>(string serialisedJson)
        {
            return BlockchainFactory<T>.FromJson(serialisedJson, GivenAHashAlgorithm());
        }

        private TestTransaction GivenATransactionToStore(decimal amount, string purchaser)
        {
            return new TestTransaction
            {
                Amount = amount,
                Purchaser = purchaser
            };
        }

        private HashAlgorithm GivenAHashAlgorithm()
        {
            return MD5.Create();
        }

        #endregion
    }
}
