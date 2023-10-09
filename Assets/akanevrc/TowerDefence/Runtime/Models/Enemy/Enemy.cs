using System;
using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct Enemy
    {
        public float Health;
        public float Attack;
        public Vector2 Offset;
        public MarkPoint Mark;
        public float OffsetFactor;
        public float Velocity;
        public EnemyDirection Direction;
    }
}
