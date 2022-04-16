using System.Collections.Generic;
using System.Management.Automation.Language;

namespace PSyringe.Language.Test.AstTransformation;

public static class TransformationConstants {
  public const string VariableName = "Test";
  public const string ScopedVariableName = $"script:{VariableName}";

  public const string VariableString = $"${VariableName}";
  public const string ScopedVariableString = $"${ScopedVariableName}";
  public const string SplattedVariableString = $"@{VariableName}";
  public const string SplattedScopedVariableString = $"@{ScopedVariableName}";

  public const string ConstantString = "Test";

  public const string True = "true";
  public const string False = "false";

  public const int One = 1;

  public const string TernaryExpression = $"${False} ? ${True} : ${False}";
  public const string TrueVariable = $"${True}";

  public static string BinaryExpression = $"{One} {TokenKind.Plus.Text()} {One}";
  public static string ConstantStringExpression = DoubleQuote(ConstantString);

  public static string ArrayExpressionOneElement = $"@({TrueVariable})";
  public static string ArrayExpressionTwoElements = $"@({TrueVariable}, {One})";

  public static List<ExpressionAst> ArrayOneElement => new() {
    TrueExpression
  };

  public static List<ExpressionAst> ArrayTwoElements => new() {
    TrueExpression,
    NumberOneExpression
  };

  public static ConstantExpressionAst NumberOneExpression => MakeConstant(One);

  public static VariableExpressionAst TrueExpression => MakeVariable(True);
  public static VariableExpressionAst FalseExpression => MakeVariable(False);

  public static IScriptExtent EmptyExtent => new ScriptExtent(null, null);

  public static VariableExpressionAst VariableExpression(string variable) {
    return MakeVariable(variable);
  }

  public static string UsingExpression(string variable) {
    return $"$using:{variable}";
  }

  public static VariableExpressionAst MakeVariable(string name, bool isSplatted = false) {
    return new VariableExpressionAst(EmptyExtent, name, isSplatted);
  }

  public static ConstantExpressionAst MakeConstant(object value) {
    return new ConstantExpressionAst(EmptyExtent, value);
  }

  public static string DoubleQuote(object value) {
    return $"\"{value}\"";
  }

  public static string SingleQuote(object value) {
    return $"'{value}'";
  }
}