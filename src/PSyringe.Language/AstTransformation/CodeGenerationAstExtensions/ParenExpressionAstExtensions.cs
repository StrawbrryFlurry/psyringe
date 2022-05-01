using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class ParenExpressionAstExtensions {
  public static string ToStringFromAst(this ParenExpressionAst ast) {
    var expression = ast.Pipeline.ToStringFromAst();

    return $"({expression})";
  }
}