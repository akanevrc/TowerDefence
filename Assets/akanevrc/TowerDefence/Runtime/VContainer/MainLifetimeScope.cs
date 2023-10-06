using UnityEngine;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private MainConfig _mainConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_mainConfig);

            builder.Register<UpdateHandler>(Lifetime.Scoped);
            builder.Register<GameHandler>(Lifetime.Scoped);

            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<UpdateEvent>(options);
            builder.RegisterMessageBroker<ModelLoopEvent>(options);

            builder.RegisterEntryPoint<MainEntryPoint>();
        }
    }
}
