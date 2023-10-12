using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BulletSetting")]
    public class BulletSetting : ScriptableObject
    {
        public enum BulletKind
        {
            Normal
        }

        [SerializeField] private BulletKind _kind;
        [SerializeField] private float _velocity;

        public BulletKind Kind => _kind;
        public float Velocity => _velocity;
    }
}
