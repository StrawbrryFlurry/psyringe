using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Parsing;

public class ScriptVisitor : AstVisitor2, IScriptVisitor {
  private readonly AstVisitAction _continue = AstVisitAction.Continue;
  public readonly List<UsingStatementAst> UsingStatements = new();

  public ScriptBlockAst? Ast { get; private set; }
  public bool HasVisited { get; private set; }

  public List<FunctionDefinitionAst> CallbackFunctions { get; } = new();

  public List<AttributedExpressionAst> InjectExpressions { get; } = new();
  public List<FunctionDefinitionAst> InjectionSites { get; } = new();
  public List<AttributedExpressionAst> ProvideExpressions { get; } = new();

  public void Visit(ScriptBlockAst scriptBlockAst) {
    Ast = scriptBlockAst;
    scriptBlockAst.Visit(this);
    HasVisited = true;
  }

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
    return ast.Attribute.IsAssignableToType<IProvideTargetAttribute>();
  }

  private bool IsInjectExpression(AttributedExpressionAst ast) {
    return ast.Attribute.IsAssignableToType<IInjectionTargetAttribute>();
  }

  private AstVisitAction AddInjectExpression(AttributedExpressionAst ast) {
    InjectExpressions.Add(ast);
    return _continue;
  }

  public override AstVisitAction VisitFunctionDefinition(FunctionDefinitionAst ast) {
    if (IsInjectionSite(ast)) {
      return AddInjectionSite(ast);
    }

    if (IsCallbackFunction(ast)) {
      return AddCallbackFunction(ast);
    }

    return _continue;
  }

  private bool IsInjectionSite(FunctionDefinitionAst ast) {
    var attributes = ast.GetAttributes();
    return attributes.HasAttributeAssignableToType<IInjectionSiteAttribute>();
  }

  private AstVisitAction AddInjectionSite(FunctionDefinitionAst site) {
    InjectionSites.Add(site);
    return _continue;
  }

  private bool IsCallbackFunction(FunctionDefinitionAst ast) {
    var attributes = ast.GetAttributes();
    return attributes.HasAttributeAssignableToType<ICallbackAttribute>();
  }

  private AstVisitAction AddCallbackFunction(FunctionDefinitionAst ast) {
    CallbackFunctions.Add(ast);
    return _continue;
  }
}