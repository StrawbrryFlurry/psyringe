using System.Management.Automation.Language;
using System.Text;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class SwitchStatementAstExtensions {
  public static string ToStringFromAst(this SwitchStatementAst ast) {
    var condition = ast.Condition.ToStringFromAst();
    var cases = ast.Clauses;
    var defaultClause = ast.Default?.ToStringFromAst();
    var flags = ast.Flags.GetAllSetEnumFlags(SwitchFlags.None);
    var label = ast.GetLabel();

    var switchStatement = new StringBuilder();

    switchStatement.Append(label);
    switchStatement.Append("switch ");
    // <switch> -Flag1 -Flag2 <condition>
    var flagSb = flags.Aggregate(new StringBuilder(), (sb, element) => {
      var flagString = $"-{element} ";
      sb.Append(flagString);
      return sb;
    });

    switchStatement.Append(flagSb);

    switchStatement.Append($"({condition})");
    switchStatement.AppendLine(" {");

    foreach (var (caseAst, blockAst) in cases) {
      var @case = caseAst.ToStringFromAst();
      var block = blockAst.ToStringFromAst();

      switchStatement.Append(@case);
      switchStatement.Append(' ');
      switchStatement.AppendLine(block);
    }

    if (defaultClause is not null) {
      switchStatement.Append("default ");
      switchStatement.AppendLine(defaultClause);
    }

    switchStatement.Append('}');

    return switchStatement.ToString();
  }
}