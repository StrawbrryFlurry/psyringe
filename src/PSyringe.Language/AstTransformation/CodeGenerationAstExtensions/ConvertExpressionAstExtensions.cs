using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class ConvertExpressionAstExtensions {
  public static string ToStringFromAst(this ConvertExpressionAst ast) {
    var typeConstraint = ast.Type.ToStringFromAst();
    var expression = ast.Child.ToStringFromAst();

    return $"{typeConstraint}{expression}";
  }
}