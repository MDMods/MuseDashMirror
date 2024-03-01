namespace MuseDashMirror.SourceGenerators;

internal static class Utils
{
    internal static Location GetClassDeclarationLocation(ClassDeclarationSyntax classDeclaration) =>
        Location.Create(classDeclaration.SyntaxTree, TextSpan.FromBounds(classDeclaration.Keyword.Span.Start, classDeclaration.Identifier.Span.End));

    internal static LocalizableString GetLocalizableString(string name) =>
        new LocalizableResourceString(name, ResourceManager, typeof(Resources));
}