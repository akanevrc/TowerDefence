using System;

namespace akanevrc.TowerDefence
{
    public class GameObjectAttribute : TypeAttribute
    {
        public GameObjectAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
