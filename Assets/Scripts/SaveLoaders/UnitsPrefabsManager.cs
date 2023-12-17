using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using UnityEngine;

[Serializable]
public sealed class UnitsPrefabsManager
{
    private Dictionary<string, Unit> _prefabs;

    public UnitsPrefabsManager(Unit[] unitPrefabs)
    {
        _prefabs = unitPrefabs.ToDictionary(u => u.Type);
    }

    public Unit GetPrefab(string type)
    {
        return _prefabs[type];
    }
}