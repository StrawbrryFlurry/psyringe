using System.Management.Automation.Language;
using PSyringe.Language.AstTransformation.SyntheticAsts;

namespace PSyringe.Language.AstTransformation;

public static class SyntaxFactory {
  public static StatementAst ToStatement(this ExpressionAst expression) {
    return new SyntheticExpressionAst(expression.Extent, expression);
  }
}