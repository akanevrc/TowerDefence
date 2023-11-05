using UnityEngine;

namespace akanevrc.TowerDefence
{
    [EntityBehaviour(nameof(EnemySetting) + "." + nameof(EnemySetting.KindType))]
    public class EnemyBehaviour : EntityBehaviour<Enemy, EnemySetting.KindType>
    {
        private void LateUpdate()
        {
            transform.localPosition = new Vector3(Entity.Position.x, Entity.Position.y, transform.localPosition.z);
        }
    }
}
