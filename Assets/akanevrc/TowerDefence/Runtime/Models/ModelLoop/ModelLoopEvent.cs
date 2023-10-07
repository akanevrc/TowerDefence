
namespace akanevrc.TowerDefence
{
    [Message]
    public struct ModelLoopEvent
    {
        public float DeltaSecond { get; }

        public ModelLoopEvent(float deltaSecond)
        {
            DeltaSecond = deltaSecond;
        }
    }
}
