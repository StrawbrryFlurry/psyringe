using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class ConfigurationDefinitionAstExtensions {
  public static string ToStringFromAst(this ConfigurationDefinitionAst ast) {
    throw new NotImplementedException("DSC ASTs are not supported yet");
  }
}