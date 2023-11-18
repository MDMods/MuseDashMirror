using System.Collections.Immutable;
using System.Reflection;
using MelonLoader;
using MuseDashMirror.Attributes;
using MuseDashMirror.Attributes.EventAttributes;
using MuseDashMirror.Extensions.CollectionExtensions;

namespace MuseDashMirror;

/// <summary>
///     Apply events and logger by using attribute reflection from assembly
/// </summary>
internal static class AttributeProcessor
{
    private static readonly ImmutableArray<Type> EventAttributes = ImmutableArray.Create(
        typeof(EnterGameSceneAttribute), typeof(ExitGameSceneAttribute),
        typeof(EnterMainSceneAttribute), typeof(ExitMainSceneAttribute),
        typeof(EnterLoadingSceneAttribute), typeof(ExitLoadingSceneAttribute),
        typeof(MenuSelectEventAttribute), typeof(PnlMenuEventAttribute),
        typeof(PnlStageEventAttribute), typeof(SwitchLanguageEventAttribute)
    );

    /// <summary>
    ///     Process attributes from assemblies
    /// </summary>
    internal static void ProcessAttributeFromAssemblies()
    {
        var melonMods = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => MelonUtils.PullAttributeFromAssembly<MelonInfoAttribute>(x) is not null);

        foreach (var melonMod in melonMods)
        {
            ApplyLoggerFromTypes(melonMod);
            ApplyAllEventsFromAssembly(melonMod);
        }
    }

    /// <summary>
    ///     Apply logger to the field which used the attribute <see cref="LoggerAttribute" />
    /// </summary>
    /// <param name="assembly"></param>
    private static void ApplyLoggerFromTypes(Assembly assembly)
    {
        var members = GetTypesFromAssembly(assembly).GetMemberInfosFromTypesByAttribute<LoggerAttribute>();
        foreach (var member in members)
        {
            switch (member)
            {
                case FieldInfo field when field.FieldType != typeof(MelonLogger.Instance):
                    MelonLogger.Error(
                        $"Field {field.Name} from assembly {assembly.FullName}, class {field.DeclaringType?.Name} is not Type MelonLogger.Instance");
                    continue;

                case FieldInfo field:
                    field.SetValue(null, new MelonLogger.Instance(field.DeclaringType?.Name));
                    break;

                case PropertyInfo property when property.PropertyType != typeof(MelonLogger.Instance):
                    MelonLogger.Error(
                        $"Property {property.Name} from assembly {assembly.FullName}, class {property.DeclaringType?.Name} is not Type MelonLogger.Instance");
                    continue;

                case PropertyInfo property:
                    property.SetValue(null, new MelonLogger.Instance(property.DeclaringType?.Name));
                    break;
            }
        }
    }

    /// <summary>
    ///     Apply events from assembly which used the attribute
    ///     <see cref="PnlMenuEventAttribute" />, <see cref="PnlStageEventAttribute" />
    /// </summary>
    /// <param name="assembly"></param>
    private static void ApplyAllEventsFromAssembly(Assembly assembly) =>
        EventAttributes.Execute(t => ApplyEventsFromAssembly(assembly, t));


    /// <summary>
    ///     Apply events from assembly which used the attribute
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="attributeType"></param>
    private static void ApplyEventsFromAssembly(Assembly assembly, Type attributeType)
    {
        var methods = GetTypesFromAssembly(assembly).GetMethodInfosFromTypesByAttribute(attributeType);
        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute(attributeType);
            if (attribute is IEventAttribute eventAttribute)
            {
                eventAttribute.Register(method);
            }
        }
    }
}