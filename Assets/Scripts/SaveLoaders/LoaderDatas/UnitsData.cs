using System;
using System.Collections.Generic;

[Serializable]
public struct UnitsData
{
    public Dictionary<string, List<(int HitPoints, (float X, float Y, float Z) Position, 
        (float X, float Y, float Z) Rotation)>> Units;
}