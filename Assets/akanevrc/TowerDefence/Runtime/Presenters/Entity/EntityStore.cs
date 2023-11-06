using System;
using System.Collections.Generic;
using System.Linq;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Presenter(typeof(Unit), typeof(UnitFactory.FactoryParams))]
    [Presenter(typeof(Bullet), typeof(BulletFactory.FactoryParams))]
    [Presenter(typeof(Enemy), typeof(EnemyFactory.FactoryParams))]
    [Presenter(typeof(Pedestal), typeof(PedestalFactory.FactoryParams))]
    [Presenter(typeof(Goal), typeof(GoalFactory.FactoryParams))]
    public class EntityStore<T, TFactoryParams>
        where T : struct
        where TFactoryParams : struct
    {
        [Inject] private readonly IEntityFactory<T, TFactoryParams> _entityFactory;
        
        [Inject] private readonly IPublisher<EntityCreatedEvent<T>> _entityCreatedPub;
        [Inject] private readonly IPublisher<EntityDestroyingEvent<T>> _entityDestroyingPub;

        private List<Entity<T>> _entities = new();
        private Dictionary<int, int> _idToIndex = new();

        public int Count => _entities.Count;

        public void Init(IEnumerable<TFactoryParams> factoryParams)
        {
            _entities =
                factoryParams
                .Select(p => _entityFactory.Create(p))
                .ToList();
            _idToIndex =
                _entities
                .Select((e, i) => (e, i))
                .ToDictionary(z => z.e.Id, z => z.i);
        }

        public Entity<T> Add(TFactoryParams factoryParams)
        {
            var entity = _entityFactory.Create(factoryParams);
            _entities.Add(entity);
            _idToIndex.Add(entity.Id, _entities.Count - 1);
            _entityCreatedPub.Publish(new EntityCreatedEvent<T>(entity.Id));
            return entity;
        }

        public bool TryGet(int id, out Entity<T> entity)
        {
            if (_idToIndex.TryGetValue(id, out var index))
            {
                entity = _entities[index];
                return true;
            }
            else
            {
                entity = default;
                return false;
            }
        }

        public bool TryModify(int id, Func<Entity<T>, Entity<T>> modify, out Entity<T> entity)
        {
            if (_idToIndex.TryGetValue(id, out var index))
            {
                entity = modify(_entities[index]);
                _entities[index] = entity;
                return true;
            }
            else
            {
                entity = default;
                return false;
            }
        }

        public IEnumerable<Entity<T>> Iterate()
        {
            return _idToIndex.Values.Select(i => _entities[i]);
        }

        public void ModifyAll(Func<Entity<T>, Entity<T>> modify)
        {
            for (var i = 0; i < _entities.Count; i++)
            {
                _entities[i] = modify(_entities[i]);
            }
        }

        public void DestroyAll(Func<Entity<T>, bool> predicate)
        {
            foreach (var entity in _entities.Where(predicate))
            {
                _entityDestroyingPub.Publish(new EntityDestroyingEvent<T>(entity.Id));
                _idToIndex.Remove(entity.Id);
            }
            _entities.RemoveAll(entity => predicate(entity));
            
            foreach (var (id, index) in _entities.Select((entity, i) => (entity.Id, i)))
            {
                _idToIndex[id] = index;
            }
        }
    }
}
