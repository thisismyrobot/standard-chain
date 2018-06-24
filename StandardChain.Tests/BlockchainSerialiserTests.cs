using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandardChain.Tests
{
    [TestClass]
    public class BlockchainSerialiserTests
    {
        [TestMethod]
        public void TestBlockchainCanBeSerialisedToJson()
        {
            var transaction = GivenATransactionToStore(23.2m, "Robert");
            var blockChain = GivenABlockchain<TestTransaction>();
            blockChain.AddBlock(transaction, new DateTime(2017, 01, 01));
            var blockchainSerialiser = GivenABlockchainSerialiser<TestTransaction>();


            var jsonRepresentation = blockchainSerialiser.Serialise(blockChain);


            Assert.AreEqual(
                "[{\"Timestamp\":\"2017-01-01T00:00:00\",\"Payload\":{\"Amount\":23.2,\"Purchaser\":\"Robert\"},\"PreviousHashValue\":\"StandardChain\"}]",
                jsonRepresentation);
        }

        #region Givens

        private static BlockchainSerialiser<T> GivenABlockchainSerialiser<T>()
        {
            return new BlockchainSerialiser<T>();
        }

        private static Blockchain<T> GivenABlockchain<T>()
        {
            return new Blockchain<T>(GivenAHashAlgorithm());
        }

        private static TestTransaction GivenATransactionToStore(decimal amount, string purchaser)
        {
            return new TestTransaction
            {
                Amount = amount,
                Purchaser = purchaser
            };
        }

        private static HashAlgorithm GivenAHashAlgorithm()
        {
            return MD5.Create();
        }

        #endregion
    }
}
