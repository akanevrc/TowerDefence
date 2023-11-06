using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Factory]
    public class UnitFactory : IEntityFactory<Unit, UnitFactory.FactoryParams>
    {
        public readonly struct FactoryParams
        {
            public int Id { get; }
            public UnitSetting.KindType Kind { get; }
            public Vector2 Position { get; }

            public FactoryParams(int id, UnitSetting.KindType kind, Vector2 position)
            {
                Id = id;
                Kind = kind;
                Position = position;
            }
        }

        [Inject] private readonly SettingStore<UnitSetting.KindType, UnitSetting> _unitSettingStore;
        [Inject] private readonly UnitStateUpdater _unitStateUpdater;

        public Entity<Unit> Create(FactoryParams factoryParams)
        {
            var (attack, range) =
                _unitSettingStore.Settings.TryGetValue(factoryParams.Kind, out var setting) ?
                    (setting.Attacks[1], setting.Ranges[1]) :
                    (0.0F, 0.0F);

            var unit = new Entity<Unit>()
            {
                Id = factoryParams.Id,
                Kind = factoryParams.Kind.KindToInt(),
                IsAlive = true,
                Position = factoryParams.Position,
                Data = new Unit()
                {
                    Action = default,
                    TargetingStrategy = TargetingStrategy.ClosestRange,
                    TargetId = Entity<Enemy>.None.Id,
                    Level = 1,
                    Attack = attack,
                    Range = range,
                    PedestalId = Entity<Pedestal>.None.Id,
                }
            };
            _unitStateUpdater.UpdateOnFirst(ref unit);

            return unit;
        }
    }
}
