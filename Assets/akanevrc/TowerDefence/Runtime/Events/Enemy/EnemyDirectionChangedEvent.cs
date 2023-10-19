
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct EnemyDirectionChangedEvent
    {
        public Entity<Enemy> Enemy { get; }
        public EnemyDirection Direction { get; }
        
        public EnemyDirectionChangedEvent(Entity<Enemy> enemy, EnemyDirection direction)
        {
            Enemy = enemy;
            Direction = direction;
        }
    }
}
