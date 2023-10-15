
namespace akanevrc.TowerDefence
{
    [Message(typeof(EnemyScheduledEvent))]
    public readonly struct EnemyScheduledEvent
    {
        public ReservedEnemy ReservedEnemy { get; }

        public EnemyScheduledEvent(ReservedEnemy reservedEnemy)
        {
            ReservedEnemy = reservedEnemy;
        }
    }
}
