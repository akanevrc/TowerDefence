using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct TargetingStrategy
    {
        public static TargetingStrategy None = new() { Kind = 0 };
        public static TargetingStrategy FirstOrder = new() { Kind = 1 };
        public static TargetingStrategy MiddleOrder = new() { Kind = 2 };
        public static TargetingStrategy LastOrder = new() { Kind = 3 };
        public static TargetingStrategy StrongestHealth = new() { Kind = 4 };
        public static TargetingStrategy MiddleHealth = new() { Kind = 5 };
        public static TargetingStrategy WeakestHealth = new() { Kind = 6 };
        public static TargetingStrategy ClosestRange = new() { Kind = 7 };
        public static TargetingStrategy MiddleRange = new() { Kind = 8 };
        public static TargetingStrategy FarthestRange = new() { Kind = 9 };

        public int Kind;
    }
}
