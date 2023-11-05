using System;

namespace akanevrc.TowerDefence
{
    public class EntityBehaviourAttribute : TypeAttribute
    {
        public EntityBehaviourAttribute(string kindTypeName, params Type[] genericParams)
            : base(genericParams, kindTypeName)
        {
        }
    }
}
