using System.Management.Automation.Language;
using PSyringe.Language.Compiler;

namespace PSyringe.Language.AstTransformation;

public static class InvokeMemberExpressionAstExtensions {
  public static string ToStringFromAst(this InvokeMemberExpressionAst ast) {
    var innerMemberExpression = new MemberExpressionAst(
      ast.Extent,
      ast.Expression.CopyAs<ExpressionAst>(),
      ast.Member.CopyAs<CommandElementAst>(),
      ast.Static,
      ast.NullConditional,
      ast.GenericTypeArguments
    );

    var memberExpression = innerMemberExpression.ToStringFromAst();
    var args = ast.Arguments?.ToStringFromAstJoinBy(", ");

    return $"{memberExpression}({args})";
  }
}