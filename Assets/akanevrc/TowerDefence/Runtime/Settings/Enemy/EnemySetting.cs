using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemySetting")]
    public class EnemySetting : ScriptableObject, ISetting<EnemySetting.EnemyKind>
    {
        public enum EnemyKind
        {
            Normal
        }

        [SerializeField] private EnemyKind _kind;
        [SerializeField] private float _health;
        [SerializeField] private float _attack;
        [SerializeField] private float _velocity;

        public EnemyKind Kind => _kind;
        public float Health => _health;
        public float Attack => _attack;
        public float Velocity => _velocity;
    }
}
