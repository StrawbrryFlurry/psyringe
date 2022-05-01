using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class MergingRedirectionAstExtensions {
  public static string ToStringFromAst(this MergingRedirectionAst ast) {
    var fromStream = (int) ast.FromStream;
    var toStream = (int) ast.ToStream;

    // 2&>1
    return $"{fromStream}>&{toStream}";
  }
}