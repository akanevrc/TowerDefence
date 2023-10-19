
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct UpdateEvent
    {
        public float DeltaTime { get; }

        public UpdateEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
