using System.Management.Automation.Language;
using System.Text.RegularExpressions;
using static System.Management.Automation.Language.StringConstantType;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class StringConstantExpressionAstExtensions {
  // (?<!\$\([^)]*)\"(?![^(]*\)) 
  // Matches all quotes that are not surrounded by a $() sub expression
  private static readonly Regex DoubleQuoteNotInSubExpressionPattern = new(@"(?<!\$\([^)]*)""(?![^(]*\))");

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
    // We don't want to change the quotes in a sub
    // expression because they don't need to escaped.
    value = FixStringEscapes(value, type);

    return type switch {
      BareWord => value,
      DoubleQuoted => $"\"{value}\"",
      DoubleQuotedHereString => $"@\"{NewLine}{value}{NewLine}\"@",
      SingleQuoted => $"'{value}'",
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
  private static string FixStringEscapes(string str, StringConstantType type) {
    if (type is DoubleQuoted or DoubleQuotedHereString) {
      // Because expandable strings can have sub expressions
      // we need to make sure that we don't duplicate quotes
      // in those sub expressions. The expressions themselves
      // are evaluated at runtime so all we get from the ast
      // is the raw string value e.g. "Something$(Foo)Else" 
      str = DoubleQuoteNotInSubExpressionPattern.Replace(str, "\"\"");
    }
    else if (type is SingleQuoted or SingleQuotedHereString) {
      str = str.Replace("'", "''");
    }

    return str;
  }
}