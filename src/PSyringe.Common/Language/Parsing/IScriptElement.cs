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

  public IEnumerable<IBeforeUnloadElement> BeforeUnloadFunctions { get; }
  public IEnumerable<IOnLoadElement> OnLoadFunctions { get; }
  public IEnumerable<IOnErrorElement> OnErrorFunctions { get; }

  public void SetStartupFunction(IStartupFunctionElement injectionSite);
  public void AddInjectionSite(IInjectionSiteElement injectionSite);
  
  public void AddInjectVariable(IInjectVariableElement injectVariable);
  public void AddInjectCredential(IInjectCredentialElement injectVariable);
  public void AddInjectDatabase(IInjectDatabaseElement injectDatabase);

  public void AddInjectTemplate(IInjectTemplateElement injectTemplate);
  
  public void AddBeforeUnloadFunction(IBeforeUnloadElement beforeUnload);
  public void AddOnLoadFunction(IOnLoadElement onLoad);
  public void AddOnErrorFunction(IOnErrorElement onError);
}