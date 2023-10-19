
namespace akanevrc.TowerDefence
{
    [Message]
    public readonly struct GameStartedEvent
    {
        public StageNumber Stage { get; }

        public GameStartedEvent(StageNumber stage)
        {
            Stage = stage;
        }
    }
}
