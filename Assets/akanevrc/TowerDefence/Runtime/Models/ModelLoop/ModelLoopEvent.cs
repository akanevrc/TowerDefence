
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct ModelLoopEvent
    {
        public float DeltaSecond { get; }

        public ModelLoopEvent(float deltaSecond)
        {
            DeltaSecond = deltaSecond;
        }
    }
}
