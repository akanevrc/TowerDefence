using System;

namespace akanevrc.TowerDefence
{
    public class MessageAttribute : TypeAttribute
    {
        public MessageAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
