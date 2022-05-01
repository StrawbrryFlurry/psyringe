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

  public static bool ReplaceChildCore(this CommandExpressionAst ast, Ast child, Ast replacement) {
    if (ast.Expression.Is(child)) {
      replacement.SetParent(ast);
      ast.SetPrivateProperty(nameof(ast.Expression), replacement);
      return true;
    }

    return ast.Expression.ReplaceChild(child, replacement);
  }
}