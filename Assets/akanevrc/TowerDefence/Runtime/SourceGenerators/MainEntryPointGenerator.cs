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
            var stageStores = TypeAttributeUtil.GetAllTypesWithAttribute<StageStoreAttribute>();
            var monoBehaviours = TypeAttributeUtil.GetAllTypesWithAttribute<MonoBehaviourAttribute>();
            var entityBehaviours = TypeAttributeUtil.GetAllTypesWithAttribute<EntityBehaviourAttribute>();
            var source =
$@"using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{{
    public partial class MainEntryPoint
    {{
        [Inject] private readonly IObjectResolver _resolver;
    {
        settingStores
        .Select(settingStore => $@"[Inject] private readonly {settingStore.GetTypeName()} {settingStore.GetSettingStoreVarName()};")
        .ToLines(8)
    }{
        settingss
        .Select(settings => $@"[Inject] private readonly {settings.GetTypeName()}[] {settings.GetArrayVarName()};")
        .ToLines(8)
    }{
        handlers
        .Select(handler => $@"[Inject] private readonly {handler.GetTypeName()} {handler.GetVarName()};")
        .ToLines(8)
    }{
        stageStores
        .Select(stageStore => $@"[Inject] private readonly {stageStore.GetTypeName()} {stageStore.GetVarName()};")
        .ToLines(8)
    }{
        monoBehaviours
        .Select(monoBehaviour => $@"[Inject] private readonly {monoBehaviour.GetTypeName()} {monoBehaviour.GetVarName()};")
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
        }{
            stageStores
            .Select(stageStore => $@"{stageStore.GetVarName()}?.Init(new StageNumber() {{ World = 1, Stage = 1 }});")
            .ToLines(12)
        }{
            monoBehaviours
            .Select(monoBehaviour => $@"_resolver.Instantiate({monoBehaviour.GetVarName()});")
            .ToLines(12)
        }
            _stageScheduler.SetStage(new StageNumber() {{ World = 1, Stage = 1 }});
        }}
    }}
}}
";
            return source.ToString();
        }
    }
}
