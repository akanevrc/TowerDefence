using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct Bullet
    {
        public TargetingStrategy TargetingStrategy;
        public int TargetId;
        public float Attack;
        public float Velocity;
        public float Angle;
    }
}
