using System.Collections.Generic;
using System.Linq;

namespace akanevrc.TowerDefence
{
    [SettingStore(typeof(SettingStore<UnitSetting.UnitKind, UnitSetting>))]
    [SettingStore(typeof(SettingStore<BulletSetting.BulletKind, BulletSetting>))]
    [SettingStore(typeof(SettingStore<EnemySetting.EnemyKind, EnemySetting>))]
    [SettingStore(typeof(SettingStore<PedestalSetting.PedestalKind, PedestalSetting>))]
    [SettingStore(typeof(SettingStore<StageNumber, GoalSetting>))]
    [SettingStore(typeof(SettingStore<StageNumber, StageSetting>))]
    [SettingStore(typeof(SettingStore<WaveNumber, EnemyWaveSetting>))]
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
