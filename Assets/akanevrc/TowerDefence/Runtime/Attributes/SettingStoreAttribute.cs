using System;

namespace akanevrc.TowerDefence
{
    public class SettingStoreAttribute : TypeAttribute
    {
        public SettingStoreAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
