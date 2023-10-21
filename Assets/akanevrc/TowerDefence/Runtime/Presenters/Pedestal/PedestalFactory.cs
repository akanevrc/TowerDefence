using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class PedestalFactory : IEntityFactory<Pedestal, PedestalFactory.FactoryParams>
    {
        public readonly struct FactoryParams
        {
            public int Id { get; }
            public PedestalSetting.KindType Kind { get; }
            public Vector2 Position { get; }

            public FactoryParams(int id, PedestalSetting.KindType kind, Vector2 position)
            {
                Id = id;
                Kind = kind;
                Position = position;
            }
        }

        [Inject] private SettingStore<PedestalSetting.KindType, PedestalSetting> _pedestalSettingStore;
        [Inject] private IPublisher<EntityCreatedEvent<Pedestal>> _pedestalCreatedPub;

        public Entity<Pedestal> Create(FactoryParams factoryParams)
        {
            var pedestal = new Entity<Pedestal>()
            {
                Id = factoryParams.Id,
                Kind = (int)factoryParams.Kind,
                IsAlive = true,
                Position = factoryParams.Position,
                Data = new Pedestal()
                {
                    UnitId = Entity<Unit>.None.Id
                }
            };

            _pedestalCreatedPub.Publish(new EntityCreatedEvent<Pedestal>(pedestal));

            return pedestal;
        }
    }
}
