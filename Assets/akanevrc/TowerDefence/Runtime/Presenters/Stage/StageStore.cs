using System.Linq;
using UnityEngine;
using VContainer;

namespace akanevrc.TowerDefence
{
    [StageStore]
    public class StageStore
    {
        [Inject] private readonly SettingStore<StageNumber, StageSetting> _stageSettingStore;
        [Inject] private readonly StageFactory _stageFactory;

        public Stage Stage { get; private set; }

        public void Init(StageNumber kind)
        {
            var route =
                _stageSettingStore.Settings.TryGetValue(kind, out var setting) ?
                    setting.Route :
                    Enumerable.Empty<Vector2Int>();

            Stage = _stageFactory.Create(route);
        }
    }
}
