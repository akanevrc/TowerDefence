using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GoalSetting")]
    [Settings]
    public class GoalSetting : ScriptableObject, ISetting<StageNumber>
    {
        [SerializeField] private StageNumber _kind;
        [SerializeField] private int _health;

        public StageNumber Kind => _kind;
        public float Health => _health;
    }
}
