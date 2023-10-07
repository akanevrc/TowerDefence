using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/MainConfig")]
    [Setting]
    public class MainConfig : ScriptableObject
    {
        [SerializeField] private float _modelLoopFrequency = 1.0F / 128.0F;
        public float ModelLoopFrequency => _modelLoopFrequency;
    }
}
