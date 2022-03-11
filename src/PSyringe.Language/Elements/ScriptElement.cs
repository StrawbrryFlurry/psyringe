using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class ScriptElement : IScriptElement {
  private readonly List<IInjectCredentialElement> _injectCredentialElements = new();
  private readonly List<IInjectionSiteElement> _injectionSiteElements = new();
  private readonly List<IInjectTemplateElement> _injectTemplateElements = new();
  private readonly List<IInjectVariableElement> _injectVariableElements = new();
  private readonly List<IInjectDatabaseElement> _injectDatabaseElements = new();

  private readonly List<IOnErrorElement> _onErrorFunctions = new();
  private readonly List<IOnLoadElement> _onLoadFunctions = new();
  private readonly List<IBeforeUnloadElement> _beforeUnloadFunctions = new();

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

  public IEnumerable<IBeforeUnloadElement> BeforeUnloadFunctions => _beforeUnloadFunctions;
  public IEnumerable<IOnLoadElement> OnLoadFunctions => _onLoadFunctions;
  public IEnumerable<IOnErrorElement> OnErrorFunctions => _onErrorFunctions;

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

  public void AddBeforeUnloadFunction(IBeforeUnloadElement beforeUnloadFunction) {
    _beforeUnloadFunctions.Add(beforeUnloadFunction);
  }

  public void AddOnLoadFunction(IOnLoadElement onLoadFunction) {
    _onLoadFunctions.Add(onLoadFunction);
  }
  
  public void AddOnErrorFunction(IOnErrorElement onErrorFunction) {
    _onErrorFunctions.Add(onErrorFunction);
  }
}