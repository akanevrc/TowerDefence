
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct ModelLoopEvent
    {
        public float DeltaTime { get; }

        public ModelLoopEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
