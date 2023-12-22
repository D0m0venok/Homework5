using System.Collections.Generic;
using Newtonsoft.Json;

namespace Homework5
{
    public struct ResourcesData
    {
        [JsonProperty]
        public readonly IReadOnlyDictionary<string, int> Resources;

        public ResourcesData(Dictionary<string, int> resources)
        {
            Resources = resources;
        }
    }
}