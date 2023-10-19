using System;

namespace akanevrc.TowerDefence
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    public abstract class TypeAttribute : Attribute
    {
        public Type[] GenericParams { get; }

        public TypeAttribute(Type[] genericParams)
        {
            GenericParams = genericParams;
        }
    }
}
