using System;

namespace akanevrc.TowerDefence
{
    public static class SourceGeneratorUtil
    {
        public static string GetVarName(this Type type)
        {
            return $"_{char.ToLower(type.Name[0])}{type.Name[1..]}";
        }
    }
}
