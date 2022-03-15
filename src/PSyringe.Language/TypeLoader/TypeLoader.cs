using System.Reflection;
using PSyringe.Language.TypeLoader.Extensions;
using PSyringe.Language.TypeLoader.Parameters;

namespace PSyringe.Language.TypeLoader;

public static class TypeLoader {
  public static object CreateInstanceOfType(Type type, ParameterCollection parameters) {
    var ctor = GetCtorOverloadForParameters(type, parameters, out var arguments);

    if (ctor is null) {
      throw new TypeLoadException($"No constructor overload found for type {type.Name}");
    }

    return ctor.Invoke(arguments);
  }

  public static ConstructorInfo? GetCtorOverloadForParameters(
    Type type,
    ParameterCollection parameterCollection,
    out object[]? constructorArguments
  ) {
    constructorArguments = null;
    var constructors = type.GetConstructors();

    foreach (var ctor in constructors) {
      var parameterInfos = ctor.GetParameters();

      if (!parameterInfos.HasSameParameterCount(parameterCollection.GetParameterCount())) {
        continue;
      }

      if (!parameterCollection.TryGetMethodOverloadArguments(parameterInfos, out var arguments)) {
        continue;
      }

      constructorArguments = arguments;
      return ctor;
    }

    return null;
  }
}