using System;
using System.Collections.Generic;

namespace Homework5
{
    [Serializable]
    public struct ResourcesData
    {
        public Dictionary<string, int> Resources;

        public ResourcesData(Dictionary<string, int> resources)
        {
            Resources = resources;
        }
    }
}