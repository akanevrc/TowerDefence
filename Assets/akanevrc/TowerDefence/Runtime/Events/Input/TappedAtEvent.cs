
namespace akanevrc.TowerDefence
{
    [Message(typeof(Unit))]
    [Message(typeof(Pedestal))]
    public struct TappedAtEvent<T>
        where T : struct
    {
        public int Id { get; }

        public TappedAtEvent(int id)
        {
            Id = id;
        }
    }
}
