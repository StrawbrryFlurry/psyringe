using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class FunctionDefinitionAstExtensions {
  public static string ToStringFromAst(this FunctionDefinitionAst ast) {
    return default;
  }

  public static bool AreStatementBracketsIncluded(this FunctionDefinitionAst ast) {
    return true;
  }
}