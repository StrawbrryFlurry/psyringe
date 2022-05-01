using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class ThrowStatementAstExtensions {
  public static string ToStringFromAst(this ThrowStatementAst ast) {
    var throwExpression = ast.Pipeline?.ToStringFromAst();
    return $"throw {throwExpression};";
  }
}