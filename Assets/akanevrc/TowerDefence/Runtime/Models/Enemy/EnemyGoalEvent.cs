
namespace akanevrc.TowerDefence
{
    [Message(typeof(EnemyGoaledEvent))]
    public readonly struct EnemyGoaledEvent
    {
        public Entity<Enemy> Enemy { get; }

        public EnemyGoaledEvent(Entity<Enemy> enemy)
        {
            Enemy = enemy;
        }
    }
}
