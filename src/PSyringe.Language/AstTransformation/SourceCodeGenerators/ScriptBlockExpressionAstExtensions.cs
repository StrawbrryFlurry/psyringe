using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class ScriptBlockExpressionAstExtensions {
  public static string ToStringFromAst(this ScriptBlockExpressionAst ast) {
    var sb = ast.ScriptBlock.ToStringFromAst();
    return sb;
  }
}