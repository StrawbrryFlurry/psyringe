using System.Collections.ObjectModel;
using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Parsing;

public class ScriptVisitor : AstVisitor2, IScriptVisitor {
  public readonly List<UsingStatementAst> UsingStatements = new();
  private readonly AstVisitAction _continue = AstVisitAction.Continue;
  
  public List<FunctionDefinitionAst> CallbackFunctions { get; } = new();
  public Dictionary<FunctionDefinitionAst, IEnumerable<ParameterAst>> FunctionParameters { get; } = new();

  public List<AttributedExpressionAst> InjectExpressions { get; } = new();
  public List<FunctionDefinitionAst> InjectionSites { get; } = new();
  public List<AttributedExpressionAst> ProvideExpressions { get; } = new();

  public override AstVisitAction VisitUsingStatement(UsingStatementAst usingStatementAst) {
    UsingStatements.Add(usingStatementAst);
    return _continue;
  }

  public override AstVisitAction VisitAttributedExpression(AttributedExpressionAst ast) {
    if (IsInjectExpression(ast)) {
      return AddInjectExpression(ast);
    }

    if (IsProvideExpression(ast)) {
      return AddProvideExpression(ast);
    }

    return _continue;
  }

  private AstVisitAction AddProvideExpression(AttributedExpressionAst ast) {
    ProvideExpressions.Add(ast);
    return _continue;
  }

  private bool IsProvideExpression(AttributedExpressionAst ast) {
    return ast.Attribute.IsOfType<IProvideTargetAttribute>();
  }

  private AstVisitAction AddCallbackFunction(FunctionDefinitionAst ast) {
    CallbackFunctions.Add(ast);
    return _continue;
  }

  private bool IsCallbackFunction(FunctionDefinitionAst ast) {
    var attributes = GetFunctionAttributes(ast);
    return attributes.HasAttributeOfType<ICallbackAttribute>();
  }


  private bool IsInjectExpression(AttributedExpressionAst ast) {
    return ast.Attribute.IsOfType<IInjectionTargetAttribute>();
  }

  private AstVisitAction AddInjectExpression(AttributedExpressionAst ast) {
    InjectExpressions.Add(ast);
    return _continue;
  }

  public override AstVisitAction VisitFunctionDefinition(FunctionDefinitionAst ast) {
    if (AcceptsParameters(ast)) {
      AddFunctionParameters(ast);
    }

    if (IsInjectionSite(ast)) {
      return AddInjectionSite(ast);
    }

    if (IsCallbackFunction(ast)) {
      return AddCallbackFunction(ast);
    }

    return _continue;
  }

  private bool AcceptsParameters(FunctionDefinitionAst ast) {
    var attributes = GetFunctionAttributes(ast);
    return attributes.HasAttributeOfType<IAcceptsParameters>();
  }

  private AstVisitAction AddInjectionSite(FunctionDefinitionAst site) {
    InjectionSites.Add(site);
    return _continue;
  }

  private void AddFunctionParameters(FunctionDefinitionAst site) {
    var parameterBlock = GetFunctionParameterBlock(site)!;
    FunctionParameters.Add(site, parameterBlock.Parameters);
  }

  private bool IsInjectionSite(FunctionDefinitionAst ast) {
    var attributes = GetFunctionAttributes(ast);
    return attributes.HasAttributeOfType<IInjectionSiteAttribute>();
  }

  private IReadOnlyCollection<AttributeBaseAst> GetFunctionAttributes(FunctionDefinitionAst ast) {
    var parameterBlock = GetFunctionParameterBlock(ast);
    return parameterBlock?.Attributes ?? MakeEmptyReadOnlyCollection<AttributeBaseAst>();
  }

  private ParamBlockAst? GetFunctionParameterBlock(FunctionDefinitionAst ast) {
    return ast.Body.ParamBlock;
  }

  private IReadOnlyCollection<T> MakeEmptyReadOnlyCollection<T>() {
    return new ReadOnlyCollection<T>(new List<T>());
  }
}