using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class TryStatementAstExtensions {
  public static string ToStringFromAst(this TryStatementAst ast) {
    var catchClauses = ast.CatchClauses?.ToStringFromAstJoinBy(NewLine);
    var finallyClause = ast.Finally?.ToStringFromAst();
    var body = ast.Body.ToStringFromAst();

    var tryStatement = new StringBuilder();

    tryStatement.Append("try ");
    tryStatement.AppendLine(body);

    if (catchClauses is not null) {
      tryStatement.AppendLine(catchClauses);
    }

    if (finallyClause is not null) {
      tryStatement.Append("finally ");
      tryStatement.Append(finallyClause);
    }

    return tryStatement.ToString();
  }
}