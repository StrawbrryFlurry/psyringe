using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class ScriptBlockExpressionAstExtensions {
  public static string ToStringFromAst(this ScriptBlockExpressionAst ast) {
    var sb = ast.ScriptBlock.ToStringFromAst();
    return sb;
  }
}