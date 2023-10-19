
namespace akanevrc.TowerDefence
{
    [Message(typeof(Unit))]
    [Message(typeof(Bullet))]
    [Message(typeof(Enemy))]
    [Message(typeof(Pedestal))]
    [Message(typeof(Goal))]
    public readonly struct EntityDestroyingEvent<T>
        where T : struct
    {
        public Entity<T> Entity { get; }

        public EntityDestroyingEvent(Entity<T> entity)
        {
            Entity = entity;
        }
    }
}
