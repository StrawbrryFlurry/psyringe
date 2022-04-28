using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class UsingExpressionAstExtensions {
  public static string ToStringFromAst(this UsingExpressionAst ast) {
    // TODO: ${ using:SomeVar } might brake this
    var variable = (VariableExpressionAst) ast.SubExpression;
    var variableName = variable.VariablePath.UserPath;

    return $"$using:{variableName}";
  }
}