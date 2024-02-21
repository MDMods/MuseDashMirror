namespace MuseDashMirror.SourceGenerators;

internal static class DiagnosticDescriptors
{
    internal static readonly DiagnosticDescriptor LoggerAttributeForNonPartialClassError = new(
        "MDM0000",
        GetLocalizableString(nameof(MDM0000Title)),
        GetLocalizableString(nameof(MDM0000MessageFormat)),
        typeof(LoggerAnalyzer).FullName!,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0000Description)));

    internal static readonly DiagnosticDescriptor PatchEventAttributeInvalidArgsError = new(
        "MDM0100",
        GetLocalizableString(nameof(MDM0100Title)),
        GetLocalizableString(nameof(MDM0100MessageFormat)),
        typeof(PatchEventAnalyzer).FullName!,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0100Description)));

    internal static readonly DiagnosticDescriptor SceneEventAttributeInvalidArgsError = new(
        "MDM0101",
        GetLocalizableString(nameof(MDM0101Title)),
        GetLocalizableString(nameof(MDM0101MessageFormat)),
        typeof(SceneEventAnalyzer).FullName!,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0101Description)));

    internal static readonly DiagnosticDescriptor EventAttributeInvalidReturnTypeError = new(
        "MDM0102",
        GetLocalizableString(nameof(MDM0102Title)),
        GetLocalizableString(nameof(MDM0102MessageFormat)),
        typeof(EventAnalyzer).FullName!,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0102Description)));

    internal static readonly DiagnosticDescriptor EventAttributeNonStaticMethodForStaticConstructorError = new(
        "MDM0103",
        GetLocalizableString(nameof(MDM0103Title)),
        GetLocalizableString(nameof(MDM0103MessageFormat)),
        typeof(EventAnalyzer).FullName!,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0103Description)));
}