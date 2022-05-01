using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class AttributedExpressionAstExtensions {
  public static string ToStringFromAst(this AttributedExpressionAst ast) {
    var expression = ast.Child.ToStringFromAst();
    var attribute = ast.Attribute.ToStringFromAst();

    return $"{attribute}{expression}";
  }

  private static bool ReplaceChildCore(this AttributedExpressionAst ast, Ast child, Ast replacement) {
    var attribute = ast.Attribute;
    var expression = ast.Child;

    if (attribute.Is(child)) {
      replacement.SetParent(ast);
      ast.SetPrivateProperty(nameof(ast.Attribute), replacement);
      return true;
    }

    if (expression.Is(child)) {
      replacement.SetParent(ast);
      ast.SetPrivateProperty(nameof(ast.Child), replacement);
      return true;
    }

    return attribute.ReplaceChild(child, replacement) || expression.ReplaceChild(child, replacement);
  }
}