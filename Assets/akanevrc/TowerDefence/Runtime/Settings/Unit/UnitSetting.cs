using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/UnitSetting")]
    [Settings(typeof(UnitSetting))]
    public class UnitSetting : ScriptableObject, ISetting<UnitSetting.UnitKind>
    {
        public enum UnitKind
        {
            Normal
        }

        [SerializeField] private UnitKind _kind;
        [SerializeField] private int _maxLevel;
        [SerializeField] private float[] _attacks;
        [SerializeField] private float[] _ranges;

        public UnitKind Kind => _kind;
        public int MaxLevel => _maxLevel;
        public float[] Attacks => _attacks;
        public float[] Ranges => _ranges;
    }
}
