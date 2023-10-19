using System;
using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct Entity<T>
        where T : struct
    {
        public static readonly Entity<T> None = new()
        {
            Id = -1,
            Kind = -1,
            IsAlive = false,
            Position = Vector2.zero,
            Data = default,
        };

        public int Id;
        public int Kind;
        public bool IsAlive;
        public Vector2 Position;

        public T Data;
    }
}
