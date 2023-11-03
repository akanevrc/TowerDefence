
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct EnemyGoaledEvent
    {
        public int EnemyId { get; }

        public EnemyGoaledEvent(int enemyId)
        {
            EnemyId = enemyId;
        }
    }
}
