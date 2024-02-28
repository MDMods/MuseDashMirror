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

    internal static readonly DiagnosticDescriptor InheritedMelonModClassNonPartialError = new(
        "MDM0001",
        GetLocalizableString(nameof(MDM0001Title)),
        GetLocalizableString(nameof(MDM0001MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0001Description)));

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

    internal static readonly DiagnosticDescriptor PnlMenuToggleAttributeInNonePartialClassError = new(
        "MDM0200",
        GetLocalizableString(nameof(MDM0200Title)),
        GetLocalizableString(nameof(MDM0200MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0200Description)));

    internal static readonly DiagnosticDescriptor PnlMenuToggleAttributeOnNonGameObjectError = new(
        "MDM0201",
        GetLocalizableString(nameof(MDM0201Title)),
        GetLocalizableString(nameof(MDM0201MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0201Description)));

    internal static readonly DiagnosticDescriptor PnlMenuToggleAttributeOnNonStaticGameObjectError = new(
        "MDM0202",
        GetLocalizableString(nameof(MDM0202Title)),
        GetLocalizableString(nameof(MDM0202MessageFormat)),
        "Usage",
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0202Description)));
}