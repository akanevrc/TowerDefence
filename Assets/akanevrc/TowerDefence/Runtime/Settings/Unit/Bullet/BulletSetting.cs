using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BulletSetting")]
    [Settings]
    public class BulletSetting : ScriptableObject, ISetting<BulletSetting.KindType>
    {
        public enum KindType
        {
            Normal
        }

        [SerializeField] private KindType _kind;
        [SerializeField] private float _velocity;

        public KindType Kind => _kind;
        public float Velocity => _velocity;
    }
}
