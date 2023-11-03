using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter]
    public class UnitStateUpdater
    {
        [Inject] private readonly EntityStore<Enemy, EnemyFactory.FactoryParams> _enemyStore;
        [Inject] private readonly Targeter _targeter;
        [Inject] private readonly IPublisher<UnitStateChangedEvent> _unitStateChangedPub;
        [Inject] private readonly IPublisher<BulletPlacingEvent> _bulletPlacingPub;

        public void UpdateOnFirst(ref Entity<Unit> unit)
        {
            unit.Data.Action = new UnitAction(UnitState.ReadySequence);
            unit.Data.TargetId = Entity<Enemy>.None.Id;
        }

        public void UpdateToNext(ref Entity<Unit> unit, float deltaTime)
        {
            if (unit.Data.Action.Index >= unit.Data.Action.Sequence.Length)
            {
                return;
            }

            switch (unit.Data.Action.CurrentState.Kind)
            {
                case UnitState.KindType.Ready:
                {
                    var t = _targeter.Target(unit.Position, unit.Data.TargetingStrategy, unit.Data.Range);
                    if (t.Id != Entity<Enemy>.None.Id)
                    {
                        unit.Data.Action = new UnitAction(UnitState.AttackSequence);
                        unit.Data.TargetId = t.Id;

                        _unitStateChangedPub.Publish(new UnitStateChangedEvent(unit.Id, UnitState.AttackSequence[0]));

                        UpdateToNext(ref unit, deltaTime);
                        return;
                    }
                    break;
                }
                case UnitState.KindType.Starting:
                    if (!_enemyStore.TryGet(unit.Data.TargetId, out var target) || !target.IsAlive)
                    {
                        var t = _targeter.Target(unit.Position, unit.Data.TargetingStrategy, unit.Data.Range);
                        if (t.Id == Entity<Enemy>.None.Id)
                        {
                            unit.Data.Action = new UnitAction(UnitState.ReadySequence);
                            unit.Data.TargetId = Entity<Enemy>.None.Id;

                            _unitStateChangedPub.Publish(new UnitStateChangedEvent(unit.Id, UnitState.ReadySequence[0]));

                            UpdateToNext(ref unit, deltaTime);
                            return;
                        }
                        else
                        {
                            unit.Data.TargetId = t.Id;
                        }
                    }
                    break;
                case UnitState.KindType.Attack:
                {
                    _bulletPlacingPub.Publish
                    (
                        new BulletPlacingEvent
                        (
                            unit.Kind,
                            unit.Position,
                            unit.Data.TargetId,
                            unit.Data.Attack
                        )
                    );
                    break;
                }
            }

            if (unit.Data.Action.RemainingTime <= deltaTime)
            {
                deltaTime -= unit.Data.Action.RemainingTime;
                unit.Data.Action.Index++;
                unit.Data.Action.RemainingTime =
                    unit.Data.Action.Index >= unit.Data.Action.Sequence.Length ?
                        float.PositiveInfinity :
                        unit.Data.Action.CurrentState.Time;

                _unitStateChangedPub.Publish(new UnitStateChangedEvent(unit.Id, unit.Data.Action.CurrentState));

                UpdateToNext(ref unit, deltaTime);
                return;
            }
            else
            {
                unit.Data.Action.RemainingTime -= deltaTime;
                return;
            }
        }
    }
}
