using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence.Runtime.VContainer
{
    public class MainLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            UnityEngine.Debug.Log("MainLifetimeScope.Configure");
            
            builder.RegisterEntryPoint<MainEntryPoint>();
        }
    }
}
