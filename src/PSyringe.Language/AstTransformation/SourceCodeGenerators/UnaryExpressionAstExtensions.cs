using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class UnaryExpressionAstExtensions {
  public static string ToStringFromAst(this UnaryExpressionAst ast) {
    var token = ast.TokenKind;
    var shouldBeAfterExpression = token.HasTrait(TokenFlags.PrefixOrPostfixOperator);
    var childExpression = ast.Child.ToStringFromAst();

    if (shouldBeAfterExpression) {
      return $"{childExpression}{token.Text()}";
    }

    return $"{token.Text()}{childExpression}";
  }
}