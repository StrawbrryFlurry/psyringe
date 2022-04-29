using System.Management.Automation.Language;
using PSyringe.Language.TypeLoader;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

/// <summary>
///   Extension methods for base AST types. Uses reflection
///   to get the extension method for the concrete implementation
///   of the runtime AST type.
/// </summary>
public static class AstBaseClassExtensions {
  private static readonly ExtensionMethodFinder _extensionMethodFinder = new(nameof(ToStringFromAst));

  public static string ToStringFromAst(this Ast ast) {
    return _extensionMethodFinder.InvokeExtensionMethodInAssemblyForConcreteType<string>(ast);
  }

  public static IList<string> ToStringListFromAst(this IEnumerable<Ast> asts) {
    var astStrings = asts.Select(a => a.ToStringFromAst()).ToList();
    return astStrings;
  }

  /// <summary>
  ///   Joins the AST's string representations with a given separator.
  ///   Returns null if the the enumerable is empty.
  /// </summary>
  public static string? ToStringFromAstJoinBy(this IEnumerable<Ast> asts, string joinBy) {
    var astStrings = asts.ToStringListFromAst();

    if (!astStrings.Any()) {
      return null;
    }

    var joinedString = string.Join(joinBy, astStrings);
    return joinedString;
  }

  internal static string? JoinBy(this IEnumerable<string> strings, string joinBy) {
    var joinedString = string.Join(joinBy, strings);

    if (string.IsNullOrWhiteSpace(joinedString)) {
      return null;
    }

    return joinedString;
  }
}