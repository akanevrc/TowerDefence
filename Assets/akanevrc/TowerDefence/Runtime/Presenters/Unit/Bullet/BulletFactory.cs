using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class BulletFactory : IEntityFactory<Bullet, BulletFactory.FactoryParams>
    {
        public readonly struct FactoryParams
        {
            public int Id { get; }
            public BulletSetting.KindType Kind { get; }
            public Vector2 Position { get; }
            public int TargetId { get; }
            public float Attack { get; }

            public FactoryParams(int id, BulletSetting.KindType kind, Vector2 position, int targetId, float attack)
            {
                Id = id;
                Kind = kind;
                Position = position;
                TargetId = targetId;
                Attack = attack;
            }
        }

        [Inject] private SettingStore<BulletSetting.KindType, BulletSetting> _bulletSettingStore;
        [Inject] private IPublisher<EntityCreatedEvent<Bullet>> _bulletCreatedPub;

        public Entity<Bullet> Create(FactoryParams factoryParams)
        {
            var velocity =
                _bulletSettingStore.Settings.TryGetValue(factoryParams.Kind, out var setting) ?
                    setting.Velocity :
                    5.0F;
                    
            var bullet = new Entity<Bullet>()
            {
                Id = factoryParams.Id,
                Kind = (int)factoryParams.Kind,
                IsAlive = true,
                Position = factoryParams.Position,
                Data = new Bullet()
                {
                    TargetId = factoryParams.TargetId,
                    Attack = factoryParams.Attack,
                    Velocity = velocity,
                    Angle = 0.0F
                }
            };

            _bulletCreatedPub.Publish(new EntityCreatedEvent<Bullet>(bullet));

            return bullet;
        }
    }
}
