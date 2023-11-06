using UnityEngine;
using VContainer;

namespace akanevrc.TowerDefence
{
    [EntityBehaviour(nameof(EnemySetting) + "." + nameof(EnemySetting.KindType))]
    public class EnemyBehaviour : EntityBehaviour<Enemy, EnemySetting.KindType>
    {
        [Inject] private readonly EntityStore<Enemy, EnemyFactory.FactoryParams> _enemyStore;

        private void LateUpdate()
        {
            if (_enemyStore.TryGet(Id, out var enemy))
            {
                transform.localPosition = new Vector3(enemy.Position.x, enemy.Position.y, transform.localPosition.z);
            }
        }
    }
}
