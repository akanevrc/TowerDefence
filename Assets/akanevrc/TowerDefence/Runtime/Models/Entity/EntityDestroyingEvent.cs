
namespace akanevrc.TowerDefence
{
    [Message(typeof(EntityDestroyingEvent<Unit>))]
    [Message(typeof(EntityDestroyingEvent<Bullet>))]
    [Message(typeof(EntityDestroyingEvent<Enemy>))]
    [Message(typeof(EntityDestroyingEvent<Pedestal>))]
    [Message(typeof(EntityDestroyingEvent<Goal>))]
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
