using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class UsingStatementAstExtensions {
  public static string ToStringFromAst(this UsingStatementAst ast) {
    // The casing doesn't matter - Just for formatting
    var usingStatementKind = ast.UsingStatementKind.ToString().ToLower();
    var name = ast.Name.ToStringFromAst();

    return $"using {usingStatementKind} {name};";
  }
}