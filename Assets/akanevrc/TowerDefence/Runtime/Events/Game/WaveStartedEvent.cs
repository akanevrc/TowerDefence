
namespace akanevrc.TowerDefence
{
    [Message(typeof(WaveStartedEvent))]
    public readonly struct WaveStartedEvent
    {
        public WaveNumber Wave { get; }

        public WaveStartedEvent(WaveNumber wave)
        {
            Wave = wave;
        }
    }
}
