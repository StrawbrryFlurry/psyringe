using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class ArrayLiteralAstExtensions {
  public static string ToStringFromAst(this ArrayLiteralAst ast) {
    var elements = ast.Elements.ToStringFromAstJoinBy(", ")!;
    return elements;
  }
}