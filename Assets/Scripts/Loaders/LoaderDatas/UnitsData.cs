using System.Collections.Generic;
using Newtonsoft.Json;

namespace Homework5
{
    public struct UnitsData
    {
        [JsonProperty]
        private Dictionary<string, List<UnitData>> units;

        public UnitsData(Dictionary<string, List<UnitData>> units)
        {
            this.units = units;
        }
        
        public void AddData(string type, UnitData unitData)
        {
            if(!units.ContainsKey(type))
                units.Add(type, new List<UnitData>());
        
            units[type].Add(unitData);
        }
        public IReadOnlyDictionary<string, List<UnitData>> GetData()
        {
            return units;
        }
    }
}