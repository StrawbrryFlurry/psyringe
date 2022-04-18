using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class ForEachStatementAstExtensions {
  public static string ToStringFromAst(this ForEachStatementAst ast) {
    var variable = ast.Variable.ToStringFromAst();
    var expression = ast.Condition.ToStringFromAst();
    var body = ast.Body.ToStringFromAst();

    var label = ast.Label;
    var labelString = label is not null ? $":{label} " : "";

    return $"{labelString}foreach ({variable} in {expression}) {body}";
  }
}