
namespace akanevrc.TowerDefence
{
    [MonoBehaviour]
    public class BulletFactoryBehaviour : EntityFactoryBehaviour<Bullet, BulletSetting.KindType, BulletSetting, BulletFactory.FactoryParams>
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
