
namespace akanevrc.TowerDefence
{
    [MonoBehaviour]
    public class GoalFactoryBehaviour : EntityFactoryBehaviour<Goal, StageNumber, GoalSetting, GoalFactory.FactoryParams>
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
