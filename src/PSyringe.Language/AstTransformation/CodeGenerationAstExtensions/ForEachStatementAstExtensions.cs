using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class ForEachStatementAstExtensions {
  public static string ToStringFromAst(this ForEachStatementAst ast) {
    var label = ast.GetLabel();
    var variable = ast.Variable.ToStringFromAst();
    var expression = ast.Condition.ToStringFromAst();
    var body = ast.Body.ToStringFromAst();

    return $"{label}foreach ({variable} in {expression}) {body}";
  }
}