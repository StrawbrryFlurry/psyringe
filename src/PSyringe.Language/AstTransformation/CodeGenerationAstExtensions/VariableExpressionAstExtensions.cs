using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class VariableExpressionAstExtensions {
  public static string ToStringFromAst(this VariableExpressionAst ast) {
    var variableName = ast.VariablePath.UserPath;
    var variableSing = ast.Splatted ? '@' : '$';

    return $"{variableSing}{variableName}";
  }
}