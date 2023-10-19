using System;

namespace akanevrc.TowerDefence
{
    public class HandlerAttribute : TypeAttribute
    {
        public HandlerAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
