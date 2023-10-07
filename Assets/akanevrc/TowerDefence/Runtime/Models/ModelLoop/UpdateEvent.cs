
namespace akanevrc.TowerDefence
{
    [Message]
    public struct UpdateEvent
    {
        public float DeltaSecond { get; }

        public UpdateEvent(float deltaSecond)
        {
            DeltaSecond = deltaSecond;
        }
    }
}
