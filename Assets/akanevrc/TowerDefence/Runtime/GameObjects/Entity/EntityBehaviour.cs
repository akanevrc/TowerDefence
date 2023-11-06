using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace akanevrc.TowerDefence
{
    public abstract class EntityBehaviour<T, TKind> : MonoBehaviour
        where T : struct
        where TKind : struct
    {
        [NonSerialized] public int Id;
        [NonSerialized] public IEntitySetting<TKind> Setting;

        void Start()
        {
            if (Setting.SpriteLib != null)
            {
                GetComponent<SpriteLibrary>().spriteLibraryAsset = Setting.SpriteLib;
            }
        }
    }
}
