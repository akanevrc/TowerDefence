
namespace akanevrc.TowerDefence
{
    public interface IKindType<out T>
    {
        T FromInt(int i);
        int ToInt();
    }
}
