using System;
using System.Security.Cryptography;
using Xunit;

namespace StandardChain.Tests
{
    public class BlockchainTests
    {
        [Fact]
        public void TestBlockchainCanHaveMultipleTransactions()
        {
            var blockChain = GivenABlockchain<TestTransaction>();
            blockChain.AddBlock(GivenATransactionToStore(23.2m, "Robert"), new DateTime(2017, 01, 01));
            blockChain.AddBlock(GivenATransactionToStore(12.6m, "Dave"), new DateTime(2017, 01, 02));
            blockChain.AddBlock(GivenATransactionToStore(47.2m, "Robert"), new DateTime(2017, 01, 03));


            // Oldest history is accessable first.
            Assert.Equal(new DateTime(2017, 01, 01), blockChain.HistoryInCreationOrder[0].Timestamp);
            Assert.Equal(new DateTime(2017, 01, 02), blockChain.HistoryInCreationOrder[1].Timestamp);
            Assert.Equal(new DateTime(2017, 01, 03), blockChain.HistoryInCreationOrder[2].Timestamp);
            Assert.Equal(23.2m, blockChain.HistoryInCreationOrder[0].Payload.Amount);
            Assert.Equal(12.6m, blockChain.HistoryInCreationOrder[1].Payload.Amount);
            Assert.Equal(47.2m, blockChain.HistoryInCreationOrder[2].Payload.Amount);

            Assert.Equal(3, blockChain.Length);
        }

        #region Givens

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
