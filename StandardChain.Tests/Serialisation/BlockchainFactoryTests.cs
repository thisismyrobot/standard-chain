using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StandardChain.Exceptions;
using StandardChain.Serialisation;
using StandardChain.Tests.TestClasses;

namespace StandardChain.Tests.Serialisation
{
    [TestClass]
    public class BlockchainFactoryTests
    {   
        [TestMethod]
        public void TestBlockchainCanBeRestoredFromJson()
        {
            var serialisedJson = "[{\"TimeStamp\":\"2017-01-01T00:00:00\",\"Payload\":{\"Amount\":23.2,\"Purchaser\":\"Robert\"},\"PreviousHashValue\":\"StandardChain\"}]";

            var restoredBlockchain = GivenABlockchainFrom<TestTransaction>(serialisedJson);

            Assert.AreEqual(1, restoredBlockchain.Length);
        }

        [TestMethod]
        public void TestBlockchainIsValidatedWhenRestoredFromJson()
        {
            // The last hash should be "0BE9468F6431F9D8C1E73369CB3B20C6"
            const string invalidJson = @"
                [
                  {
                    ""TimeStamp"": ""2017-01-01T00:00:00"",
                    ""Payload"": 1,
                    ""PreviousHashValue"": ""StandardChain""
                  },
                  {
                    ""TimeStamp"": ""2017-01-02T00:00:00"",
                    ""Payload"": 2,
                    ""PreviousHashValue"": ""4714DF86BEF51CE25E3F810A64F041D6""
                  },
                  {
                    ""TimeStamp"": ""2017-01-03T00:00:00"",
                    ""Payload"": 3,
                    ""PreviousHashValue"": ""******""
                  }
                ]
            ";

            Assert.ThrowsException<InvalidBlockException>(() =>
            {
                var blockChain = GivenABlockchainFrom<int>(invalidJson);
            });
        }

        #region Givens

        private Blockchain<T> GivenABlockchainFrom<T>(string serialisedJson)
        {
            return BlockchainFactory<T>.FromJson(serialisedJson, GivenAHashAlgorithm());
        }

        private HashAlgorithm GivenAHashAlgorithm()
        {
            return MD5.Create();
        }

        #endregion
    }
}
