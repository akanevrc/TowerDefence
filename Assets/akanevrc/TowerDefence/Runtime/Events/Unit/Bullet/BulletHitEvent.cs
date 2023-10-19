
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct BulletHitEvent
    {
        public Entity<Bullet> Bullet { get; }
        public Entity<Enemy> Target { get; }

        public BulletHitEvent(Entity<Bullet> bullet, Entity<Enemy> target)
        {
            Bullet = bullet;
            Target = target;
        }
    }
}
