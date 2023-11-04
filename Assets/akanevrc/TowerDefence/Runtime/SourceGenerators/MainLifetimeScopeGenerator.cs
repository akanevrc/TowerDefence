using System.Linq;

namespace akanevrc.TowerDefence
{
    [SourceGenerator("MainLifetimeScope.g.cs")]
    public static class MainLifetimeScopeGenerator
    {
        public static string Generate()
        {
            var settings = TypeAttributeUtil.GetAllTypesWithAttribute<SettingAttribute>();
            var settingss = TypeAttributeUtil.GetAllTypesWithAttribute<SettingsAttribute>();
            var settingStores = TypeAttributeUtil.GetAllTypesWithAttribute<SettingStoreAttribute>();
            var stageStores = TypeAttributeUtil.GetAllTypesWithAttribute<StageStoreAttribute>();
            var factories = TypeAttributeUtil.GetAllTypesWithAttribute<FactoryAttribute>();
            var presenters = TypeAttributeUtil.GetAllTypesWithAttribute<PresenterAttribute>();
            var handlers = TypeAttributeUtil.GetAllTypesWithAttribute<HandlerAttribute>();
            var messages = TypeAttributeUtil.GetAllTypesWithAttribute<MessageAttribute>();
            var gameObjects = TypeAttributeUtil.GetAllTypesWithAttribute<GameObjectAttribute>();
            var source =
$@"using UnityEngine;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{{
    public partial class MainLifetimeScope
    {{{
        settings
        .Select(setting => $@"[SerializeField] private {setting.GetTypeName()} {setting.GetVarName()};")
        .ToLines(8)
    }{
        settingss
        .Select(settings => $@"[SerializeField] private {settings.GetTypeName()}[] {settings.GetArrayVarName()};")
        .ToLines(8)
    }{
        gameObjects
        .Select(gameObject => $@"[SerializeField] private {gameObject.GetTypeName()} {gameObject.GetVarName()};")
        .ToLines(8)
    }

        protected override void Configure(IContainerBuilder builder)
        {{{
            settings
            .Select(setting => $@"if ({setting.GetVarName()} != null) builder.RegisterInstance({setting.GetVarName()});")
            .ToLines(12)
        }{
            settingss
            .Select(settings => $@"if ({settings.GetArrayVarName()} != null) builder.RegisterInstance({settings.GetArrayVarName()});")
            .ToLines(12)
        }{
            settingStores
            .Select(settingStore => $@"builder.Register<{settingStore.GetTypeName()}>(Lifetime.Scoped);")
            .ToLines(12)
        }{
            stageStores
            .Select(stageStore => $@"builder.Register<{stageStore.GetTypeName()}>(Lifetime.Scoped);")
            .ToLines(12)
        }{
            factories
            .Select(factory => $@"builder.Register<{factory.GetTypeName()}>(Lifetime.Scoped);")
            .ToLines(12)
        }{
            factories
            .Select(factory => $@"builder.Register<{factory.GetFactoryInterfaceName()}, {factory.GetTypeName()}>(Lifetime.Scoped);")
            .ToLines(12)
        }{
            presenters
            .Select(presenter => $@"builder.Register<{presenter.GetTypeName()}>(Lifetime.Scoped);")
            .ToLines(12)
        }{
            handlers
            .Select(handler => $@"builder.Register<{handler.GetTypeName()}>(Lifetime.Scoped);")
            .ToLines(12)
        }{
            (messages.Any() ? new string[] { $@"var options = builder.RegisterMessagePipe();" } : Enumerable.Empty<string>())
            .Concat(
                messages
                .Select(message => $@"builder.RegisterMessageBroker<{message.GetTypeName()}>(options);")
            )
            .ToLines(12)
        }{
            gameObjects
            .Select(gameObject => $@"if ({gameObject.GetVarName()} != null) builder.RegisterComponent({gameObject.GetVarName()});")
            .ToLines(12)
        }
            builder.RegisterEntryPoint<MainEntryPoint>();
        }}
    }}
}}
";
            return source.ToString();
        }
    }
}
