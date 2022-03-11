using System.Management.Automation.Language;

namespace PSyringe.Language.Extensions;

public static class FunctionDefinitionAstExtensions {
  public static IEnumerable<AttributeBaseAst> GetAttributes(this FunctionDefinitionAst ast) {
    var parameterBlock = ast.GetParameterBlock();
    return parameterBlock?.Attributes ?? Enumerable.Empty<AttributeBaseAst>();
  }

  public static IEnumerable<ParameterAst> GetParameters(this FunctionDefinitionAst ast) {
    var parameterBlock = ast.GetParameterBlock();
    return parameterBlock?.Parameters ?? Enumerable.Empty<ParameterAst>();
  }

  public static ParamBlockAst? GetParameterBlock(this FunctionDefinitionAst ast) {
    return ast.Body.ParamBlock;
  }

  public static bool HasAttributeOfType<T>(this FunctionDefinitionAst ast) where T : Attribute {
    var attributes = ast.GetAttributes();
    return attributes.Any(a => a.IsOfExactType<T>());
  }
}