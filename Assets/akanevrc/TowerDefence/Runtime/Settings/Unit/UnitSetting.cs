using UnityEngine;
using UnityEngine.U2D.Animation;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UnitSetting")]
    [Settings]
    public class UnitSetting : ScriptableObject, IEntitySetting<UnitSetting.KindType>
    {
        public enum KindType
        {
            Normal
        }

        [SerializeField] private KindType _kind;
        [SerializeField] private int _maxLevel;
        [SerializeField] private float[] _attacks;
        [SerializeField] private float[] _ranges;
        [SerializeField] private SpriteLibraryAsset _spriteLib;

        public KindType Kind => _kind;
        public int MaxLevel => _maxLevel;
        public float[] Attacks => _attacks;
        public float[] Ranges => _ranges;
        public SpriteLibraryAsset SpriteLib => _spriteLib;
    }
}
