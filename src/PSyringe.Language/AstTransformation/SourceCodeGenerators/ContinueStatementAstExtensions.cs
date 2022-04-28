using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class ContinueStatementAstExtensions {
  public static string ToStringFromAst(this ContinueStatementAst ast) {
    if (ast.Label is null) {
      return "continue;";
    }

    var label = ast.Label.ToStringFromAst();
    return $"continue {label};";
  }
}