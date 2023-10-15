using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WaveSetting")]
    [Setting(typeof(EnemyWaveSetting))]
    public class EnemyWaveSetting : ScriptableObject, ISetting<WaveNumber>
    {
        [SerializeField] private WaveNumber _kind;
        [SerializeField] private EnemySeries[] _enemySeries;

        public WaveNumber Kind => _kind;
        public EnemySeries[] EnemySeries => _enemySeries;
    }
}
