using System.Management.Automation.Language;

namespace PSyringe.Language.Test.AstTransformation;

public static class TransformationConstants {
  public const string VariableName = "Test";
  public const string ScopedVariableName = "script:Test";

  public const string VariableString = $"${VariableName}";
  public const string ScopedVariableString = $"${ScopedVariableName}";
  public const string SplattedVariableString = $"@{VariableName}";
  public const string SplattedScopedVariableString = $"@{ScopedVariableName}";

  public const string True = "true";
  public const string False = "false";

  public const string TernaryExpression = $"${False} ? ${True} : ${False}";

  public static VariableExpressionAst TrueExpression => MakeVariable(True);
  public static VariableExpressionAst FalseExpression => MakeVariable(False);

  public static IScriptExtent EmptyExtent => new ScriptExtent(null, null);

  public static VariableExpressionAst MakeVariable(string name, bool isSplatted = false) {
    return new VariableExpressionAst(EmptyExtent, name, isSplatted);
  }
}