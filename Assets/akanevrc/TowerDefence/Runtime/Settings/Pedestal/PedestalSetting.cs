using UnityEngine;
using UnityEngine.U2D.Animation;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PedestalSetting")]
    [Settings]
    public class PedestalSetting : ScriptableObject, IEntitySetting<PedestalSetting.KindType>
    {
        public enum KindType
        {
            None,
            Normal
        }

        [SerializeField] private KindType _kind;
        [SerializeField] private SpriteLibraryAsset _spriteLib;

        public KindType Kind => _kind;
        public SpriteLibraryAsset SpriteLib => _spriteLib;
    }
}
