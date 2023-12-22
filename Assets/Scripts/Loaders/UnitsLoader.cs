using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveSystem;

namespace Homework5
{
    public sealed class UnitsLoader : SaveLoader<UnitsData, UnitManager>
    {
        private readonly UnitSpawnerDecorator _unitSpawnerDecorator;
    
        public UnitsLoader(UnitSpawnerDecorator unitSpawnerDecorator)
        {
            _unitSpawnerDecorator = unitSpawnerDecorator;
        }

        protected override UnitsData ConvertToData(UnitManager service)
        {
            var data = new UnitsData(new Dictionary<string, List<UnitData>>());

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
                foreach (var unitData in units.Value)
                {
                    var position = unitData.Position;
                    var rotation = unitData.Rotation;
                    var hitPoints = unitData.HitPoints;
                    _unitSpawnerDecorator.Spawn(units.Key, position.ToVector3(), rotation.ToVector3(), hitPoints);
                }
            }
        }
    }
}