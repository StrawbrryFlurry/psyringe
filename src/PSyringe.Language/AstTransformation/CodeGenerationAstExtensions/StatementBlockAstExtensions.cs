using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class StatementBlockAstExtensions {
  public static string ToStringFromAst(this StatementBlockAst ast, bool includeBrackets = true) {
    var statements = ast.Statements?.ToStringFromAstJoinBy($";{NewLine}");
    var traps = ast.Traps?.ToStringFromAstJoinBy($"{NewLine}{NewLine}");
    var block = new StringBuilder();


    if (includeBrackets) {
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

    if (includeBrackets) {
      block.Append('}');
    }

    return block.ToString();
  }
}