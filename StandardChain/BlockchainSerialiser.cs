using Newtonsoft.Json;
using System;
using System.Linq;

namespace StandardChain
{
    public class BlockchainSerialiser<T>
    {
        public string Serialise(Blockchain<T> blockchain)
        {
            if (blockchain == null) throw new ArgumentNullException(nameof(blockchain));

            var orderedBlocks = blockchain
                .BlocksInCreationOrder
                .Select(SerialisableBlockConverters<T>.FromBlock);
            return JsonConvert.SerializeObject(orderedBlocks);
        }
    }
}
