using System.Management.Automation.Language;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PSyringe.Language.AstTransformation;

/// <summary>
///   Extension methods for base AST types. Uses reflection
///   to get the extension method for the concrete implementation
///   of the runtime AST type.
/// </summary>
public static class AstBaseClassExtensions {
  // Caching reflection info
  internal static readonly Type NamespaceType = typeof(AstBaseClassExtensions);
  internal static readonly Type ExtensionAttributeType = typeof(ExtensionAttribute);

  internal static readonly IEnumerable<MethodInfo> StringGeneratorExtensionMethods =
    GetAllStringGeneratorMethodsInNamespace();

  internal static readonly BindingFlags ExtensionMethodBindingFlags =
    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

  public static string GetAstAsString(this Ast ast) {
    return InvokeExtensionMethodForDerivedType(ast);
  }

  public static string GetAstAsString(this ExpressionAst ast) {
    return InvokeExtensionMethodForDerivedType(ast);
  }

  internal static string InvokeExtensionMethodForDerivedType(Ast ast) {
    var derivedTypeExtensionMethod = GetExtensionMethodOverloadDerivedAstType(ast);
    return (string) derivedTypeExtensionMethod.Invoke(null, new object[] {ast})!;
  }

  internal static MethodInfo GetExtensionMethodOverloadDerivedAstType(Ast ast) {
    var astType = ast.GetType();
    var extensionMethod = StringGeneratorExtensionMethods
      .SingleOrDefault(m => DoesMethodTakeTypeAsFirstParameter(m, astType));

    if (extensionMethod is null) {
      throw new Exception($"Could not find string generator extension method for type {astType.Name}");
    }

    return extensionMethod;
  }

  private static bool DoesMethodTakeTypeAsFirstParameter(MethodInfo method, Type type) {
    var parameter = method.GetParameters().First();
    var doesParameterMatch = parameter.ParameterType == type;

    return doesParameterMatch;
  }

  private static IEnumerable<MethodInfo> GetAllStringGeneratorMethodsInNamespace() {
    return NamespaceType.Assembly.GetTypes()
                        .Where(IsClassInNamespace)
                        .Select(c => c.GetMethods(ExtensionMethodBindingFlags)
                                      .Where(IsMethodExtensionMethod)
                                      .Where(IsFirstMethodParameterAssignableTo<Ast>)
                        ).SelectMany(m => m);
  }

  private static bool IsFirstMethodParameterAssignableTo<T>(this MethodInfo method) {
    return method.GetParameters().First().ParameterType.IsAssignableTo(typeof(T));
  }

  private static bool IsClassInNamespace(Type type) {
    return type.IsClass && type.Namespace == NamespaceType.Namespace;
  }

  private static bool IsMethodExtensionMethod(MethodInfo method) {
    return method.IsDefined(ExtensionAttributeType);
  }
}