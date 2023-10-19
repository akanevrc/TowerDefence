
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct UnitStateChangedEvent
    {
        public Entity<Unit> Unit { get; }
        public UnitState State { get; }

        public UnitStateChangedEvent(Entity<Unit> unit, UnitState state)
        {
            Unit = unit;
            State = state;
        }
    }
}
