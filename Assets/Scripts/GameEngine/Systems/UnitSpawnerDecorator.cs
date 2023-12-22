using UnityEngine;

namespace GameEngine
{
    public sealed class UnitSpawnerDecorator
    {
        private readonly UnitManager _unitManager;
        private readonly UnitsPrefabsCollection _unitsPrefabsCollection;
    
        public UnitSpawnerDecorator(UnitManager unitManager, UnitsPrefabsCollection unitsPrefabsCollection)
        {
            _unitManager = unitManager;
            _unitsPrefabsCollection = unitsPrefabsCollection;
        }

        public void Spawn(string type, Vector3 position, Vector3 rotation, int hitPoints)
        {
            var unit = _unitManager.SpawnUnit(_unitsPrefabsCollection.GetPrefab(type), 
                position, new Quaternion(rotation.x, rotation.y, rotation.z, 0));
            unit.HitPoints = hitPoints;
        }
    }
}