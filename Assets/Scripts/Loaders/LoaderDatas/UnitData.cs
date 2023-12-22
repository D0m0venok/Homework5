using Newtonsoft.Json;
using UnityEngine;

namespace Homework5
{
    public struct UnitData
    {
        [JsonProperty]
        public int HitPoints { get; private set; }
        [JsonProperty]
        public SerializedVector3 Position { get; private set; }
        [JsonProperty]
        public SerializedVector3 Rotation { get; private set; }

        public UnitData(int hitPoints, Vector3 position, Vector3 rotation)
        {
            HitPoints = hitPoints;
            Position = new SerializedVector3(position);
            Rotation = new SerializedVector3(rotation);
        }
    }
}