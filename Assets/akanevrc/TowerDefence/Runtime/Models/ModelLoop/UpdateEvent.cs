
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct UpdateEvent
    {
        public float DeltaSecond { get; }

        public UpdateEvent(float deltaSecond)
        {
            DeltaSecond = deltaSecond;
        }
    }
}
