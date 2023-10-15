using System.Linq;

namespace akanevrc.TowerDefence
{
    [SourceGenerator("MainEntryPoint.g.cs")]
    public static class MainEntryPointGenerator
    {
        public static string Generate()
        {
            var settingStores = TypeAttributeUtil.GetAllTypesWithAttribute<SettingStoreAttribute>();
            var settingss = TypeAttributeUtil.GetAllTypesWithAttribute<SettingsAttribute>();
            var handlers = TypeAttributeUtil.GetAllTypesWithAttribute<HandlerAttribute>();
            var source =
$@"using VContainer;

namespace akanevrc.TowerDefence
{{
    public partial class MainEntryPoint
    {{
        [Inject] private IObjectResolver _resolver;
    {
        settingStores
        .Select(settingStore => $@"[Inject] private {settingStore.GetTypeName()} {settingStore.GetSettingStoreVarName()};")
        .ToLines(8)
    }{
        settingss
        .Select(settings => $@"[Inject] private {settings.GetTypeName()}[] {settings.GetArrayVarName()};")
        .ToLines(8)
    }{
        handlers
        .Select(handler => $@"[Inject] private {handler.GetTypeName()} {handler.GetVarName()};")
        .ToLines(8)
    }

        partial void Init()
        {{{
            settingStores.Join
            (
                settingss,
                settingStore => settingStore.GetGenericArguments()[1],
                settings => settings,
                (settingStore, settings) =>
                    $@"if ({settings.GetArrayVarName()} != null) {settingStore.GetSettingStoreVarName()}?.Init({settings.GetArrayVarName()});"
            )
            .ToLines(12)
        }{
            handlers
            .Select(handler => $@"{handler.GetVarName()}?.Init();")
            .ToLines(12)
        }
        }}
    }}
}}
";
            return source.ToString();
        }
    }
}
