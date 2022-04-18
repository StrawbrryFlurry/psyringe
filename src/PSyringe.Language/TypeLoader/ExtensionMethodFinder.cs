using System.Reflection;
using System.Runtime.CompilerServices;

namespace PSyringe.Language.TypeLoader;

/// <summary>
///   Utility extension method for `object`. Can be used to
///   invoke extension methods on a concrete implementation of
///   type of the object provided in this namespace.
/// </summary>
public static class ExtensionMethodFinder {
  // Caching reflection info
  internal static readonly Type TypeInAssembly = typeof(ExtensionMethodFinder);
  internal static readonly Type ExtensionAttributeType = typeof(ExtensionAttribute);

  internal static readonly IEnumerable<MethodInfo> ExtensionMethods =
    GetAllExtensionMethodsInAssembly();

  internal static readonly BindingFlags ExtensionMethodBindingFlags =
    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

  internal static T InvokeExtensionMethodInAssemblyForConcreteType<T>(this object type, string name,
    params object[] args) {
    var concreteExtensionMethod = GetExtensionMethodOverloadForConcreteType(type, name);
    var parameterSet = args.ToList();
    // The first parameter of an extension method is always the instance type
    parameterSet.Insert(0, type);
    return (T) concreteExtensionMethod.Invoke(null, parameterSet.ToArray())!;
  }

  internal static MethodInfo GetExtensionMethodOverloadForConcreteType(object obj, string name) {
    var objectType = obj.GetType();
    var extensionMethod = ExtensionMethods
                          .Where(m => m.Name == name)
                          .SingleOrDefault(
                            m => DoesMethodTakeTypeAsFirstParameter(m, objectType)
                          );

    if (extensionMethod is null) {
      throw new Exception($"Could not find extension method for type {objectType.Name}");
    }

    return extensionMethod;
  }

  private static bool DoesMethodTakeTypeAsFirstParameter(MethodInfo method, Type type) {
    var parameter = method.GetParameters().First();
    var doesParameterMatch = parameter.ParameterType == type;

    return doesParameterMatch;
  }

  private static IEnumerable<MethodInfo> GetAllExtensionMethodsInAssembly() {
    var m = TypeInAssembly.Assembly.GetTypes()
                          .Select(c => c.GetMethods(ExtensionMethodBindingFlags)
                                        .Where(IsMethodExtensionMethod)
                          ).SelectMany(m => m);
    return m;
  }

  private static bool IsMethodExtensionMethod(MethodInfo method) {
    return method.IsDefined(ExtensionAttributeType);
  }
}