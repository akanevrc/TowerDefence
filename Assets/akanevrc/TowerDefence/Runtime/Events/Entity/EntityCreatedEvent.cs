
namespace akanevrc.TowerDefence
{
    [Message(typeof(Unit))]
    [Message(typeof(Bullet))]
    [Message(typeof(Enemy))]
    [Message(typeof(Pedestal))]
    [Message(typeof(Goal))]
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
