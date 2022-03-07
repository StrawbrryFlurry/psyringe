using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class ScriptElement : IScriptElement {
  private readonly List<IBeforeUnloadElement> _beforeUnloadFunctions = new();
  private readonly List<IInjectCredentialElement> _injectCredentials = new();
  private readonly List<IInjectionSiteElement> _injectionSites = new();
  private readonly List<IInjectTemplateElement> _injectTemplates = new();
  private readonly List<IInjectVariableElement> _injectVariables = new();
  private readonly List<IOnErrorElement> _onErrorFunctions = new();
  private readonly List<IOnLoadElement> _onLoadFunctions = new();

  public IStartupFunctionElement? StartupFunction { get; private set; }

  public IEnumerable<IInjectionSiteElement> InjectionSites => _injectionSites;
  public IEnumerable<IInjectVariableElement> InjectVariables => _injectVariables;
  public IEnumerable<IInjectCredentialElement> InjectCredentials => _injectCredentials;
  public IEnumerable<IInjectTemplateElement> InjectTemplates => _injectTemplates;

  public IEnumerable<IBeforeUnloadElement> BeforeUnloadFunctions => _beforeUnloadFunctions;
  public IEnumerable<IOnLoadElement> OnLoadFunctions => _onLoadFunctions;
  public IEnumerable<IOnErrorElement> OnErrorFunctions => _onErrorFunctions;

  public void AddInjectionSite(IInjectionSiteElement site) {
    _injectionSites.Add(site);
  }

  public void AddInjectVariable(IInjectVariableElement injectVariable) {
    _injectVariables.Add(injectVariable);
  }

  public void AddInjectCredential(IInjectCredentialElement injectCredential) {
    _injectCredentials.Add(injectCredential);
  }

  public void AddInjectTemplate(IInjectTemplateElement injectTemplate) {
    _injectTemplates.Add(injectTemplate);
  }

  public void SetStartupFunction(IStartupFunctionElement startupFunctionFunction) {
    StartupFunction = startupFunctionFunction;
  }

  public void AddBeforeUnload(IBeforeUnloadElement beforeUnloadFunction) {
    _beforeUnloadFunctions.Add(beforeUnloadFunction);
  }

  public void AddOnLoad(IOnLoadElement onLoadFunction) {
    _onLoadFunctions.Add(onLoadFunction);
  }

  public void AddOnError(IOnErrorElement onErrorFunction) {
    _onErrorFunctions.Add(onErrorFunction);
  }
}