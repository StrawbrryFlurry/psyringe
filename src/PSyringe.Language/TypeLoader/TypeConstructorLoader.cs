using System.Management.Automation.Internal;
using System.Reflection;
using PSyringe.Language.TypeLoader.Extensions;
using PSyringe.Language.TypeLoader.Parameters;

namespace PSyringe.Language.TypeLoader; 

public static class TypeConstructorLoader {
  public static object CreateInstanceOfType(Type type, ParameterCollection parameters) {
    var constructor = GetCtorOverloadForParameters(type, parameters, out var arguments);

    if(constructor is null) {
      throw new Exception($"No constructor found for type {type.Name} with parameters {string.Join(", ", arguments)}");
    }

    return constructor.Invoke(new [] {arguments[0], null});
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

      if (MatchesCtorParameterOverload(parameterInfos, parameterCollection.Clone(), out var arguments)) {
        constructorArguments = arguments;
        return ctor;
      }
    }

    return null;
  }

  // TODO: Move to ParameterCollection
  private static bool MatchesCtorParameterOverload(
    ParameterInfo[] parameters,
    ParameterCollection parameterCollection,
    out object[]? constructorArguments
  ) {
    constructorArguments = null;
    var positionsCoveredByNamedParameters = new List<int>();

    foreach (var parameter in parameterCollection.NamedParameters) {
      var parameterInfo = parameters.GetEquivalentParameter(parameter);

      if (parameterInfo == null) {
        return false;
      }

      positionsCoveredByNamedParameters.Add(parameterInfo.Position);
      parameterCollection.AddConstructorParameter(new PositionalParameter {
        Position = parameterInfo.Position, 
        Type = parameterInfo.ParameterType,
        Value = parameter.Value
      });
    }

    var positionalParameters = parameterCollection
      .GetPositionalParametersWithoutNamedParameterIndexes(positionsCoveredByNamedParameters);
    
    foreach (var parameter in positionalParameters) {
      var parameterInfo = parameters.GetEquivalentParameter(parameter);
      
      if (parameterInfo == null) {
        return false;
      }
      
      parameterCollection.AddConstructorParameter(parameter);
    }

    // Validation
    constructorArguments = parameterCollection.GetConstructorParameters();

    for(var i = 0; i < parameters.Length; i++) {
      var parameter = parameters[i];
      var ctorParameter = constructorArguments.ElementAtOrDefault(i);
      
      if (parameter.IsOfSameTypeAs(ctorParameter)) {
        continue;
      }

      if (parameter.IsOptional) {
        continue;
      }

      return false;
    }

    return true;
  }
}

