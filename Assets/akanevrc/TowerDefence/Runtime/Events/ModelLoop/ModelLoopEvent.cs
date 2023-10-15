
namespace akanevrc.TowerDefence
{
    [Message(typeof(ModelLoopEvent))]
    public readonly struct ModelLoopEvent
    {
        public float DeltaTime { get; }

        public ModelLoopEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
