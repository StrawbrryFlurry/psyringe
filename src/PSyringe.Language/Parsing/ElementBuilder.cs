using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Parsing;

public class ElementBuilder : IElementBuilder {
  public ElementBuilder(IElementFactory factory) {
    Factory = factory;
    Script = Factory.CreateScript();
  }

  public IScriptElement Script { get; }
  public IElementFactory Factory { get; }

  public IScriptElement Build() {
    return Script;
  }

  public void SetStartupFunction(FunctionDefinitionAst startupFunctionAst) {
    if (ScriptHasStartupFunction()) {
      throw new InvalidOperationException("Script already has a startup function");
    }

    var startupFunction = Factory.CreateStartupFunction(startupFunctionAst);
    Script.SetStartupFunction(startupFunction);
  }

  public void AddInjectionSite(FunctionDefinitionAst injectionSiteAst) {
    var injectionSite = Factory.CreateInjectionSite(injectionSiteAst);
    Script.AddInjectionSite(injectionSite);
  }

  public void AddParameterToInjectionSite(FunctionDefinitionAst injectionSiteAst, ParameterAst parameterAst) {
    var parameter = Factory.CreateInjectionSiteParameter(parameterAst);
    var site = GetInjectionSiteInScriptFromAst(injectionSiteAst);
    site.AddParameter(parameter);
  }

  public void AddInjectVariable(AttributedExpressionAst injectVariableAst) {
    var variable = Factory.CreateInjectVariable(injectVariableAst);
    Script.AddInjectVariable(variable);
  }

  public void AddInjectCredential(AttributedExpressionAst injectCredentialAst) {
    var credential = Factory.CreateInjectCredential(injectCredentialAst);
    Script.AddInjectCredential(credential);
  }

  public void AddInjectTemplate(AttributedExpressionAst injectTemplateAst) {
    var template = Factory.CreateInjectTemplate(injectTemplateAst);
    Script.AddInjectTemplate(template);
  }

  public void AddOnError(FunctionDefinitionAst onErrorAst) {
    var onError = Factory.CreateOnError(onErrorAst);
    Script.AddOnError(onError);
  }

  public void AddOnLoad(FunctionDefinitionAst onLoadAst) {
    var onLoad = Factory.CreateOnLoad(onLoadAst);
    Script.AddOnLoad(onLoad);
  }

  public void AddBeforeUnload(FunctionDefinitionAst beforeUnloadAst) {
    var beforeUnload = Factory.CreateBeforeUnload(beforeUnloadAst);
    Script.AddBeforeUnload(beforeUnload);
  }

  private IInjectionSiteElement GetInjectionSiteInScriptFromAst(FunctionDefinitionAst ast) {
    return Script.InjectionSites.Single(site => site.Ast == ast);
  }

  private bool ScriptHasStartupFunction() {
    return Script?.StartupFunction is not null;
  }
}