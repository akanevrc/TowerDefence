using System;

namespace akanevrc.TowerDefence
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    public class TypeAttribute : Attribute
    {
        public Type Type { get; }

        public TypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
