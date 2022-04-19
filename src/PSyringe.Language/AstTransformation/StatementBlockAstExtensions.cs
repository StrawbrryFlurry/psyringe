using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.Compiler.CompilerScriptText;

namespace PSyringe.Language.AstTransformation;

public static class StatementBlockAstExtensions {
  public static string ToStringFromAst(this StatementBlockAst ast) {
    var statements = ast.Statements?.ToStringFromAstJoinBy($";{NewLine}");
    var traps = ast.Traps?.ToStringFromAstJoinBy($"{NewLine}{NewLine}");
    var block = new StringBuilder();

    var shouldAddBrackets = !ast.Parent.AreStatementBracketsIncluded();

    if (shouldAddBrackets) {
      block.AppendLine("{");
    }

    if (statements is not null) {
      if (!statements.EndsWith(';')) {
        statements += ';';
      }

      block.AppendLine(statements);
    }

    if (traps is not null) {
      block.AppendLine(traps);
    }

    if (shouldAddBrackets) {
      block.Append('}');
    }

    return block.ToString();
  }
}