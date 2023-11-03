using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class EnemyFactory : IEntityFactory<Enemy, EnemyFactory.FactoryParams>
    {
        public readonly struct FactoryParams
        {
            public int Id { get; }
            public EnemySetting.KindType Kind { get; }
            public float OffsetFactor { get; }

            public FactoryParams(int id, EnemySetting.KindType kind, float offsetFactor)
            {
                Id = id;
                Kind = kind;
                OffsetFactor = offsetFactor;
            }
        }

        [Inject] private SettingStore<EnemySetting.KindType, EnemySetting> _enemySettingStore;
        [Inject] private StageStore _stageStore;
        [Inject] private EnemyStateUpdater _enemyStateUpdater;
        [Inject] private IPublisher<EntityCreatedEvent<Enemy>> _enemyCreatedPub;

        public Entity<Enemy> Create(FactoryParams factoryParams)
        {
            var (health, attack, velocity) =
                _enemySettingStore.Settings.TryGetValue(factoryParams.Kind, out var setting) ?
                    (setting.Health, setting.Attack, setting.Velocity) :
                    (1.0F, 1.0F, 1.0F);

            var enemy = new Entity<Enemy>()
            {
                Id = factoryParams.Id,
                Kind = factoryParams.Kind.KindToInt(),
                IsAlive = true,
                Position = default,
                Data = new Enemy()
                {
                    Health = health,
                    Attack = attack,
                    Offset = default,
                    Mark = default,
                    OffsetFactor = factoryParams.OffsetFactor,
                    Velocity = velocity,
                    Direction = default,
                }
            };
            _enemyStateUpdater.UpdateOnFirst(enemy, _stageStore.Stage.Route, factoryParams.OffsetFactor);

            _enemyCreatedPub.Publish(new EntityCreatedEvent<Enemy>(enemy));

            return enemy;
        }
    }
}
