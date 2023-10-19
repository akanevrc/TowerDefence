using System.Collections.Generic;
using System.Linq;

namespace akanevrc.TowerDefence
{
    [SettingStore(typeof(UnitSetting.KindType), typeof(UnitSetting))]
    [SettingStore(typeof(BulletSetting.KindType), typeof(BulletSetting))]
    [SettingStore(typeof(EnemySetting.KindType), typeof(EnemySetting))]
    [SettingStore(typeof(PedestalSetting.KindType), typeof(PedestalSetting))]
    [SettingStore(typeof(StageNumber), typeof(GoalSetting))]
    [SettingStore(typeof(StageNumber), typeof(StageSetting))]
    [SettingStore(typeof(WaveNumber), typeof(EnemyWaveSetting))]
    public class SettingStore<TKind, TSetting>
        where TKind : struct
        where TSetting : ISetting<TKind>
    {
        public Dictionary<TKind, TSetting> Settings { get; private set; }

        public void Init(IEnumerable<TSetting> settings)
        {
            Settings = settings.ToDictionary(x => x.Kind, x => x);
        }
    }
}
