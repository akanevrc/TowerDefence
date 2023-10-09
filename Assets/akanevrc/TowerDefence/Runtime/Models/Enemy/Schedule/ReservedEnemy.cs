using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct ReservedEnemy
    {
        public int Id;
        public float SpawnTime;
        public float OffsetFactor;
        public int Kind;
    }
}
