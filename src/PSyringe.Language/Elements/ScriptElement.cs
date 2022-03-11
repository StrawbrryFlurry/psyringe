using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class ScriptElement : IScriptElement {
  private readonly List<IBeforeUnloadCallbackElement> _beforeUnloadFunctions = new();
  private readonly List<IInjectCredentialElement> _injectCredentialElements = new();
  private readonly List<IInjectDatabaseElement> _injectDatabaseElements = new();
  private readonly List<IInjectionSiteElement> _injectionSiteElements = new();
  private readonly List<IInjectTemplateElement> _injectTemplateElements = new();
  private readonly List<IInjectVariableElement> _injectVariableElements = new();

  private readonly List<IOnErrorCallbackElement> _onErrorFunctions = new();
  private readonly List<IOnLoadCallbackElement> _onLoadFunctions = new();

  public ScriptElement(ScriptBlockAst ast) {
    ScriptBlockAst = ast;
  }

  public IStartupFunctionElement? StartupFunction { get; private set; }
  public ScriptBlockAst ScriptBlockAst { get; }

  public IEnumerable<IInjectionSiteElement> InjectionSiteElements => _injectionSiteElements;
  public IEnumerable<IInjectVariableElement> InjectVariableElements => _injectVariableElements;
  public IEnumerable<IInjectCredentialElement> InjectCredentialElements => _injectCredentialElements;
  public IEnumerable<IInjectTemplateElement> InjectTemplateElements => _injectTemplateElements;
  public IEnumerable<IInjectDatabaseElement> InjectDatabaseElements => _injectDatabaseElements;

  public IEnumerable<IBeforeUnloadCallbackElement> BeforeUnloadFunctions => _beforeUnloadFunctions;
  public IEnumerable<IOnLoadCallbackElement> OnLoadFunctions => _onLoadFunctions;
  public IEnumerable<IOnErrorCallbackElement> OnErrorFunctions => _onErrorFunctions;

  public void AddInjectionSite(IInjectionSiteElement site) {
    _injectionSiteElements.Add(site);
  }

  public void AddInjectVariable(IInjectVariableElement injectVariable) {
    _injectVariableElements.Add(injectVariable);
  }

  public void AddInjectCredential(IInjectCredentialElement injectCredential) {
    _injectCredentialElements.Add(injectCredential);
  }

  public void AddInjectTemplate(IInjectTemplateElement injectTemplate) {
    _injectTemplateElements.Add(injectTemplate);
  }

  public void AddInjectDatabase(IInjectDatabaseElement injectDatabase) {
    _injectDatabaseElements.Add(injectDatabase);
  }

  public void SetStartupFunction(IStartupFunctionElement startupFunctionFunction) {
    StartupFunction = startupFunctionFunction;
  }

  public void AddBeforeUnloadFunction(IBeforeUnloadCallbackElement beforeUnloadCallbackFunction) {
    _beforeUnloadFunctions.Add(beforeUnloadCallbackFunction);
  }

  public void AddOnLoadFunction(IOnLoadCallbackElement onLoadCallbackFunction) {
    _onLoadFunctions.Add(onLoadCallbackFunction);
  }

  public void AddOnErrorFunction(IOnErrorCallbackElement onErrorCallbackFunction) {
    _onErrorFunctions.Add(onErrorCallbackFunction);
  }
}