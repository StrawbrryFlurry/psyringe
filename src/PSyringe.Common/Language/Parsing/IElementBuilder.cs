using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing;

public interface IElementBuilder {
  public IScriptElement Script { get; }
  public IElementFactory Factory { get; }

  public void SetStartupFunction(FunctionDefinitionAst startupFunctionAst);
  public void AddInjectionSite(FunctionDefinitionAst injectionSiteAst);
  public void AddParameterToInjectionSite(FunctionDefinitionAst injectionSiteAst, ParameterAst parameterAst);
  public void AddInjectVariable(AttributedExpressionAst injectVariableAst);
  public void AddInjectCredential(AttributedExpressionAst injectCredentialAst);
  public void AddInjectTemplate(AttributedExpressionAst injectTemplateAst);
  public void AddOnError(FunctionDefinitionAst onErrorAst);
  public void AddOnLoad(FunctionDefinitionAst onLoadAst);
  public void AddBeforeUnload(FunctionDefinitionAst beforeUnloadAst);
  
  public IScriptElement Build();
}