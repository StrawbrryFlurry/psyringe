using System.Management.Automation.Language;

namespace PSyringe.Language.AstExtensions;

public static class TernaryExpressionAstExtensions {
  public static string GetAstAsString(this TernaryExpressionAst ast) {
    var conditionExpression = ast.Condition.GetAstAsString();
    var ifTrue = ast.IfTrue.GetAstAsString();
    var ifFalse = ast.IfFalse.GetAstAsString();

    return $"{conditionExpression} ? {ifTrue} : {ifFalse}";
  }
}