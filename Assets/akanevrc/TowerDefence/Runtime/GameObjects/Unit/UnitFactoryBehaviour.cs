using UnityEngine;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [MonoBehaviour]
    public class UnitFactoryBehaviour : EntityFactoryBehaviour<Unit, UnitSetting.KindType, UnitSetting, UnitFactory.FactoryParams>
    {
        [Inject] private readonly ISubscriber<UnitStateChangedEvent> _unitStateChangedSub;

        private void Start()
        {
            Init();
            _unitStateChangedSub.Subscribe(OnUnitStateChanged).AddTo(Disposables);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void OnUnitStateChanged(UnitStateChangedEvent ev)
        {
            if (Entities.TryGetValue(ev.UnitId, out var unit))
            {
                var animator = unit.GetComponent<Animator>();
                switch (ev.State.Kind)
                {
                    case UnitState.KindType.Ready:
                        animator.SetInteger("State", 0);
                        return;
                    case UnitState.KindType.Starting:
                        animator.SetInteger("State", 1);
                        return;
                    case UnitState.KindType.Ending:
                        animator.SetInteger("State", 2);
                        return;
                    default:
                        return;
                }
            }
        }
    }
}
