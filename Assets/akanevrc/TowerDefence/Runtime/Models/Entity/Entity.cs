using System;
using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct Entity<T>
        where T : struct
    {
        public int Id;
        public int Kind;
        public bool IsAlive;
        public Vector2 Position;

        public T Data;
    }
}
