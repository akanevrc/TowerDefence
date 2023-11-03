using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class BulletStateUpdater
    {
        [Inject] private EntityStore<Enemy, EnemyFactory.FactoryParams> _enemyStore { get; }
        [Inject] private IPublisher<BulletHitEvent> _bulletHitPub { get; }

        public void UpdateToNext(Entity<Bullet> bullet, float deltaTime)
        {
            if (!_enemyStore.Entities.TryGetValue(bullet.Data.TargetId, out var target))
            {
                bullet.IsAlive = false;
                return;
            }

            if (bullet.Position == target.Position)
            {
                bullet.Data.Velocity = 0.0F;
                bullet.Data.Angle = GetAngle(bullet.Position, target.Position);
                bullet.Data.TargetId = Entity<Enemy>.None.Id;

                _bulletHitPub.Publish(new BulletHitEvent(bullet, target));
                return;
            }

            var position = bullet.Position;
            var diff = target.Position - position;
            var dp = diff.normalized * bullet.Data.Velocity * deltaTime;

            if (diff.sqrMagnitude <= dp.sqrMagnitude)
            {
                bullet.Position = target.Position;
                bullet.Data.Velocity = 0.0F;
                bullet.Data.Angle = GetAngle(position, target.Position);
                bullet.Data.TargetId = Entity<Enemy>.None.Id;

                _bulletHitPub.Publish(new BulletHitEvent(bullet, target));
                return;
            }
            else
            {
                bullet.Position = position + dp;
                bullet.Data.Angle = GetAngle(position + dp, target.Position);
                return;
            }
        }

        private float GetAngle(Vector2 position, Vector2 target)
        {
            if (target == position) return 0.0F;

            var v = target - position;
            return Mathf.Atan2(v.y, v.x) * 180.0F / Mathf.PI;
        }
    }
}
