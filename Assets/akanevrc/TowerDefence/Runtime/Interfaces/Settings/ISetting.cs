
namespace akanevrc.TowerDefence
{
    public interface ISetting<TKind>
        where TKind : struct
    {
        TKind Kind { get; }
    }
}
