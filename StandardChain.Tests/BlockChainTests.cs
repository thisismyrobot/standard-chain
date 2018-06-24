﻿using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChain.Serialisation;
using StandardChain.Tests.TestClasses;

namespace StandardChain.Tests
{
    [TestClass]
    public class BlockchainTests
    {
        [TestMethod]
        public void TestBlockchainCanHaveMultipleTransactions()
        {
            var blockChain = GivenABlockchain<TestTransaction>();
            blockChain.AddBlock(GivenATransactionToStore(23.2m, "Robert"), new DateTime(2017, 01, 01));
            blockChain.AddBlock(GivenATransactionToStore(12.6m, "Dave"), new DateTime(2017, 01, 02));
            blockChain.AddBlock(GivenATransactionToStore(47.2m, "Robert"), new DateTime(2017, 01, 03));


            var jsonRepresentation = BlockchainSerialiser<TestTransaction>.Serialise(blockChain);


            // Oldest history is accessable first.
            Assert.AreEqual(new DateTime(2017, 01, 01), blockChain.HistoryInCreationOrder[0].TimeStamp);
            Assert.AreEqual(new DateTime(2017, 01, 02), blockChain.HistoryInCreationOrder[1].TimeStamp);
            Assert.AreEqual(new DateTime(2017, 01, 03), blockChain.HistoryInCreationOrder[2].TimeStamp);
            Assert.AreEqual(23.2m, blockChain.HistoryInCreationOrder[0].Payload.Amount);
            Assert.AreEqual(12.6m, blockChain.HistoryInCreationOrder[1].Payload.Amount);
            Assert.AreEqual(47.2m, blockChain.HistoryInCreationOrder[2].Payload.Amount);

            Assert.AreEqual(blockChain.Length, 3);
        }
        
        #region Givens

        private Blockchain<T> GivenABlockchain<T>()
        {
            return new Blockchain<T>(GivenAHashAlgorithm());
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
