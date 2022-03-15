using System.Management.Automation.Language;
using System.Text.RegularExpressions;
using PSyringe.Common.Language.Parsing.Elements.Properties;

namespace PSyringe.Language.Elements.Properties;

public class ParameterInjectionTarget : IInjectionTarget {
  public AttributedExpressionAst ast;


  public ParameterInjectionTarget(AttributedExpressionAst ast) {
  }

  public string Target { get; }

  public bool HasDefaultValue() {
    throw new NotImplementedException();
  }

  private string GetImplicitTargetName() {
    var variableExpression = ast.Child as VariableExpressionAst;

    if (variableExpression.VariablePath.IsUnqualified) {
      return variableExpression.VariablePath.UserPath;
    }

    return new Regex(".*:").Replace(variableExpression.VariablePath.UserPath, "", 1);
  }
}