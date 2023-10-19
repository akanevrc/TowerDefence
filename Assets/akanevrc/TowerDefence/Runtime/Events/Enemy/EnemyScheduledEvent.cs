
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct EnemyScheduledEvent
    {
        public ReservedEnemy ReservedEnemy { get; }

        public EnemyScheduledEvent(ReservedEnemy reservedEnemy)
        {
            ReservedEnemy = reservedEnemy;
        }
    }
}
