using System;

namespace akanevrc.TowerDefence
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class MessageAttribute : Attribute
    {
    }
}
