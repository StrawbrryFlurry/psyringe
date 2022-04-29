using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class ArrayExpressionAstExtensions {
  public static string ToStringFromAst(this ArrayExpressionAst ast) {
    var elements = ast.SubExpression.Statements.ToStringFromAstJoinBy(", ");
    // There should not be any scenario where this ever comes up but
    // just to be safe we include traps as well.
    var traps = ast.SubExpression.Traps?.ToStringFromAstJoinBy(NewLine);

    var arrayExpression = new StringBuilder();

    arrayExpression.Append("@(");
    arrayExpression.Append(elements);

    if (traps is not null) {
      arrayExpression.AppendLine(";");
      arrayExpression.Append(traps);
    }

    arrayExpression.Append(')');

    return arrayExpression.ToString();
  }
}