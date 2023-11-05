using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [MonoBehaviour]
    public class EnemyFactoryBehaviour : EntityFactoryBehaviour<Enemy, EnemySetting.KindType, EnemySetting, EnemyFactory.FactoryParams>
    {
        [Inject] private readonly ISubscriber<EnemyDirectionChangedEvent> _enemyDirectionChangedSub;

        private void Start()
        {
            Init();
            _enemyDirectionChangedSub.Subscribe(OnEnemyDirectionChanged).AddTo(Disposables);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void OnEnemyDirectionChanged(EnemyDirectionChangedEvent ev)
        {
            if (Entities.TryGetValue(ev.EnemyId, out var enemy))
            {
                enemy.GetComponent<SpriteRenderer>().flipX = ev.Direction == EnemyDirection.L;
            }
        }
    }
}
