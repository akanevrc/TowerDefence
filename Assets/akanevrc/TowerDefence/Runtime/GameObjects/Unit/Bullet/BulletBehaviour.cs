using UnityEngine;

namespace akanevrc.TowerDefence
{
    [EntityBehaviour(nameof(BulletSetting) + "." + nameof(BulletSetting.KindType))]
    public class BulletBehaviour : EntityBehaviour<Bullet, BulletSetting.KindType>
    {
        private void LateUpdate()
        {
            transform.localPosition = new Vector3(Entity.Position.x, Entity.Position.y, transform.localPosition.z);
            transform.localRotation = Quaternion.AngleAxis(Entity.Data.Angle, Vector3.forward);
        }
    }
}
