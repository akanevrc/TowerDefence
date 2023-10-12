using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GoalSetting")]
    public class GoalSetting : ScriptableObject
    {
        [SerializeField] private StageNumber _stageNumber;
        [SerializeField] private int _health;

        public StageNumber StageNumber => _stageNumber;
        public float Health => _health;
    }
}
