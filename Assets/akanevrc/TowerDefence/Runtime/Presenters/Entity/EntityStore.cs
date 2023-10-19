using System.Collections.Generic;
using System.Linq;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter(typeof(Unit), typeof(UnitFactory.FactoryParams))]
    public class EntityStore<T, TFactoryParams>
        where T : struct
        where TFactoryParams : struct
    {
        [Inject] private IEntityFactory<T, TFactoryParams> _entityFactory;

        public Dictionary<int, Entity<T>> Entities { get; private set; }

        public void Init(IEnumerable<TFactoryParams> factoryParams)
        {
            Entities = factoryParams
            .Select(p => _entityFactory.Create(p))
            .ToDictionary(entity => entity.Id, entity => entity);
        }
    }
}
