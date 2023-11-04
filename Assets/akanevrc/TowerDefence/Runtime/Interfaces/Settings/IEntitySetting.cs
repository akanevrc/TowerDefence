using UnityEngine.U2D.Animation;

namespace akanevrc.TowerDefence
{
    public interface IEntitySetting<TKind> : ISetting<TKind>
        where TKind : struct
    {
        SpriteLibraryAsset SpriteLib { get; }
    }
}
