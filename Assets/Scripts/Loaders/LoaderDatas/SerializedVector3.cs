using Newtonsoft.Json;
using UnityEngine;

namespace Homework5
{
    public struct SerializedVector3
    {
        [JsonProperty]
        public float X { get; private set; }
        [JsonProperty]
        public float Y { get; private set; }
        [JsonProperty]
        public float Z { get; private set; }

        public SerializedVector3(Vector3 vector3)
        {
            X = vector3.x;
            Y = vector3.y;
            Z = vector3.z;
        }
        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }
}