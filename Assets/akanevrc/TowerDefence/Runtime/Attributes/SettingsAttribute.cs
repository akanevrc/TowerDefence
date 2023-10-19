using System;

namespace akanevrc.TowerDefence
{
    public class SettingsAttribute : TypeAttribute
    {
        public SettingsAttribute(params Type[] genericParams)
            : base(genericParams)
        {
        }
    }
}
