using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Common.Language.Parsing;

/// <summary>
/// Abstraction of a script, containing all parsed wrapper elements
/// that are used by the compiler to create a script.
/// </summary>
public interface IScriptElement {
  public IStartupFunctionElement? StartupFunction { get; }
  public ScriptBlockAst ScriptBlockAst { get; }

  public IEnumerable<IInjectionSiteElement> InjectionSiteElements { get; }
  public IEnumerable<IInjectVariableElement> InjectVariableElements { get; }
  public IEnumerable<IInjectCredentialElement> InjectCredentialElements { get; }
  public IEnumerable<IInjectDatabaseElement> InjectDatabaseElements { get; }
  public IEnumerable<IInjectConstantElement> InjectConstantElements { get; }
  public IEnumerable<IInjectTemplateElement> InjectTemplateElements { get; }

  public IEnumerable<IBeforeUnloadCallbackElement> BeforeUnloadCallbacks { get; }
  public IEnumerable<IOnLoadCallbackElement> OnLoadCallbacks { get; }
  public IEnumerable<IOnErrorCallbackElement> OnErrorCallbacks { get; }

  public void SetStartupFunction(IStartupFunctionElement injectionSite);
  public void AddInjectionSite(IInjectionSiteElement injectionSite);

  public void AddInjectVariable(IInjectVariableElement injectVariable);
  public void AddInjectCredential(IInjectCredentialElement injectVariable);
  public void AddInjectDatabase(IInjectDatabaseElement injectDatabase);

  public void AddInjectTemplate(IInjectTemplateElement injectTemplate);

  public void AddBeforeUnloadCallback(IBeforeUnloadCallbackElement beforeUnloadCallback);
  public void AddOnLoadCallback(IOnLoadCallbackElement onLoadCallback);
  public void AddOnErrorCallback(IOnErrorCallbackElement onErrorCallback);
  public void AddInjectConstant(IInjectConstantElement injectConstant);
}