using System.Management.Automation.Language;
using PSyringe.Language.TypeLoader;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

/// <summary>
///   Extension methods for base AST types. Uses reflection
///   to get the extension method for the concrete implementation
///   of the runtime AST type.
/// </summary>
public static class AstBaseClassExtensions {
  private static readonly ExtensionMethodFinder _toStringFromAstMethodFinder = new(nameof(ToStringFromAst));
  private static readonly ExtensionMethodFinder _replaceChildMethodFinder = new("ReplaceChildCore");

  public static string ToStringFromAst(this Ast ast) {
    return _toStringFromAstMethodFinder.InvokeExtensionMethodInAssemblyForConcreteType<string>(ast);
  }

  /// <summary>
  ///   Replaces a child node of an AST with a new node.
  /// </summary>
  /// <param name="ast"></param>
  /// <param name="child">The node that should be replaced (By reference)</param>
  /// <param name="replacement">The replacement for the node</param>
  /// <returns>Whether a node was replaced or not</returns>
  public static bool ReplaceChild(this Ast ast, Ast child, Ast replacement) {
    // When the replace method is called on an AST node
    // to replace a child that is the node itself, the
    // node is replaced instead. To do so, we call the
    // replace method in the parent node.
    if (ast.Is(child)) {
      ast.Parent.ReplaceChild(child, replacement);
      return true;
    }

    try {
      return _replaceChildMethodFinder.InvokeExtensionMethodInAssemblyForConcreteType<bool>(ast, child, replacement);
    }
    catch (Exception e) {
      throw new Exception($@"Cannot invoke `ReplaceChild` on an edge AST node ""{ast.GetType().Name}"". " +
                          "Instead, invoke the method on it's parent node to replace the AST entirely.\n" +
                          e);
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