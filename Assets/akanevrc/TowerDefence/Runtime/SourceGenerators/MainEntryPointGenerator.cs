using System;
using System.Linq;

namespace akanevrc.TowerDefence
{
    [SourceGenerator("MainEntryPoint.g.cs")]
    public static class MainEntryPointGenerator
    {
        public static string Generate()
        {
            var handlers = TypeAttributeUtil.GetAllTypesWithAttribute<HandlerAttribute>();
            var source =
$@"using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{{
    public partial class MainEntryPoint
    {{{string.Join("", handlers.Select(handler => $@"{Environment.NewLine}        [Inject] private {handler.Name} {GetFieldName(handler)};"))}

        private partial void HoldHandlers()
        {{{string.Join("", handlers.Select(handler => $@"{Environment.NewLine}            _disposables.Add({GetFieldName(handler)});"))}
        }}
    }}
}}
";
            return source.ToString();
        }
        
        private static string GetFieldName(Type type)
        {
            return $"_{char.ToLower(type.Name[0])}{type.Name[1..]}";
        }
    }
}
