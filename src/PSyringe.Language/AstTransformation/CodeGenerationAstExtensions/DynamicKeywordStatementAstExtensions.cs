using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class DynamicKeywordStatementAstExtensions {
  public static string ToStringFromAst(this DynamicKeywordStatementAst ast) {
    throw new NotImplementedException("DSC ASTs are not yet supported");
  }
}