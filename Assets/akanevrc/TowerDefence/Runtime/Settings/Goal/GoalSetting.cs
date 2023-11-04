using UnityEngine;
using UnityEngine.U2D.Animation;

namespace akanevrc.TowerDefence
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GoalSetting")]
    [Settings]
    public class GoalSetting : ScriptableObject, IEntitySetting<StageNumber>
    {
        [SerializeField] private StageNumber _kind;
        [SerializeField] private int _health;
        [SerializeField] private SpriteLibraryAsset _spriteLib;

        public StageNumber Kind => _kind;
        public float Health => _health;
        public SpriteLibraryAsset SpriteLib => _spriteLib;
    }
}
