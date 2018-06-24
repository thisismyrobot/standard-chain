using Newtonsoft.Json;
using System;
using System.Linq;

namespace StandardChain.Serialisation
{
    public static class BlockchainSerialiser<T>
    {
        public static string Serialise(Blockchain<T> blockchain)
        {
            if (blockchain == null) throw new ArgumentNullException(nameof(blockchain));

            var orderedBlocks = blockchain
                .BlocksInCreationOrder
                .Select(SerialisableBlockConverters<T>.FromBlock);
            return JsonConvert.SerializeObject(orderedBlocks);
        }
    }
}
