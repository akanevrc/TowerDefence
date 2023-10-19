using UnityEngine;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/EnemySetting")]
    [Settings]
    public class EnemySetting : ScriptableObject, ISetting<EnemySetting.KindType>
    {
        public enum KindType
        {
            Normal
        }

        [SerializeField] private KindType _kind;
        [SerializeField] private float _health;
        [SerializeField] private float _attack;
        [SerializeField] private float _velocity;

        public KindType Kind => _kind;
        public float Health => _health;
        public float Attack => _attack;
        public float Velocity => _velocity;
    }
}
