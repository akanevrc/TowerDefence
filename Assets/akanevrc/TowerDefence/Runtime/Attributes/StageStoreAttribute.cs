using System;

namespace akanevrc.TowerDefence
{
    public class StageStoreAttribute : TypeAttribute
    {
        public StageStoreAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
