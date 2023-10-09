using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct Unit
    {
        public UnitAction Action;
        public TargetingStrategy TargetingStrategy;
        public int TargetId;
        public int Level;
        public float Attack;
        public float Range;
        public int PedestalId;
    }
}
