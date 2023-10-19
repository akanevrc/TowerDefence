
namespace akanevrc.TowerDefence
{
    public interface IEntityFactory<T, TFactoryParams>
        where T : struct
        where TFactoryParams : struct
    {
        Entity<T> Create(TFactoryParams factoryParams);
    }
}
