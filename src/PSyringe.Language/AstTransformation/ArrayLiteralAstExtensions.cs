using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class ArrayLiteralAstExtensions {
  /// <summary>
  ///   An array literal would usually be represented as
  ///   an expression without @() but, for the sake of
  ///   simplicity, we don't differentiate them from
  ///   `ArrayExpressionAst`.
  /// </summary>
  public static string ToStringFromAst(this ArrayLiteralAst ast) {
    return MakeArrayExpression(ast.Elements);
  }

  private static string MakeArrayExpression(IEnumerable<Ast> elements) {
    var elementsAsString = elements.Select(e => e.ToStringFromAst()).ToList();
    var elementsAsStringJoined = string.Join(", ", elementsAsString);

    return $"@({elementsAsStringJoined})";
  }
}