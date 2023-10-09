using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct EnemySeries
    {
        public float SpawnTime;
        public int Kind;
        public float Interval;
        public int Count;
    }
}
