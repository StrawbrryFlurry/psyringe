using System.Management.Automation.Language;
using PSyringe.Language.TypeLoader;

namespace PSyringe.Language.AstTransformation;

public static class TypeExpressionAstExtensions {
  public static string ToStringFromAst(this TypeExpressionAst ast) {
    return ast.TypeName.InvokeExtensionMethodInAssemblyForConcreteType<string>(nameof(ToStringFromAst));
  }

  public static string ToStringFromAst(this ITypeName type) {
    return type.InvokeExtensionMethodInAssemblyForConcreteType<string>(nameof(ToStringFromAst));
  }

  public static string ToStringFromAst(this TypeName type) {
    return FullTypeNameAsString(type);
  }

  public static string ToStringFromAst(this ReflectionTypeName type) {
    return FullTypeNameAsString(type);
  }

  /// <summary>
  ///   The FullName of the type already includes the generics
  ///   e.g. System.Collections.Generic.List would be
  ///   System.Collections.Generic.List[System.String]
  /// </summary>
  public static string ToStringFromAst(this GenericTypeName type) {
    return FullTypeNameAsString(type);
  }

  private static string FullTypeNameAsString(this ITypeName type) {
    return $"[{type.FullName}]";
  }

  public static string ToStringFromAst(this ArrayTypeName type) {
    var brackets = string.Concat(Enumerable.Repeat("[]", type.Rank));
    var typeName = type.ElementType.ToStringFromAst();
    // The GetString method will return the type name with the brackets
    // which we need to remove to add the array brackets.
    var typeNameWithoutEndBrackets = typeName.TrimEnd(']');

    return $"{typeNameWithoutEndBrackets}{brackets}]";
  }
}