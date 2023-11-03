using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace akanevrc.TowerDefence
{
    public static class SourceGeneratorUtil
    {
        public static string GetTypeName(this Type type)
        {
            return
                (type.DeclaringType == null ? "" : $"{GetTypeName(type.DeclaringType)}.") +
                GetName(type) +
                (type.IsGenericType ? $"<{string.Join(", ", type.GenericTypeArguments.Select(t => t.GetTypeName()))}>" : "");
        }

        public static string GetFactoryInterfaceName(this Type type)
        {
            var name = Regex.Replace(GetName(type), @"(.+)Factory", "$1");
            return $"IEntityFactory<{name}, {name}Factory.FactoryParams>";
        }

        public static string GetVarName(this Type type)
        {
            var name = GetName(type);
            return $"_{char.ToLower(name[0])}{name[1..]}";
        }

        public static string GetArrayVarName(this Type type)
        {
            return $"{type.GetVarName()}s";
        }

        public static string GetSettingStoreVarName(this Type type)
        {
            return $"{type.GetGenericArguments()[1].GetVarName()}Store";
        }

        private static string GetName(Type type)
        {
            return Regex.Replace(type.Name, @"(.+\+)*(.+?)(`.+)?", "$2");
        }

        public static string ToLines(this IEnumerable<string> sources, int indent)
        {
            return string.Join("", sources.Select(source => $"{Environment.NewLine}{new string(' ', indent)}{source}"));
        }
    }
}
