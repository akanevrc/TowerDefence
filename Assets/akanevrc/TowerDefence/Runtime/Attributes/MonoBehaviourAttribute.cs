using System;

namespace akanevrc.TowerDefence
{
    public class MonoBehaviourAttribute : TypeAttribute
    {
        public MonoBehaviourAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
