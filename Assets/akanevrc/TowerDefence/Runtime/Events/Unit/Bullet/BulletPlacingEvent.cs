using UnityEngine;

namespace akanevrc.TowerDefence
{
    [Message(typeof(BulletPlacingEvent))]
    public readonly struct BulletPlacingEvent
    {
        public int Kind { get; }
        public Vector2 Position { get; }
        public int TargetId { get; }
        public float Attack { get; }

        public BulletPlacingEvent(int kind, Vector2 position, int targetId, float attack)
        {
            Kind = kind;
            Position = position;
            TargetId = targetId;
            Attack = attack;
        }
    }
}
