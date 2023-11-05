using System.Collections.Generic;
using UnityEngine;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{
    public abstract class EntityFactoryBehaviour<T, TKind, TSetting, TFactoryParam> : MonoBehaviour
        where T : struct
        where TKind : struct
        where TSetting : IEntitySetting<TKind>
        where TFactoryParam : struct
    {
        [Inject] protected IObjectResolver Resolver;
        [Inject] protected EntityStore<T, TFactoryParam> EntityStore;
        [Inject] protected SettingStore<TKind, TSetting> SettingStore;
        [Inject] protected ISubscriber<EntityCreatedEvent<T>> EntityCreatedSub;
        [Inject] protected ISubscriber<EntityDestroyingEvent<T>> EntityDestroyingSub;
        [Inject] protected EntityBehaviour<T, TKind> EntityPrefab;

        protected Dictionary<int, EntityBehaviour<T, TKind>> Entities { get; } = new();
        protected DisposableBagBuilder Disposables { get; } = DisposableBag.CreateBuilder();

        protected void Init()
        {
            EntityCreatedSub.Subscribe(OnEntityCreated).AddTo(Disposables);
            EntityDestroyingSub.Subscribe(OnEntityDestroying).AddTo(Disposables);
        }

        protected void Dispose()
        {
            Disposables.Build().Dispose();
        }

        private void OnEntityCreated(EntityCreatedEvent<T> ev)
        {
            if (EntityStore.TryGet(ev.Id, out var entity)) return;

            var z = typeof(T) == typeof(Pedestal) ? 50.0F : entity.Position.y;

            var behaviour = Resolver.Instantiate
            (
                EntityPrefab,
                new Vector3(entity.Position.x, entity.Position.y, z),
                transform.rotation
            );
            behaviour.Entity = entity;
            Entities.TryAdd(ev.Id, behaviour);

            behaviour.Setting = SettingStore.Settings.TryGetValue(entity.Kind.IntToKind<TKind>(), out var setting) ? setting : null;
        }

        private void OnEntityDestroying(EntityDestroyingEvent<T> ev)
        {
            if (Entities.TryGetValue(ev.Id, out var behaviour))
            {
                UnityEngine.Object.Destroy(behaviour.gameObject);
            }
            
            Entities.Remove(ev.Id);
        }
    }
}
