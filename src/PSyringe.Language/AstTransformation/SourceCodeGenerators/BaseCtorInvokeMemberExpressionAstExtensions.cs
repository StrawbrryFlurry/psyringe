using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class BaseCtorInvokeMemberExpressionAstExtensions {
  public static string ToStringFromAst(this BaseCtorInvokeMemberExpressionAst ast) {
    var args = ast.Arguments?.ToStringFromAstJoinBy(", ") ?? "";

    return $"base({args})";
  }
}