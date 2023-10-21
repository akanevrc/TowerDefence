using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class UnitLevelUpdater
    {
        [Inject] private SettingStore<UnitSetting.KindType, UnitSetting> _unitSettingStore;

        public void IncrementLevel(Entity<Unit> unit)
        {
            if (_unitSettingStore.Settings.TryGetValue((UnitSetting.KindType)unit.Kind, out var setting) && unit.Data.Level < setting.MaxLevel)
            {
                var level = unit.Data.Level + 1;
                SetStatus(unit, level, setting.Attacks[level], setting.Ranges[level]);
            }
        }

        private void SetStatus(Entity<Unit> unit, int level, float attack, float range)
        {
            unit.Data.Level = level;
            unit.Data.Attack = attack;
            unit.Data.Range = range;
        }
    }
}
