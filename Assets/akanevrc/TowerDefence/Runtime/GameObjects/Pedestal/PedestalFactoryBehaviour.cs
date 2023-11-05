
namespace akanevrc.TowerDefence
{
    [MonoBehaviour]
    public class PedestalFactoryBehaviour : EntityFactoryBehaviour<Pedestal, PedestalSetting.KindType, PedestalSetting, PedestalFactory.FactoryParams>
    {
        private void Start()
        {
            Init();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
