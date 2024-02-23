namespace MuseDashMirror.SourceGenerators;

internal static class DiagnosticDescriptors
{
    internal static readonly DiagnosticDescriptor LoggerAttributeForNonPartialClassError = new(
        "MDM0000",
        GetLocalizableString(nameof(MDM0000Title)),
        GetLocalizableString(nameof(MDM0000MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0000Description)));

    internal static readonly DiagnosticDescriptor PatchEventAttributeInvalidArgsError = new(
        "MDM0100",
        GetLocalizableString(nameof(MDM0100Title)),
        GetLocalizableString(nameof(MDM0100MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0100Description)));

    internal static readonly DiagnosticDescriptor SceneEventAttributeInvalidArgsError = new(
        "MDM0101",
        GetLocalizableString(nameof(MDM0101Title)),
        GetLocalizableString(nameof(MDM0101MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0101Description)));

    internal static readonly DiagnosticDescriptor EventAttributeInvalidReturnTypeError = new(
        "MDM0102",
        GetLocalizableString(nameof(MDM0102Title)),
        GetLocalizableString(nameof(MDM0102MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0102Description)));

    internal static readonly DiagnosticDescriptor EventAttributeNonStaticMethodForStaticConstructorError = new(
        "MDM0103",
        GetLocalizableString(nameof(MDM0103Title)),
        GetLocalizableString(nameof(MDM0103MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0103Description)));

    internal static readonly DiagnosticDescriptor EventAttributeInNonPartialClassError = new(
        "MDM0104",
        GetLocalizableString(nameof(MDM0104Title)),
        GetLocalizableString(nameof(MDM0104MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0104Description)));

    internal static readonly DiagnosticDescriptor RegisterInMuseDashMirrorSuggestion = new(
        "MDM0105",
        GetLocalizableString(nameof(MDM0105Title)),
        GetLocalizableString(nameof(MDM0105MessageFormat)),
        "Usage",
        DiagnosticSeverity.Warning,
        true,
        GetLocalizableString(nameof(MDM0105Description)));
}