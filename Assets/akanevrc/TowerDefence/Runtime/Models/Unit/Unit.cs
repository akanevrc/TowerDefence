using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct Unit
    {
        public UnitAction Action;
        public TargetingStrategy TargetingStrategy;
    }
}
