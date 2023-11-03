using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class UnitLevelUpdater
    {
        [Inject] private readonly SettingStore<UnitSetting.KindType, UnitSetting> _unitSettingStore;

        public void IncrementLevel(ref Entity<Unit> unit)
        {
            if (_unitSettingStore.Settings.TryGetValue(unit.Kind.IntToKind<UnitSetting.KindType>(), out var setting) && unit.Data.Level < setting.MaxLevel)
            {
                var level = unit.Data.Level + 1;
                SetStatus(ref unit, level, setting.Attacks[level], setting.Ranges[level]);
            }
        }

        private void SetStatus(ref Entity<Unit> unit, int level, float attack, float range)
        {
            unit.Data.Level = level;
            unit.Data.Attack = attack;
            unit.Data.Range = range;
        }
    }
}
