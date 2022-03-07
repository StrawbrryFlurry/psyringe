using System.Collections.ObjectModel;
using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Attributes;

namespace PSyringe.Language.Extensions; 

internal static class FunctionDefinitionAstExtensions {
  internal static IReadOnlyCollection<AttributeBaseAst> GetAttributes(this FunctionDefinitionAst ast) {
    var parameterBlock = ast.GetParameterBlock();
    return parameterBlock?.Attributes ?? MakeEmptyReadOnlyCollection<AttributeBaseAst>();
  }
  
  internal static ParamBlockAst? GetParameterBlock(this FunctionDefinitionAst ast) {
    return ast.Body.ParamBlock;
  }
  
  private static IReadOnlyCollection<T> MakeEmptyReadOnlyCollection<T>() {
    return new ReadOnlyCollection<T>(new List<T>());
  }
}