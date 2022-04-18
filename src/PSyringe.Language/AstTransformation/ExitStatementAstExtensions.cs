using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class ExitStatementAstExtensions {
  public static string ToStringFromAst(this ExitStatementAst ast) {
    var expression = ast.Pipeline;
    if (expression is null) {
      return "exit;";
    }

    var exitExpression = expression.ToStringFromAst();
    return $"exit {exitExpression};";
  }
}