using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class TernaryExpressionAstExtensions {
  public static string ToStringFromAst(this TernaryExpressionAst ast) {
    var conditionExpression = ast.Condition.ToStringFromAst();
    var ifTrue = ast.IfTrue.ToStringFromAst();
    var ifFalse = ast.IfFalse.ToStringFromAst();

    return $"{conditionExpression} ? {ifTrue} : {ifFalse}";
  }
}