using System;
using System.Collections.Generic;
using System.Reflection;

namespace akanevrc.TowerDefence
{
    public static class TypeAttributeUtil
    {
        public static IEnumerable<Type> GetAllTypesWithAttribute<T>()
            where T : TypeAttribute
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var attribute in type.GetCustomAttributes(typeof(T), false))
                {
                    var genericParams = ((T)attribute).GenericParams;

                    if (genericParams.Length == 0)
                    {
                        yield return type;
                    }
                    else
                    {
                        yield return type.MakeGenericType(genericParams);
                    }
                }
            }
        }
    }
}
