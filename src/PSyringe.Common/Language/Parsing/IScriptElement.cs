using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Common.Language.Parsing;

public interface IScriptElement {
  public IStartupElement? StartupFunction { get; }

  public IEnumerable<IInjectionSiteElement> InjectionSites { get; }
  public IEnumerable<IInjectVariableElement> InjectVariables { get; }
  public IEnumerable<IInjectCredentialElement> InjectCredentials { get; }
  public IEnumerable<IInjectTemplateElement> InjectTemplates { get; }

  public IEnumerable<IBeforeUnloadElement> BeforeUnloadFunctions { get; }
  public IEnumerable<IOnLoadElement> OnLoadFunctions { get; }
  public IEnumerable<IOnErrorElement> OnErrorFunctions { get; }


  public void AddInjectionSite(IInjectionSiteElement injectionSite);
  public void AddInjectVariable(IInjectVariableElement injectVariable);
  public void AddInjectCredential(IInjectCredentialElement injectVariable);
  public void AddInjectTemplate(IInjectTemplateElement injectTemplate);

  public void SetStartupFunction(IStartupElement injectionSite);
  public void AddBeforeUnload(IBeforeUnloadElement beforeUnload);
  public void AddOnLoad(IOnLoadElement onLoad);
  public void AddOnError(IOnErrorElement onError);
}