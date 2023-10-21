using System.Linq;
using UnityEngine;
using VContainer;

namespace akanevrc.TowerDefence
{
    public class Targeter
    {
        [Inject] private EntityStore<Enemy, EnemyFactory.FactoryParams> _enemyStore;

        public Entity<Enemy> Target(Vector2 position, TargetingStrategy strategy, float range)
        {
            return strategy switch
            {
                TargetingStrategy.None => Entity<Enemy>.None,
                TargetingStrategy.ClosestRange =>
                    _enemyStore.Entities.Values
                    .Aggregate
                    (
                        (enemy: Entity<Enemy>.None, distance: float.PositiveInfinity),
                        (acc, enemy) =>
                            Vector2.Distance(position, enemy.Position).Assign(out var distance) <= range && distance < acc.distance ?
                                (enemy, distance) :
                                acc
                    ).enemy,
                _ => Entity<Enemy>.None
            };
        }
    }
}
