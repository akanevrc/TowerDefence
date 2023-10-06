using System;
using System.Collections.Generic;
using System.Reflection;

namespace akanevrc.TowerDefence
{
    public static class TypeAttributeUtil
    {
        public static IEnumerable<Type> GetAllTypesWithAttribute<T>()
            where T : Attribute
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.HasAttribute<T>())
                {
                    yield return type;
                }
            }
        }

        public static bool HasAttribute<T>(this Type type)
            where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), false).Length > 0;
        }
    }
}
