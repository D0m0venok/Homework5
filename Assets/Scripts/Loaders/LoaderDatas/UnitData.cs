using System;
using UnityEngine;

namespace Homework5
{
    [Serializable]
    public struct UnitData
    {
        public int HitPoints;
        public SerializedVector3 Position;
        public SerializedVector3 Rotation;

        public UnitData(int hitPoints, Vector3 position, Vector3 rotation)
        {
            HitPoints = hitPoints;
            Position = new SerializedVector3(position);
            Rotation = new SerializedVector3(rotation);
        }
    }
}