using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StageSetting")]
    [Settings]
    public class StageSetting : ScriptableObject, ISetting<StageNumber>
    {
        [SerializeField] private StageNumber _kind;
        [SerializeField] private int _waveCount;
        [SerializeField] private Vector2Int[] _route;

        public StageNumber Kind => _kind;
        public int WaveCount => _waveCount;
        public Vector2Int[] Route => _route;
    }
}
