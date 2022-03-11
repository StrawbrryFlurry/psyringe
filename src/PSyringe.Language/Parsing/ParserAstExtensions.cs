using System.Management.Automation.Language;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Parsing;

internal static class ParserAstExtensions {
  internal static IEnumerable<FunctionDefinitionAst> GetFunctionDefinitionWithAttribute<T>(
    this IEnumerable<FunctionDefinitionAst>? functionDefinitionAst
  ) where T : Attribute {
    return functionDefinitionAst is null 
      ? Enumerable.Empty<FunctionDefinitionAst>() 
      : functionDefinitionAst.Where(HasAttributeOfType<T>);
  }

  internal static bool HasAttributeOfType<T>(this FunctionDefinitionAst ast) where T : Attribute {
    var attributes = ast.GetAttributes();
    return attributes.HasAttributeOfType<T>();
  }

  internal static IEnumerable<AttributedExpressionAst> GetAttributedScriptBlockExpressionOfType<T> (
    this IEnumerable<AttributedExpressionAst>? attributedExpressions
  ) where T : Attribute {
    return attributedExpressions is null 
      ? Enumerable.Empty<AttributedExpressionAst>() 
      : attributedExpressions.Where(IsAttributedScriptBlockExpressionOfType<T>);
  }

  internal static IEnumerable<AttributedExpressionAst> GetAttributedVariableExpressionsOfType<T>(
    this IEnumerable<AttributedExpressionAst>? attributedExpressions
  ) where T : Attribute {
    return attributedExpressions is null 
      ? Enumerable.Empty<AttributedExpressionAst>() 
      : attributedExpressions.Where(IsAttributedVariableExpressionOfType<T>);
  }

  internal static IEnumerable<AttributedExpressionAst> GetAttributedExpressionsOfType<T>(
    this IEnumerable<AttributedExpressionAst>? attributedExpressions
  ) where T : Attribute {
    return attributedExpressions is null 
      ? Enumerable.Empty<AttributedExpressionAst>() 
      : attributedExpressions.Where(IsAttributedWithType<T>);
  }

  internal static IEnumerable<ParameterAst> GetParameters(this FunctionDefinitionAst ast) {
    var parameterBlock = ast.GetParameterBlock();
    return parameterBlock?.Parameters ?? Enumerable.Empty<ParameterAst>();
  }

  internal static bool IsAttributedVariableExpressionOfType<T>(this AttributedExpressionAst ast) {
    return ast.IsVariableExpression() && ast.IsAttributedWithType<T>();
  }

  internal static bool IsAttributedScriptBlockExpressionOfType<T>(this AttributedExpressionAst ast) {
    return ast.IsScriptBlockExpression() && ast.IsAttributedWithType<T>();
  }

  internal static bool IsScriptBlockExpression(this AttributedExpressionAst ast) {
    return ast.Child is ScriptBlockExpressionAst;
  }
  
  internal static bool IsAttributedWithType<T>(this AttributedExpressionAst ast) {
    return ast.Attribute.IsOfExactType<T>();
  }

  internal static bool IsVariableExpression(this AttributedExpressionAst ast) {
    return ast.Child is VariableExpressionAst;
  }
}