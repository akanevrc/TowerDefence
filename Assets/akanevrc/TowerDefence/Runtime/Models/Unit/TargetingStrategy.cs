using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct TargetingStrategy
    {
        public static TargetingStrategy None = new() { Kind = 0 };
        public static TargetingStrategy Nearest = new() { Kind = 1 };

        public int Kind;
    }
}
