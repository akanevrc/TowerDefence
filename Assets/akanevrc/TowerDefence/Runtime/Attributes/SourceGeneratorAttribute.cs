using System;

namespace akanevrc.TowerDefence
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SourceGeneratorAttribute : Attribute
    {
        public string GeneratedFileName { get; }

        public SourceGeneratorAttribute(string generatedFileName)
        {
            GeneratedFileName = generatedFileName;
        }
    }
}
