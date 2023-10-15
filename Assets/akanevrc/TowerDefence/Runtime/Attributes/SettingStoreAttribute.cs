using System;

namespace akanevrc.TowerDefence
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    public class SettingStoreAttribute : Attribute
    {
        public Type Type { get; }

        public SettingStoreAttribute(Type type)
        {
            Type = type;
        }
    }
}
