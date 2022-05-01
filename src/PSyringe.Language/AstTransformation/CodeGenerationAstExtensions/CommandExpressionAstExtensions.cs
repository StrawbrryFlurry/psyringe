using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class CommandExpressionAstExtensions {
  public static string ToStringFromAst(this CommandExpressionAst ast) {
    var command = ast.Expression.ToStringFromAst();
    var redirections = ast.Redirections.ToStringFromAstJoinBy(" ");

    if (redirections is null) {
      return command;
    }

    return $"{command} {redirections}";
  }
}