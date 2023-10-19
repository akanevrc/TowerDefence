using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StageSetting")]
    [Settings]
    public class StageSetting : ScriptableObject, ISetting<StageNumber>
    {
        [SerializeField] private StageNumber _kind;
        [SerializeField] private int _waveCount;

        public StageNumber Kind => _kind;
        public int WaveCount => _waveCount;
    }
}
