using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StageSetting")]
    public class StageSetting : ScriptableObject
    {
        [SerializeField] private StageNumber _stageNumber;
        [SerializeField] private int _waveCount;

        public StageNumber StageNumber => _stageNumber;
        public int WaveCount => _waveCount;
    }
}
