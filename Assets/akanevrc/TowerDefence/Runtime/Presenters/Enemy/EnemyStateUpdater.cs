using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class EnemyStateUpdater
    {
        [Inject] private IPublisher<EnemyDirectionChangedEvent> _enemyDirectionChangedPub;
        [Inject] private IPublisher<EnemyGoaledEvent> _enemyGoaledPub;

        public void UpdateOnFirst(Entity<Enemy> enemy, RouteMarkPoints marks, float offsetFactor)
        {
            var current = marks.GetFirst();
            if (!current.IsValid)
            {
                enemy.Position = Vector2.zero;
                enemy.Data.Offset = Vector2.zero;
                enemy.Data.Mark = MarkPoint.None;
                enemy.Data.Direction = EnemyDirection.L;
                return;
            }

            var (next, offset2) = GetNextAndOffset(marks, current, offsetFactor);
            if (!next.IsValid)
            {
                enemy.Position = current.Point;
                enemy.Data.Offset = Vector2.zero;
                enemy.Data.Mark = MarkPoint.None;
                enemy.Data.Direction = EnemyDirection.L;
                return;
            }

            var offset1 = GetOffset(current.Point, next.Point, next.Point, offsetFactor);

            enemy.Position = current.Point + offset1;
            enemy.Data.Offset = offset2;
            enemy.Data.Mark = next;
            enemy.Data.Direction = GetFirstDirection(current.Point, next.Point);
            return;
        }

        public void UpdateToNext(Entity<Enemy> enemy, RouteMarkPoints marks, float deltaTime)
        {
            var current = enemy.Data.Mark;
            if (!current.IsValid)
            {
                enemy.Data.Mark = MarkPoint.None;
                enemy.Data.Direction = EnemyDirection.L;

                _enemyGoaledPub.Publish(new EnemyGoaledEvent(enemy));
                return;
            }

            var markPos = current.Point + enemy.Data.Offset;
            var diff = markPos - enemy.Position;
            var dp = diff.normalized * enemy.Data.Velocity * deltaTime;

            if (diff.sqrMagnitude < dp.sqrMagnitude)
            {
                var (next, newOffset) = GetNextAndOffset(marks, current, enemy.Data.OffsetFactor);

                enemy.Position = markPos;
                enemy.Data.Offset = newOffset;
                enemy.Data.Mark = next;

                UpdateToNext(enemy, marks, (1.0F - diff.magnitude / dp.magnitude) * deltaTime);
                return;
            }
            else
            {
                var p = enemy.Position + dp;
                if (p == markPos)
                {
                    var (next, newOffset) = GetNextAndOffset(marks, current, enemy.Data.OffsetFactor);

                    enemy.Position = p;
                    enemy.Data.Offset = newOffset;
                    enemy.Data.Mark = next;
                    enemy.Data.Direction = next.IsValid ? GetNextDirection(enemy, p, next.Point, enemy.Data.Direction) : EnemyDirection.L;
                    return;
                }
                else
                {
                    enemy.Position = p;
                    enemy.Data.Direction = GetNextDirection(enemy, p, current.Point, enemy.Data.Direction);
                    return;
                }
            }
        }

        private (MarkPoint next, Vector2 offset) GetNextAndOffset(RouteMarkPoints marks, MarkPoint current, float offsetFactor)
        {
            var second = marks.GetNext(current);
            var third = marks.GetNext(second);
            var offset = second.IsValid ? GetOffset(current.Point, second.Point, third.IsValid ? third.Point : second.Point, offsetFactor) : default;
            return (second, offset);
        }

        private Vector2 GetOffset(Vector2 first, Vector2 second, Vector2 third, float offsetFactor)
        {
            var dir1 = second - first;
            var dir2 = third - first;
            var dir = Vector2.Dot(dir1.normalized, dir2.normalized) > 0.9F ? dir1 : dir2;
            return new Vector2(-dir.y, dir.x) * offsetFactor;
        }

        private EnemyDirection GetFirstDirection(Vector2 current, Vector2 next)
        {
            return (next - current).x <= 0.0F ? EnemyDirection.L : EnemyDirection.R;
        }

        private EnemyDirection GetNextDirection(Entity<Enemy> enemy, Vector2 current, Vector2 next, EnemyDirection direction)
        {
            var angle = Vector2.Angle(Vector2.right, next - current);

            if (Mathf.Abs(angle) < 67.5F)
            {
                if (direction != EnemyDirection.R) _enemyDirectionChangedPub.Publish(new EnemyDirectionChangedEvent(enemy, EnemyDirection.R));
                return EnemyDirection.R;
            }
            else if (Mathf.Abs(angle) > 112.5F)
            {
                if (direction != EnemyDirection.L) _enemyDirectionChangedPub.Publish(new EnemyDirectionChangedEvent(enemy, EnemyDirection.L));
                return EnemyDirection.L;
            }
            else
            {
                return direction;
            }
        }
    }
}
