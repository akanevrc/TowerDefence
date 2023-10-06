using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence.Runtime.VContainer
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            UnityEngine.Debug.Log("RootLifetimeScope.Configure");
        }
    }
}
