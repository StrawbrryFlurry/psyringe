using System.Reflection;
using System.Runtime.CompilerServices;

namespace PSyringe.Language.TypeLoader;

/// <summary>
///   Utility extension method for `object`. Can be used to
///   invoke extension methods on a concrete implementation of
///   type of the object provided in this namespace.
/// </summary>
public class ExtensionMethodFinder {
  // Caching reflection info
  internal static readonly Type ExtensionAttributeType = typeof(ExtensionAttribute);

  internal static readonly BindingFlags ExtensionMethodBindingFlags =
    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

  internal IDictionary<Type, MethodInfo> ExtensionMethods = null!;
  internal string MethodName;

  public ExtensionMethodFinder(string methodName, Assembly? assembly = null) {
    assembly ??= GetType().Assembly;
    MethodName = methodName;
    ExtensionMethods = GetExtensionMethods(methodName, assembly);
  }

  internal void InvokeExtensionMethodInAssemblyForConcreteType(object instance, params object[] args) {
    InvokeExtensionMethodCore(instance, args);
  }

  internal T InvokeExtensionMethodInAssemblyForConcreteType<T>(object instance, params object[] args) {
    return (T) InvokeExtensionMethodCore(instance, args)!;
  }

  private object? InvokeExtensionMethodCore(object instance, IEnumerable<object> args) {
    var concreteExtensionMethod = GetExtensionMethodOverloadForConcreteType(instance);
    var parameterSet = args.ToList();
    // The first parameter of an extension method is always the instance type
    parameterSet.Insert(0, instance);
    return concreteExtensionMethod.Invoke(null, parameterSet.ToArray())!;
  }

  internal MethodInfo GetExtensionMethodOverloadForConcreteType(object obj) {
    var objectType = obj.GetType();

    if (!ExtensionMethods.TryGetValue(objectType, out var extensionMethod)) {
      throw new Exception($@"Could not find extension method ""{MethodName}"" for type ""{objectType.Name}""");
    }

    return extensionMethod;
  }

  private static IDictionary<Type, MethodInfo> GetExtensionMethods(string methodName, Assembly assembly) {
    var methods = assembly.GetTypes()
                          .Select(t => GetExtensionMethodsOfType(t)
                            .Where(m => m.Name == methodName)
                          )
                          .SelectMany(m => m)
                          .Select(method => (method.GetParameters().First().ParameterType, method));

    return methods.ToDictionary(e => e.ParameterType, e => e.method);
  }

  private static IEnumerable<MethodInfo> GetExtensionMethodsOfType(Type type) {
    return type.GetMethods(ExtensionMethodBindingFlags)
               .Where(m => m.IsDefined(ExtensionAttributeType));
  }
}