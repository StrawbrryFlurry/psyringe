using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class AttributedExpressionAstExtensions {
  public static string ToStringFromAst(this AttributedExpressionAst ast) {
    var expression = ast.Child.ToStringFromAst();
    var attribute = ast.Attribute.ToStringFromAst();

    return $"{attribute}{expression}";
  }
}