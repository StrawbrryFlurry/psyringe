using System.Management.Automation.Language;
using PSyringe.Language.TypeLoader;

namespace PSyringe.Language.AstTransformation;

/// <summary>
///   Extension methods for base AST types. Uses reflection
///   to get the extension method for the concrete implementation
///   of the runtime AST type.
/// </summary>
public static class AstBaseClassExtensions {
  public static string ToStringFromAst(this Ast ast) {
    return ast.InvokeExtensionMethodInAssemblyForConcreteType<string>(nameof(ToStringFromAst));
  }

  /// <summary>
  ///   For certain AST types like <see cref="StatementBlockAst" />, the brackets
  ///   may or may not be included as part of the parent. For example, the
  ///   FunctionDefinitionAst has a StatementBlockAst as its body, but the brackets
  ///   are added by the FunctionDefinitionAst itself.
  /// </summary>
  public static bool AreStatementBracketsIncluded(this Ast ast) {
    try {
      return ast.InvokeExtensionMethodInAssemblyForConcreteType<bool>(nameof(AreStatementBracketsIncluded));
    }
    // Could not find extension method for concrete type.
    catch {
      // By default the brackets are not included.
      return false;
    }
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