using System.Reflection;
using PSyringe.Language.TypeLoader.Extensions;

namespace PSyringe.Language.TypeLoader.Parameters;

public class ParameterCollection {
  private IList<NamedParameter> NamedParameters { get; }
  private IList<PositionalParameter> PositionalParameters { get; }

  public ParameterCollection(
    IList<NamedParameter> namedParameters,
    IList<PositionalParameter> positionalParameters
  ) {
    NamedParameters = namedParameters;
    PositionalParameters = positionalParameters;
  }

  public int GetParameterCount() {
    return NamedParameters.Count + PositionalParameters.Count;
  }

  public bool TryGetMethodOverloadArguments(
    ParameterInfo[] overloadParameters,
    out object[]? outOverloadArguments
  ) {
    outOverloadArguments = null;
    var overloadArguments = new List<PositionalParameter>();

    foreach (var parameter in NamedParameters) {
      var parameterInfo = overloadParameters.GetEquivalentParameter(parameter);

      if (parameterInfo == null) {
        return false;
      }

      overloadArguments.Add(new PositionalParameter {
        Position = parameterInfo.Position,
        Type = parameterInfo.ParameterType,
        Value = parameter.Value
      });
    }

    var position = 0;
    var positionalParameters = PositionalParameters
                               .OrderBy(p => p.Position)
                               .Select(p => {
                                 while (overloadArguments.Any(a => a.Position == position)) {
                                   position++;
                                 }

                                 return new PositionalParameter {
                                   Position = position,
                                   Type = p.Type,
                                   Value = p.Value
                                 };
                               });

    foreach (var parameter in positionalParameters) {
      var parameterInfo = overloadParameters.GetEquivalentParameter(parameter);

      if (parameterInfo == null) {
        return false;
      }

      overloadArguments.Add(parameter);
    }

    if (!TryValidateParameterOverload(overloadParameters, overloadArguments, out var arguments)) {
      return false;
    }

    outOverloadArguments = arguments;
    return true;
  }

  internal static bool TryValidateParameterOverload(
    ParameterInfo[] overloadParameters,
    List<PositionalParameter> actualParameters,
    out object[] arguments
  ) {
    arguments = null;
    // Make sure parameters are ordered by their position
    // to match up with the overload parameter position.
    actualParameters = actualParameters.OrderBy(p => p.Position).ToList();
    var argumentList = new List<object>();

    for (var i = 0; i < overloadParameters.Length; i++) {
      var parameter = overloadParameters[i];
      var valueParameter = actualParameters.ElementAtOrDefault(i);

      if (valueParameter is null) {
        if (!parameter.IsOptional) {
          return false;
        }

        argumentList.Add(null);
        continue;
      }

      var value = valueParameter.Value;
      if (parameter.IsOfSameTypeAs(value)) {
        argumentList.Add(value);
        continue;
      }

      // A mandatory parameter is missing
      return false;
    }

    arguments = argumentList.ToArray();
    return true;
  }
}