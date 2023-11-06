using UnityEngine;
using VContainer;

namespace akanevrc.TowerDefence
{
    [EntityBehaviour(nameof(BulletSetting) + "." + nameof(BulletSetting.KindType))]
    public class BulletBehaviour : EntityBehaviour<Bullet, BulletSetting.KindType>
    {
        [Inject] private readonly EntityStore<Bullet, BulletFactory.FactoryParams> _bulletStore;

        private void LateUpdate()
        {
            if (_bulletStore.TryGet(Id, out var bullet))
            {
                transform.SetLocalPositionAndRotation
                (
                    new Vector3(bullet.Position.x, bullet.Position.y, transform.localPosition.z),
                    Quaternion.AngleAxis(bullet.Data.Angle, Vector3.forward)
                );
            }
        }
    }
}
