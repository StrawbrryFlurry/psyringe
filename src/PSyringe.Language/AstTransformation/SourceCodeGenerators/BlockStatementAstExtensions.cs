using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class BlockStatementAstExtensions {
  public static string ToStringFromAst(this BlockStatementAst ast) {
    // We get the text of the token kind rather than the `Token`
    // instance because the `Token` instance points to the script extent.
    var keyword = ast.Kind.Kind.Text().ToLower();
    var body = ast.Body.ToStringFromAst();

    return $"{keyword} {body}";
  }
}