using System;
using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct MarkPoint
    {
        public static MarkPoint None { get; } = new MarkPoint() { Id = -1 };

        public int Id;
        public Vector2 Point;

        public readonly bool IsValid => Id >= 0;
    }
}
