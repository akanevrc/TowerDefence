
namespace akanevrc.TowerDefence
{
    [Message(typeof(GameStartedEvent))]
    public readonly struct GameStartedEvent
    {
        public StageNumber Stage { get; }

        public GameStartedEvent(StageNumber stage)
        {
            Stage = stage;
        }
    }
}
