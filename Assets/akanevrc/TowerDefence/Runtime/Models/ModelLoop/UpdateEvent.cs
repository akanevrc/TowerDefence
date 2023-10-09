
namespace akanevrc.TowerDefence
{
    [Message(typeof(UpdateEvent))]
    public readonly struct UpdateEvent
    {
        public float DeltaTime { get; }

        public UpdateEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
