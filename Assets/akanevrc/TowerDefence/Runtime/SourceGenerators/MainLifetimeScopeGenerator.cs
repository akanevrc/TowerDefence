using System;
using System.Linq;

namespace akanevrc.TowerDefence
{
    [SourceGenerator("MainLifetimeScope.g.cs")]
    public static class MainLifetimeScopeGenerator
    {
        public static string Generate()
        {
            var settings = TypeAttributeUtil.GetAllTypesWithAttribute<SettingAttribute>();
            var handlers = TypeAttributeUtil.GetAllTypesWithAttribute<HandlerAttribute>();
            var messages = TypeAttributeUtil.GetAllTypesWithAttribute<MessageAttribute>();
            var source =
$@"using UnityEngine;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{{
    public partial class MainLifetimeScope
    {{{
        string.Join("", settings.Select(setting => $@"{Environment.NewLine}        [SerializeField] private {setting.Name} {setting.GetVarName()};"))
    }

        protected override void Configure(IContainerBuilder builder)
        {{{
            string.Join("", settings.Select(setting => $@"{Environment.NewLine}            builder.RegisterInstance({setting.GetVarName()});"))
        }{
            string.Join("", handlers.Select(handler => $@"{Environment.NewLine}            builder.Register<{handler.Name}>(Lifetime.Scoped);"))
        }{
            string.Join("", messages.Select((message, i) => {
                var optionDef = i == 0 ? $@"{Environment.NewLine}            var options = builder.RegisterMessagePipe();" : "";
                return $@"{optionDef}{Environment.NewLine}            builder.RegisterMessageBroker<{message.Name}>(options);";
            }))
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
