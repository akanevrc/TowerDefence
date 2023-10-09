
namespace akanevrc.TowerDefence
{
    [Message(typeof(EntityCreatedEvent<Unit>))]
    [Message(typeof(EntityCreatedEvent<Bullet>))]
    [Message(typeof(EntityCreatedEvent<Enemy>))]
    [Message(typeof(EntityCreatedEvent<Pedestal>))]
    [Message(typeof(EntityCreatedEvent<Goal>))]
    public readonly struct EntityCreatedEvent<T>
        where T : struct
    {
        public Entity<T> Entity { get; }

        public EntityCreatedEvent(Entity<T> entity)
        {
            Entity = entity;
        }
    }
}
