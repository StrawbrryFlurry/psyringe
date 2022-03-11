using System.Management.Automation.Language;

namespace PSyringe.Language.Extensions;

public static class AttributeAstExtension {
  public static bool IsAssignableToType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    var attributeType = ast.GetReflectionAttributeType();

    return attributeType?.IsAssignableTo(type) ?? false;
  }

  public static bool IsOfExactType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    var attributeType = ast.GetReflectionAttributeType();

    return attributeType == type;
  }

  public static Type GetReflectionAttributeType(this AttributeBaseAst ast) {
    return ast.TypeName.GetReflectionType();
  }

  public static bool ContainsAttributeAssignableToType<T>(this IEnumerable<AttributeBaseAst> attributes) {
    return attributes.Any(IsAssignableToType<T>);
  }
}