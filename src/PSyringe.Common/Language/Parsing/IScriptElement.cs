using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Common.Language.Parsing;

public interface IScriptElement {
  public IStartupFunctionElement? StartupFunction { get; }
  public ScriptBlockAst ScriptBlockAst { get; }

  public IEnumerable<IInjectionSiteElement> InjectionSiteElements { get; }
  public IEnumerable<IInjectVariableElement> InjectVariableElements { get; }
  public IEnumerable<IInjectCredentialElement> InjectCredentialElements { get; }
  public IEnumerable<IInjectDatabaseElement> InjectDatabaseElements { get; }
  public IEnumerable<IInjectTemplateElement> InjectTemplateElements { get; }

  public IEnumerable<IBeforeUnloadCallbackElement> BeforeUnloadFunctions { get; }
  public IEnumerable<IOnLoadCallbackElement> OnLoadFunctions { get; }
  public IEnumerable<IOnErrorCallbackElement> OnErrorFunctions { get; }

  public void SetStartupFunction(IStartupFunctionElement injectionSite);
  public void AddInjectionSite(IInjectionSiteElement injectionSite);

  public void AddInjectVariable(IInjectVariableElement injectVariable);
  public void AddInjectCredential(IInjectCredentialElement injectVariable);
  public void AddInjectDatabase(IInjectDatabaseElement injectDatabase);

  public void AddInjectTemplate(IInjectTemplateElement injectTemplate);

  public void AddBeforeUnloadFunction(IBeforeUnloadCallbackElement beforeUnloadCallback);
  public void AddOnLoadFunction(IOnLoadCallbackElement onLoadCallback);
  public void AddOnErrorFunction(IOnErrorCallbackElement onErrorCallback);
}