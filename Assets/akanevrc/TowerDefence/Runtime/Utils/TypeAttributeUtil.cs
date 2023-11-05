using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace akanevrc.TowerDefence
{
    public static class TypeAttributeUtil
    {
        public static IEnumerable<Type> GetAllTypesWithAttribute<T>()
            where T : TypeAttribute
        {
            return GetAllTypesAndDataWithAttribute<T>().Select(x => x.type);
        }

        public static IEnumerable<(Type type, string additionalData)> GetAllTypesAndDataWithAttribute<T>()
            where T : TypeAttribute
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var attribute in type.GetCustomAttributes(typeof(T), false))
                {
                    var typeAttribute = (T)attribute;
                    var genericParams = typeAttribute.GenericParams;
                    var concreteType = genericParams.Length == 0 ? type : type.MakeGenericType(genericParams);
                    yield return (concreteType, typeAttribute.AdditionalData);
                }
            }
        }
    }
}
