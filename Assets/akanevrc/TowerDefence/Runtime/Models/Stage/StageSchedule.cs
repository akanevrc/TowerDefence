using System.Collections.Generic;
using System.Linq;

namespace akanevrc.TowerDefence
{
    public struct StageSchedule
    {
        public StageNumber Stage;
        public Dictionary<WaveNumber, EnemySchedule> EnemySchedules;

        public StageSchedule(StageNumber stage, IEnumerable<EnemySchedule> enemySchedules)
        {
            Stage = stage;
            EnemySchedules = enemySchedules.ToDictionary(x => x.Wave, x => x);
        }
    }
}
