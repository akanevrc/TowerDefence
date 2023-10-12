using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/PedestalSetting")]
    public class PedestalSetting : ScriptableObject
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
