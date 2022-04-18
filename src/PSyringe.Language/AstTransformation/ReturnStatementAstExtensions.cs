using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class ReturnStatementAstExtensions {
  public static string ToStringFromAst(this ReturnStatementAst ast) {
    var expression = ast.Pipeline;
    if (expression is null) {
      return "return;";
    }

    var returnExpression = expression.ToStringFromAst();
    return $"return {returnExpression};";
  }
}