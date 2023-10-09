using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace akanevrc.TowerDefence
{
    public static class SourceGeneratorUtil
    {
        public static string GetTypeName(this Type type)
        {
            return type.GenericTypeArguments.Length == 0 ?
                GetName(type) :
                $"{GetName(type)}<{string.Join(", ", type.GenericTypeArguments.Select(t => t.GetTypeName()))}>";
        }

        public static string GetVarName(this Type type)
        {
            var name = GetName(type);
            return $"_{char.ToLower(name[0])}{name[1..]}";
        }

        private static string GetName(Type type)
        {
            return Regex.Replace(type.Name, @"(.+\+)?(.+?)(`.+)?", "$2");
        }
    }
}
