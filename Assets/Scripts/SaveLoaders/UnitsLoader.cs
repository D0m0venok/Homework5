using System.Collections.Generic;
using System.Linq;
using GameEngine;
using UnityEngine;

public sealed class UnitsLoader : SaveLoader<UnitsData, UnitManager>
{
    private UnitsPrefabsManager _unitsPrefabsManager;
    
    public UnitsLoader(UnitsPrefabsManager unitsPrefabsManager)
    {
        _unitsPrefabsManager = unitsPrefabsManager;
    }

    protected override UnitsData ConvertToData(UnitManager service)
    {
        var data = new UnitsData { Units = new Dictionary<string, List<(int HitPoints, (float, float, float), (float, float, float))>>()};

        foreach (var unit in service.GetAllUnits())
        {
            if (!data.Units.ContainsKey(unit.Type))
                data.Units.Add(unit.Type, new List<(int HitPoints, (float, float, float), (float, float, float))>());

            var pos = unit.Position;
            var rot = unit.Rotation;
            data.Units[unit.Type].Add((unit.HitPoints, (pos.x, pos.y, pos.z), (rot.x, rot.y, rot.z)));
        }
        return data;
    }
    protected override void SetData(UnitsData data, UnitManager service)
    {
        foreach (var unit in service.GetAllUnits().ToArray())
        {
            service.DestroyUnit(unit);
        }

        foreach (var units in data.Units)
        {
            foreach (var dataUnit in units.Value)
            {
                var pos = dataUnit.Position;
                var rotation = dataUnit.Rotation;
                var unit = service.SpawnUnit(_unitsPrefabsManager.GetPrefab(units.Key), 
                    new Vector3(pos.X, pos.Y, pos.Z), new Quaternion(rotation.X, rotation.Y, rotation.Z, 0));
                unit.HitPoints = dataUnit.HitPoints;
            }
        }
    }
}