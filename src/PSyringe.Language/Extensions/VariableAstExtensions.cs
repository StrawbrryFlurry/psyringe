using System.Management.Automation.Language;
using System.Text.RegularExpressions;

namespace PSyringe.Language.Extensions;

public static class VariableAstExtensions {
  public static string GetName(this VariableExpressionAst ast) {
    var variable = ast.VariablePath;

    if (variable.IsUnqualified) {
      return variable.UserPath;
    }

    // If the path is "qualified", remove the scope name "$script:xxx".
    return new Regex(@"\w*:").Replace(variable.UserPath, "", 1);
  }

  public static string? GetVariableName(this AssignmentStatementAst ast) {
    var variable = ast.Left.FindOfType<VariableExpressionAst>();

    if (variable == null) {
      return null;
    }

    return variable.GetName();
  }
}