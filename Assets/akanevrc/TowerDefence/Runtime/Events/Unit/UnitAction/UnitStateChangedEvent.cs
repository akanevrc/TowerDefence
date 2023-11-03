
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct UnitStateChangedEvent
    {
        public int UnitId { get; }
        public UnitState State { get; }

        public UnitStateChangedEvent(int unitId, UnitState state)
        {
            UnitId = unitId;
            State = state;
        }
    }
}
