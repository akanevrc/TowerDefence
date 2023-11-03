
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct BulletHitEvent
    {
        public int BulletId { get; }
        public int TargetId { get; }

        public BulletHitEvent(int bulletId, int targetId)
        {
            BulletId = bulletId;
            TargetId = targetId;
        }
    }
}
