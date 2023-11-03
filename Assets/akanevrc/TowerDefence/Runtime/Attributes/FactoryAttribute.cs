using System;

namespace akanevrc.TowerDefence
{
    public class FactoryAttribute : TypeAttribute
    {
        public FactoryAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
