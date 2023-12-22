using System;
using System.Collections.Generic;

namespace Homework5
{
    [Serializable]
    public struct UnitsData
    {
        public Dictionary<string, List<UnitData>> Units;

        public UnitsData(Dictionary<string, List<UnitData>> units)
        {
            Units = units;
        }
        
        public void AddData(string type, UnitData unitData)
        {
            if(!Units.ContainsKey(type))
                Units.Add(type, new List<UnitData>());
        
            Units[type].Add(unitData);
        }
        public IReadOnlyDictionary<string, List<UnitData>> GetData()
        {
            return Units;
        }
    }
}