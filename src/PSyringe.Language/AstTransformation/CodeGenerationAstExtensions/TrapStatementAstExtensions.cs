using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class TrapStatementAstExtensions {
  public static string ToStringFromAst(this TrapStatementAst ast) {
    var attribute = ast.TrapType?.ToStringFromAst() ?? "";
    var body = ast.Body.ToStringFromAst();

    return $"trap {attribute}{body}";
  }
}