using System;

namespace akanevrc.TowerDefence
{
    public class PresenterAttribute : TypeAttribute
    {
        public PresenterAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
