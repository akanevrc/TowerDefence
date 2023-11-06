using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct UnitPlacingEvent
    {
        public int Kind { get; }
        public int PedestalId { get; }

        public UnitPlacingEvent(int kind, int pedestalId)
        {
            Kind = kind;
            PedestalId = pedestalId;
        }
    }
}
