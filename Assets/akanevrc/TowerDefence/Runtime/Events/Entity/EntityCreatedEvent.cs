
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
        public int Id { get; }

        public EntityCreatedEvent(int id)
        {
            Id = id;
        }
    }
}
