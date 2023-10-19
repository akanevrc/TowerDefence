using System;

namespace akanevrc.TowerDefence
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    public class TypeAttribute : Attribute
    {
        public Type[] GenericParams { get; }

        public TypeAttribute(params Type[] genericParams)
        {
            GenericParams = genericParams;
        }
    }
}
