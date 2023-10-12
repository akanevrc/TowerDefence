using System;

namespace akanevrc.TowerDefence
{
    [Serializable]
    public struct EnemySeries
    {
        public float SpawnTime;
        public EnemySetting.EnemyKind Kind;
        public float Interval;
        public int Count;
    }
}
