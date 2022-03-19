using System.Management.Automation.Language;

namespace PSyringe.Language.Extensions;

public static class AttributeAstExtension {
  public static bool IsAssignableToType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    var attributeType = ast.GetAttributeType();

    return attributeType?.IsAssignableTo(type) ?? false;
  }

  public static bool IsOfExactType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    var attributeType = ast.GetAttributeType();

    return attributeType == type;
  }

  public static Type GetAttributeType(this AttributeBaseAst ast) {
    return ast.TypeName.GetReflectionType();
  }

  public static bool ContainsAttributeAssignableToType<T>(this IEnumerable<AttributeBaseAst> attributes) {
    return attributes.Any(IsAssignableToType<T>);
  }
  
  /// <summary>
  /// Traverses the ast's children's and parent's AttributedExpressionAsts
  /// checking if it's type is assignable to the specified type. Also
  /// accepts matches for a direct parent, child or itself of an AttributedExpressionAst.
  ///
  /// Used to find related asts in nested expressions like this:
  /// <code>
  /// [Inject([ILogger])][LogExpression()][ILogger]$Logger
  /// </code>
  /// </summary>
  public static T? GetAstInTreeAssignableToType<T>(this AttributedExpressionAst? ast) where T : Ast {
    Ast? currentAst = ast;

    while (currentAst is not null) {
      switch (currentAst) {
        case T targetAst:
          return targetAst;
        case AttributedExpressionAst attributedExpressionAst:
          currentAst = attributedExpressionAst.Child;
          break;
        default: currentAst = null;
          break;
      }
    }
    
    currentAst = ast;
    while (currentAst is not null) {
      switch (currentAst) {
        case T targetAst:
          return targetAst;
        case AttributedExpressionAst attributedExpressionAst:
          currentAst = attributedExpressionAst.Parent;
          break;
        default:
          currentAst = null;
          break;
      }
    }

    return null;
  }
}