using System.Management.Automation.Language;
using static System.Management.Automation.Language.StringConstantType;
using static PSyringe.Language.Compiler.CompilerScriptText;

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
    FixStringEscapes(ref value, type);

    return type switch {
      BareWord => value,
      DoubleQuoted => DoubleQuote(value),
      DoubleQuotedHereString => $"@\"{NewLine}{value}{NewLine}\"@",
      SingleQuoted => SingleQuote(value),
      SingleQuotedHereString => $"@'{NewLine}{value}{NewLine}'@",
      _ => ""
    };
  }

  /// <summary>
  ///   The parser will remove string escapes such as
  ///   <code>
  ///  'It''s me!'
  ///  </code>
  ///   So we need to add them back.
  /// </summary>
  private static void FixStringEscapes(ref string str, StringConstantType type) {
    if (type is DoubleQuoted or DoubleQuotedHereString) {
      str = str.Replace("\"", "\"\"");
    }
    else if (type is SingleQuoted or SingleQuotedHereString) {
      str = str.Replace("'", "''");
    }
  }

  private static string SingleQuote(object value) {
    return $"'{value}'";
  }

  private static string DoubleQuote(object value) {
    return $"\"{value}\"";
  }
}