using System.Management.Automation.Language;

namespace PSyringe.Language.AstExtensions;

public static class VariableExpressionAstExtension {
  public static string GetAstAsString(this VariableExpressionAst ast) {
    var variableName = ast.VariablePath.UserPath;
    var variableSing = ast.Splatted ? '@' : '$';

    return $"{variableSing}{variableName}";
  }
}