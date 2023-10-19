using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UnitSetting")]
    [Settings]
    public class UnitSetting : ScriptableObject, ISetting<UnitSetting.KindType>
    {
        public enum KindType
        {
            Normal
        }

        [SerializeField] private KindType _kind;
        [SerializeField] private int _maxLevel;
        [SerializeField] private float[] _attacks;
        [SerializeField] private float[] _ranges;

        public KindType Kind => _kind;
        public int MaxLevel => _maxLevel;
        public float[] Attacks => _attacks;
        public float[] Ranges => _ranges;
    }
}
