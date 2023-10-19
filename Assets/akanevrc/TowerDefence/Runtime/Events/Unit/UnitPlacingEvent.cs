using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct UnitPlacingEvent
    {
        public int Kind { get; }
        public Vector2 Position { get; }
        public int PedestalId { get; }

        public UnitPlacingEvent(int kind, Vector2 position, int pedestalId)
        {
            Kind = kind;
            Position = position;
            PedestalId = pedestalId;
        }
    }
}
