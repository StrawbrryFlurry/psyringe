using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class BinaryExpressionAstExtensions {
  public static string ToStringFromAst(this BinaryExpressionAst ast) {
    var left = ast.Left.ToStringFromAst();
    var right = ast.Right.ToStringFromAst();
    var @operator = ast.Operator.Text();

    return $"{left} {@operator} {right}";
  }
}