using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct EnemySeries
    {
        public float SpawnTime;
        public EnemySetting.KindType Kind;
        public float Interval;
        public int Count;
    }
}
