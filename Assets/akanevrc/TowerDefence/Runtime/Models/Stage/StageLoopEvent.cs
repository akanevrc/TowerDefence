
namespace akanevrc.TowerDefence
{
    [Message(typeof(StageLoopEvent))]
    public readonly struct StageLoopEvent
    {
        public float DeltaTime { get; }

        public StageLoopEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
