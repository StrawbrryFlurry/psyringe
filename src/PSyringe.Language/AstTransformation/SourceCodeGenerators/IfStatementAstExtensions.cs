using System.Management.Automation.Language;
using System.Text;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class IfStatementAstExtensions {
  public static string ToStringFromAst(this IfStatementAst ast) {
    var ifStatement = new IfStatement(ast.Clauses, ast.ElseClause);
    return ifStatement.ToString();
  }

  // Utility class to represent an if statement
  // Mostly keep track of the keyword
  internal class IfStatement {
    public IList<Tuple<PipelineBaseAst, StatementBlockAst>> Clauses { get; }
    public StatementBlockAst? ElseClause { get; }
    private bool _isFirstIf { get; set; } = true;

    public IfStatement(IList<Tuple<PipelineBaseAst, StatementBlockAst>> clauses, StatementBlockAst? elseClause) {
      Clauses = clauses;
      ElseClause = elseClause;
    }

    public override string ToString() {
      var statement = new StringBuilder();

      foreach (var (condition, body) in Clauses) {
        var block = GetNextBlock(condition, body);
        statement.AppendLine(block);
      }

      if (ElseClause is not null) {
        statement.Append("else ");
        statement.Append(ElseClause.ToStringFromAst());
      }

      return statement.ToString().TrimEnd();
    }

    private string GetNextBlock(PipelineBaseAst condition, StatementBlockAst body) {
      var keyword = GetContextAwareKeyword();
      var conditionString = condition.ToStringFromAst();
      var bodyString = body.ToStringFromAst();

      return $"{keyword} ({conditionString}) {bodyString}";
    }

    private string GetContextAwareKeyword() {
      if (!_isFirstIf) {
        return "elseif";
      }

      _isFirstIf = false;
      return "if";
    }
  }
}