using Newtonsoft.Json;
using System;
using System.Linq;

namespace StandardChain
{
    public class BlockchainSerializer<T>
    {
        public string Serialize(Blockchain<T> blockchain)
        {
            if (blockchain == null) throw new ArgumentNullException(nameof(blockchain));

            var orderedBlocks = blockchain
                .BlocksInCreationOrder
                .Select(SerialisableBlockConverters<T>.FromBlock);
            return JsonConvert.SerializeObject(orderedBlocks);
        }
    }
}
