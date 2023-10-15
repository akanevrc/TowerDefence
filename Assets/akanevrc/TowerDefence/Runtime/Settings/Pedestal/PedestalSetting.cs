using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PedestalSetting")]
    [Settings(typeof(PedestalSetting))]
    public class PedestalSetting : ScriptableObject, ISetting<PedestalSetting.PedestalKind>
    {
        public enum PedestalKind
        {
            None,
            Normal
        }

        [SerializeField] private PedestalKind _kind;

        public PedestalKind Kind => _kind;
    }
}
