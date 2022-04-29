using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class BreakStatementAstExtensions {
  public static string ToStringFromAst(this BreakStatementAst ast) {
    if (ast.Label is null) {
      return "break;";
    }

    var label = ast.Label.ToStringFromAst();
    return $"break {label};";
  }
}