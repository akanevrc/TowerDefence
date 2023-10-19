
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct WaveStartedEvent
    {
        public WaveNumber Wave { get; }

        public WaveStartedEvent(WaveNumber wave)
        {
            Wave = wave;
        }
    }
}
