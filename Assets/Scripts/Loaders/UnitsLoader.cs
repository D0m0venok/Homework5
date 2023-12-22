using System.Collections.Generic;
using System.Linq;
using GameEngine;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class UnitsLoader : SaveLoader<UnitsData, UnitManager>
{
    private readonly UnitSpawnerDecorator _unitSpawnerDecorator;
    
    public UnitsLoader(UnitSpawnerDecorator unitSpawnerDecorator)
    {
        _unitSpawnerDecorator = unitSpawnerDecorator;
    }

    protected override UnitsData ConvertToData(UnitManager service)
    {
        var data = new UnitsData{Units = new Dictionary<string, List<UnitData>>()};

        foreach (var unit in service.GetAllUnits())
        {
            var unitData = new UnitData(unit.HitPoints, unit.Position, unit.Rotation);
            data.AddData(unit.Type, unitData);
        }
        
        return data;
    }
    protected override void SetData(UnitsData data, UnitManager service)
    {
        foreach (var unit in service.GetAllUnits().ToArray())
        {
            service.DestroyUnit(unit);
        }
        
        foreach (var units in data.GetData())
        {
            foreach (var dataUnit in units.Value)
            {
                var position = dataUnit.Position;
                var rotation = dataUnit.Rotation;
                var hitPoints = dataUnit.HitPoints;
                _unitSpawnerDecorator.Spawn(units.Key, position.ToVector3(), rotation.ToVector3(), hitPoints);
            }
        }
    }
}