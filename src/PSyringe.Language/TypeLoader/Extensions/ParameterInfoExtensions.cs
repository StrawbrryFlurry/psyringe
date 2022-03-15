using System.Reflection;
using PSyringe.Language.TypeLoader.Parameters;

namespace PSyringe.Language.TypeLoader.Extensions;

public static class ParameterInfoExtensions {
  public static bool IsEquivalentTo(this ParameterInfo parameter, NamedParameter namedParameter) {
    if (!namedParameter.Type.IsAssignableTo(parameter.ParameterType)) {
      return false;
    }

    // PowerShell is case-insensitive, so we need
    // to check for a case-insensitive match
    if (!parameter.Name!.Equals(namedParameter.Name, StringComparison.OrdinalIgnoreCase)) {
      return false;
    }

    return true;
  }

  public static bool IsEquivalentTo(this ParameterInfo parameter, PositionalParameter positionalParameter) {
    if (!positionalParameter.Type.IsAssignableTo(parameter.ParameterType)) {
      return false;
    }

    if (parameter.Position != positionalParameter.Position) {
      return false;
    }

    return true;
  }

  public static bool IsOfSameTypeAs(this ParameterInfo parameterInfo, object? value) {
    return value?.GetType()?.IsAssignableTo(parameterInfo.ParameterType) ?? false;
  }

  public static ParameterInfo? GetEquivalentParameter(this ParameterInfo[] parameters, NamedParameter parameter) {
    return parameters.FirstOrDefault(p => p.IsEquivalentTo(parameter));
  }

  public static ParameterInfo? GetEquivalentParameter(this ParameterInfo[] parameters, PositionalParameter parameter) {
    return parameters.FirstOrDefault(p => p.IsEquivalentTo(parameter));
  }

  public static bool HasSameParameterCount(this ParameterInfo[] parameters, int parameterCount) {
    var optionalParameterCount = parameters.Count(p => p.IsOptional);

    if (parameters.Length == parameterCount) {
      return true;
    }

    if (parameters.Length - optionalParameterCount <= parameterCount) {
      return true;
    }

    return false;
  }
}