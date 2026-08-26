namespace MuseDashMirror.CodeAnalysis;

internal static class DiagnosticDescriptors
{
    private const string UsageCategory = "Usage";

    internal static readonly DiagnosticDescriptor PatchEventAttributeInvalidArgsError = new(
        "MDM0000",
        GetLocalizableString(nameof(MDM0000Title)),
        GetLocalizableString(nameof(MDM0000MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0000Description)));

    internal static readonly DiagnosticDescriptor SceneEventAttributeInvalidArgsError = new(
        "MDM0001",
        GetLocalizableString(nameof(MDM0001Title)),
        GetLocalizableString(nameof(MDM0001MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0001Description)));

    internal static readonly DiagnosticDescriptor EventAttributeInvalidReturnTypeError = new(
        "MDM0002",
        GetLocalizableString(nameof(MDM0002Title)),
        GetLocalizableString(nameof(MDM0002MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0002Description)));

    internal static readonly DiagnosticDescriptor EventAttributeNonStaticMethodForStaticConstructorError = new(
        "MDM0003",
        GetLocalizableString(nameof(MDM0003Title)),
        GetLocalizableString(nameof(MDM0003MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0003Description)));

    internal static readonly DiagnosticDescriptor PnlMenuToggleAttributeOnNonGameObjectError = new(
        "MDM0004",
        GetLocalizableString(nameof(MDM0004Title)),
        GetLocalizableString(nameof(MDM0004MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0004Description)));

    internal static readonly DiagnosticDescriptor PnlMenuToggleAttributeOnNonStaticGameObjectError = new(
        "MDM0005",
        GetLocalizableString(nameof(MDM0005Title)),
        GetLocalizableString(nameof(MDM0005MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0005Description)));

    internal static readonly DiagnosticDescriptor PnlMenuToggleAttributeOnMultipleFieldsError = new(
        "MDM0006",
        GetLocalizableString(nameof(MDM0006Title)),
        GetLocalizableString(nameof(MDM0006MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0006Description)));

    internal static readonly DiagnosticDescriptor PnlMenuToggleAttributeArgumentIsNotNameofError = new(
        "MDM0007",
        GetLocalizableString(nameof(MDM0007Title)),
        GetLocalizableString(nameof(MDM0007MessageFormat)),
        UsageCategory,
        DiagnosticSeverity.Error,
        true,
        GetLocalizableString(nameof(MDM0007Description)));
}
