using System;

namespace akanevrc.TowerDefence
{
    public class SettingAttribute : TypeAttribute
    {
        public SettingAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
