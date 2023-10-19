
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct EnemyGoaledEvent
    {
        public Entity<Enemy> Enemy { get; }

        public EnemyGoaledEvent(Entity<Enemy> enemy)
        {
            Enemy = enemy;
        }
    }
}
