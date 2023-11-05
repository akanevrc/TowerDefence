
namespace akanevrc.TowerDefence
{
    [EntityBehaviour(nameof(PedestalSetting) + "." + nameof(PedestalSetting.KindType))]
    public class PedestalBehaviour : EntityBehaviour<Pedestal, PedestalSetting.KindType>
    {
    }
}
