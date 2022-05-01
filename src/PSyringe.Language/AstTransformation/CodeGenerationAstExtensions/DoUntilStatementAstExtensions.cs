using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class DoUntilStatementAstExtensions {
  public static string ToStringFromAst(this DoUntilStatementAst ast) {
    var condition = ast.Condition.ToStringFromAst();
    var body = ast.Body.ToStringFromAst();
    var label = ast.GetLabel();

    return $"{label}do {body} until ({condition});";
  }
}