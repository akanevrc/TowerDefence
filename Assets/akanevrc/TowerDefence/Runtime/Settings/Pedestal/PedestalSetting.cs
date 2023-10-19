using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PedestalSetting")]
    [Settings]
    public class PedestalSetting : ScriptableObject, ISetting<PedestalSetting.KindType>
    {
        public enum KindType
        {
            None,
            Normal
        }

        [SerializeField] private KindType _kind;

        public KindType Kind => _kind;
    }
}
