
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct EnemyDirectionChangedEvent
    {
        public int EnemyId { get; }
        public EnemyDirection Direction { get; }
        
        public EnemyDirectionChangedEvent(int enemyId, EnemyDirection direction)
        {
            EnemyId = enemyId;
            Direction = direction;
        }
    }
}
