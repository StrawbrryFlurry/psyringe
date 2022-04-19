using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class CatchClauseAstExtensions {
  public static string ToStringFromAst(this CatchClauseAst ast) {
    var typeConstraints = ast.CatchTypes.ToStringFromAstJoinBy("");
    var body = ast.Body.ToStringFromAst();

    return $"catch {typeConstraints}{body}";
  }
}