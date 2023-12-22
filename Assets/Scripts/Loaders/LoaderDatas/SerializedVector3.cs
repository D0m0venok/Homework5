using System;
using UnityEngine;

namespace Homework5
{
    [Serializable]
    public struct SerializedVector3
    {
        public float X;
        public float Y;
        public float Z;

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