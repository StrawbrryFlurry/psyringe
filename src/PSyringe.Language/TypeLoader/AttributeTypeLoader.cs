using System.Management.Automation.Language;
using PSyringe.Language.Exceptions;
using PSyringe.Language.Extensions;
using PSyringe.Language.TypeLoader.Parameters;

namespace PSyringe.Language.TypeLoader;

public static class AttributeTypeLoader {
  public static T CreateAttributeInstanceFromAst<T>(
    AttributeAst attributeAst
  ) where T : Attribute {
    var namedParameters = MakeNamedParametersFromArguments(attributeAst.NamedArguments);
    var positionalParameters = MakePositionalParametersFromArguments(attributeAst.PositionalArguments);
    var collection = new ParameterCollection(namedParameters, positionalParameters);

    var attributeType = attributeAst.GetReflectionAttributeType();

    return (T) TypeLoader.CreateInstanceOfType(attributeType, collection);
  }

  internal static IList<NamedParameter> MakeNamedParametersFromArguments(
    IEnumerable<NamedAttributeArgumentAst> namedArguments) {
    var namedParameters = new List<NamedParameter>();

    foreach (var namedArgument in namedArguments) {
      var value = GetNamedAttributeParameterValue(namedArgument);

      if (value is null) {
        throw InvalidAttributeUsageException.NonStaticParameter((AttributeAst) namedArgument.Parent,
          namedArgument.ArgumentName);
      }

      namedParameters.Add(new NamedParameter {
        Name = namedArgument.ArgumentName,
        Value = value,
        Type = value.GetType()
      });
    }

    return namedParameters;
  }

  internal static IList<PositionalParameter> MakePositionalParametersFromArguments(
    IEnumerable<ExpressionAst> positionalArguments
  ) {
    var positionalParameters = new List<PositionalParameter>();

    for (var i = 0; i < positionalArguments.Count(); i++) {
      var positionalArgument = positionalArguments.ElementAt(i);
      var value = GetValueFromExpressionAst(positionalArgument);

      if (value is null) {
        throw InvalidAttributeUsageException.NonStaticParameter((AttributeAst) positionalArgument.Parent, i);
      }

      positionalParameters.Add(new PositionalParameter {
        Value = value,
        Type = value.GetType()
      });
    }

    return positionalParameters;
  }

  private static object? GetNamedAttributeParameterValue(NamedAttributeArgumentAst argumentAst) {
    // If the parameter has no expression it is
    // implicitly treated as true
    if (argumentAst.ExpressionOmitted) {
      return true;
    }

    return GetValueFromExpressionAst(argumentAst.Argument);
  }

  private static object? GetValueFromExpressionAst(ExpressionAst ast) {
    return ast switch {
      StringConstantExpressionAst stringExpressionAst => stringExpressionAst.Value,
      TypeExpressionAst typeExpressionAst => typeExpressionAst.TypeName.GetReflectionType(),
      VariableExpressionAst variableExpressionAst => InternalVariableValueOrNullIfIsUserVariable(variableExpressionAst),
      _ => null
    };
  }

  /// Booleans such as $true are variable expressions like
  /// every other variable. We don't accept unset variables because we
  /// cannot obtain the runtime value of the variable. If the variable
  /// is one of the built-in variables that we know about, we return their
  /// value. Otherwise null.
  private static bool? InternalVariableValueOrNullIfIsUserVariable(VariableExpressionAst variableAst) {
    var variableName = variableAst.VariablePath.UserPath.ToLower();

    return variableName switch {
      "true" => true,
      "false" => false,
      "null" => false,
      _ => null
    };
  }
}