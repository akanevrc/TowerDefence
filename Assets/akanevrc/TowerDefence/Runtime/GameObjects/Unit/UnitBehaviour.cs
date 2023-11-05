
namespace akanevrc.TowerDefence
{
    [EntityBehaviour(nameof(UnitSetting) + "." + nameof(UnitSetting.KindType))]
    public class UnitBehaviour : EntityBehaviour<Unit, UnitSetting.KindType>
    {
    }
}
