
namespace akanevrc.TowerDefence
{
    public static class ObjectUtil
    {
        public static T Assign<T>(this T obj, out T assigned)
        {
            assigned = obj;
            return obj;
        }
    }
}
