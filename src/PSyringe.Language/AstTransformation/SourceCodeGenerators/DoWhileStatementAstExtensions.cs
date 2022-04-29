using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class DoWhileStatementAstExtensions {
  public static string ToStringFromAst(this DoWhileStatementAst ast) {
    var condition = ast.Condition.ToStringFromAst();
    var body = ast.Body.ToStringFromAst();
    var label = ast.GetLabel();

    return $"{label}do {body} while ({condition});";
  }
}