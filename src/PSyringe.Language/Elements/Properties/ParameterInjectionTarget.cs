using System.Management.Automation.Language;
using System.Text.RegularExpressions;

namespace PSyringe.Language.Elements.Properties;

public class ParameterInjectionTarget {
  public AttributedExpressionAst ast;

  public string Target { get; }

  public ParameterInjectionTarget(AttributedExpressionAst ast) {
  }

  public bool HasDefaultValue() {
    throw new NotImplementedException();
  }

  private string GetImplicitTargetName() {
    var variableExpression = ast.Child as VariableExpressionAst;

    if (variableExpression.VariablePath.IsUnqualified) {
      return variableExpression.VariablePath.UserPath;
    }

    // If the path is "qualified", remove the scope name "$script:xxx".
    return new Regex(".*:").Replace(variableExpression.VariablePath.UserPath, "", 1);
  }
}