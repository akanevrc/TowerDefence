
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct StageLoopEvent
    {
        public float DeltaTime { get; }

        public StageLoopEvent(float deltaTime)
        {
            DeltaTime = deltaTime;
        }
    }
}
