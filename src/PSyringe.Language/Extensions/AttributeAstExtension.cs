using System.Management.Automation.Language;
using System.Reflection;
using PSyringe.Common.Language.Attributes;

namespace PSyringe.Language.Extensions;

public static class AttributeAstExtension {
  public static bool IsAssignableToType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    var attributeType = ast.GetAttributeType();

    return attributeType?.IsAssignableTo(type) ?? false;
  }

  public static bool IsOfExactType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    return IsOfExactType(ast, type);
  }

  public static bool IsOfExactType(this AttributeBaseAst ast, Type type) {
    var attributeType = ast.GetAttributeType();
    return attributeType == type;
  }

  public static Type? GetAttributeType(this AttributeBaseAst ast) {
    return ast.TypeName.GetReflectionType();
  }

  public static bool CanBeUsedForType(this AttributeBaseAst ast, PSAttributeTargets target) {
    var attributeType = ast.GetAttributeType();
    var psAttributeUsage = attributeType?.GetCustomAttribute<PSAttributeUsageAttribute>();

    return psAttributeUsage is not null && psAttributeUsage.Target.HasFlag(target);
  }

  public static bool IsAttributeOfExactType(this AttributedExpressionAst ast, Type type) {
    return IsOfExactType(ast.Attribute, type);
  }

  /// <summary>
  ///   Returns the child of a nested attributed expression e.g.
  ///   [Inject()][LogExpression()]"SomeExpression" if that child
  ///   is assignable to type T
  /// </summary>
  /// <param name="ast"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public static T? GetNestedChildAssignableToType<T>(this AttributedExpressionAst? ast) where T : Ast {
    Ast? currentAst = ast;

    while (currentAst is not null) {
      switch (currentAst) {
        case T targetAst:
          return targetAst;
        case AttributedExpressionAst attributedExpressionAst:
          currentAst = attributedExpressionAst.Child;
          break;
        default:
          return null;
      }
    }

    return null;
  }

  /// <summary>
  ///   Returns the direct parent of a nested attributed expression e.g.
  ///   "ParentAst.Child" { [Inject()][LogExpression()]"SomeExpression" }
  ///   if that parent is assignable to type T
  /// </summary>
  /// <param name="ast"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public static T? GetDirectParentAssignableToType<T>(this AttributedExpressionAst? ast) where T : Ast {
    Ast? currentAst = ast;

    while (currentAst is not null) {
      switch (currentAst) {
        case T targetAst:
          return targetAst;
        case AttributedExpressionAst attributedExpressionAst:
          currentAst = attributedExpressionAst.Parent;
          break;
        default:
          return null;
      }
    }

    return null;
  }

  /// <summary>
  ///   Traverses the ast's children's and parent's AttributedExpressionAsts
  ///   checking if it's type is assignable to the specified type. Also
  ///   accepts matches for a direct parent, child or itself of an AttributedExpressionAst.
  ///   Used to find related asts in nested expressions like this:
  ///   <code>
  /// [Inject([ILogger])][LogExpression()][ILogger]$Logger
  /// </code>
  /// </summary>
  public static T? GetAstInTreeAssignableToType<T>(this AttributedExpressionAst? ast) where T : Ast {
    var childAssignableToType = ast.GetNestedChildAssignableToType<T>();
    return childAssignableToType ?? ast.GetDirectParentAssignableToType<T>();
  }
}