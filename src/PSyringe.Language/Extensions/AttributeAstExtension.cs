using System.Management.Automation.Language;

namespace PSyringe.Language.Extensions;

internal static class AttributeAstExtension {
  internal static bool IsAssignableToType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    var attributeType = ast.GetReflectionAttributeType();

    return attributeType?.IsAssignableTo(type) ?? false;
  }

  internal static bool IsOfExactType<T>(this AttributeBaseAst ast) {
    var type = typeof(T);
    var attributeType = ast.GetReflectionAttributeType();

    return attributeType == type;
  }

  internal static bool HasAttributeAssignableToType<T>(this IEnumerable<AttributeBaseAst> attributes) {
    return attributes.Any(IsAssignableToType<T>);
  }

  internal static bool HasAttributeOfType<T>(this IEnumerable<AttributeBaseAst> attributes) {
    return attributes.Any(IsOfExactType<T>);
  }

  internal static Type GetReflectionAttributeType(this AttributeBaseAst ast) {
    return ast.TypeName.GetReflectionType();
  }
}