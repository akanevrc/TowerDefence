using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WaveSetting")]
    public class EnemyWaveSetting : ScriptableObject
    {
        [SerializeField] private WaveNumber _waveNumber;
        [SerializeField] private EnemySeries[] _enemySeries;

        public WaveNumber WaveNumber => _waveNumber;
        public EnemySeries[] EnemySeries => _enemySeries;
    }
}
