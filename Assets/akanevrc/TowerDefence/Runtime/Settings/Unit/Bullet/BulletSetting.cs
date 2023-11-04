using UnityEngine;
using UnityEngine.U2D.Animation;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BulletSetting")]
    [Settings]
    public class BulletSetting : ScriptableObject, IEntitySetting<BulletSetting.KindType>
    {
        public enum KindType
        {
            Normal
        }

        [SerializeField] private KindType _kind;
        [SerializeField] private float _velocity;
        [SerializeField] private SpriteLibraryAsset _spriteLib;

        public KindType Kind => _kind;
        public float Velocity => _velocity;
        public SpriteLibraryAsset SpriteLib => _spriteLib;
    }
}
