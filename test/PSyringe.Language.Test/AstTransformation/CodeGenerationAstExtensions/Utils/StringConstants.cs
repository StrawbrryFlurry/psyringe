using System.Management.Automation.Language;

namespace PSyringe.Language.Test.AstTransformation.CodeGenerationAstExtensions.Utils;

public static class StringConstants {
  public const string ConstantString = "constant";
  public const string VariableName = "Test";

  public const string True = "true";
  public const string False = "false";

  public const int Zero = 0;
  public const int One = 1;
  public const int Two = 2;
  public const int Three = 3;
  public const int Four = 4;
  public const int Five = 5;
  public const int Six = 6;
  public const int Seven = 7;
  public const int Eight = 8;
  public const int Nine = 9;

  public const string NewLine = "\r\n";
  public const TokenKind PlusPlus = TokenKind.PlusPlus;

  public static string BinaryExpression(object left, string op, object right) {
    return $"{left} {op} {right}";
  }

  public static string TernaryExpression(string condition, string ifTrue, string ifFalse) {
    return $"{condition} ? {ifTrue} : {ifFalse}";
  }

  /// <summary>
  ///   Makes a variable string
  /// </summary>
  public static string VarS(string name) {
    return $"${name}";
  }

  public static string VarS(string name, bool splatted) {
    return splatted ? $"@{name}" : $"${name}";
  }

  public static string VarS(string name, string scope) {
    return $"${scope}:{name}";
  }

  public static string VarS(string name, string scope, bool splatted) {
    return splatted ? $"@{scope}:{name}" : VarS(name, scope);
  }

  public static string AttrS<T>() {
    return $"[{typeof(T).Name}()]";
  }

  public static string TypeS<T>() {
    return $"[{typeof(T).FullName}]";
  }

  public static string UsingExpression(string variable) {
    return $"$using:{variable}";
  }

  public static string IndexExpression(string variable, object index, bool nullConditional = false) {
    return $"{variable}{(nullConditional ? "?" : "")}[{index}]";
  }

  public static string DoubleQuote(object value) {
    return $"\"{value}\"";
  }

  public static string SingleQuote(object value) {
    return $"'{value}'";
  }
}