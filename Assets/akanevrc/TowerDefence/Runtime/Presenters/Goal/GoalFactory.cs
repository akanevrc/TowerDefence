using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class GoalFactory : IEntityFactory<Goal, GoalFactory.FactoryParams>
    {
        public readonly struct FactoryParams
        {
            public int Id { get; }
            public StageNumber Kind { get; }
            public Vector2 Position { get; }

            public FactoryParams(int id, StageNumber kind, Vector2 position)
            {
                Id = id;
                Kind = kind;
                Position = position;
            }
        }

        [Inject] private SettingStore<StageNumber, GoalSetting> _goalSettingStore;
        [Inject] private IPublisher<EntityCreatedEvent<Goal>> _goalCreatedPub;

        public Entity<Goal> Create(FactoryParams factoryParams)
        {
            var health =
                _goalSettingStore.Settings.TryGetValue(factoryParams.Kind, out var setting) ?
                    setting.Health :
                    10.0F;

            var goal = new Entity<Goal>()
            {
                Id = factoryParams.Id,
                Kind = factoryParams.Kind.ToInt(),
                IsAlive = true,
                Position = factoryParams.Position,
                Data = new Goal()
                {
                    Health = health
                }
            };

            _goalCreatedPub.Publish(new EntityCreatedEvent<Goal>(goal));

            return goal;
        }
    }
}
