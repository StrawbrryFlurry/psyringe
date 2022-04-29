using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class SubExpressionAstExtensions {
  public static string ToStringFromAst(this SubExpressionAst ast) {
    var subExpression = ast.SubExpression.ToStringFromAst(false);
    return $"$({subExpression})";
  }
}