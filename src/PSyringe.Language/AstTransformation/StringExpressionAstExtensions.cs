using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class StringConstantExpressionAstExtensions {
  public static string ToStringFromAst(this StringConstantExpressionAst ast) {
    return QuoteStringExpression(ast.Value, ast.StringConstantType);
  }

  /// <summary>
  ///   The expression is evaluated at runtime which means we
  ///   can process this the same as a string constant.
  /// </summary>
  public static string ToStringFromAst(this ExpandableStringExpressionAst ast) {
    return QuoteStringExpression(ast.Value, ast.StringConstantType);
  }

  private static string QuoteStringExpression(string value, StringConstantType type) {
    return type switch {
      StringConstantType.BareWord => value,
      StringConstantType.DoubleQuoted => DoubleQuote(value),
      StringConstantType.DoubleQuotedHereString => $"@{DoubleQuote(value)}@",
      StringConstantType.SingleQuoted => SingleQuote(value),
      StringConstantType.SingleQuotedHereString => $"@{SingleQuote(value)}@",
      _ => ""
    };
  }

  private static string SingleQuote(object value) {
    return $"'{value}'";
  }

  private static string DoubleQuote(object value) {
    return $"\"{value}\"";
  }
}