using System.Collections.Generic;
using System.Linq;

namespace akanevrc.TowerDefence
{
    public struct EnemySchedule
    {
        public WaveNumber Wave;
        public ReservedEnemy[] ReservedEnemies;

        public EnemySchedule(WaveNumber wave, IEnumerable<EnemySeries> enemySerieses)
        {
            Wave = wave;
            ReservedEnemies =
                ToReservedEnemies(enemySerieses)
                .OrderBy(x => x.SpawnTime)
                .ToArray();
        }

        private static IEnumerable<ReservedEnemy> ToReservedEnemies(IEnumerable<EnemySeries> enemySerieses)
        {
            var id = 0;
            foreach (var enemySeries in enemySerieses)
            {
                var time = enemySeries.SpawnTime;
                for (var i = 0; i < enemySeries.Count; i++)
                {
                    yield return new ReservedEnemy() { Id = id, SpawnTime = time, OffsetFactor = 0.0F, Kind = enemySeries.Kind };
                    id++;
                    time += enemySeries.Interval;
                }
            }
        }
    }
}
