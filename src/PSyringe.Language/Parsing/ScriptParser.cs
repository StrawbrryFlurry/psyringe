using System.Management.Automation.Language;
using System.Text;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Attributes;
using PSyringe.Language.Attributes.Base;

namespace PSyringe.Language.Parsing;

public class ScriptParser : IScriptParser {
  internal readonly IElementFactory ElementFactory;
  internal readonly IScriptElement ScriptElement;

  internal readonly IScriptVisitor Visitor;

  public ScriptParser(IScriptVisitor visitor, IElementFactory elementFactory) {
    Visitor = visitor;
    ElementFactory = elementFactory;
    ScriptElement = ElementFactory.CreateScript();
  }

  internal string ScriptBeforeParsing { get; private set; } = "";
  internal ScriptBlockAst ScriptAst { get; private set; }

  private IEnumerable<AttributedExpressionAst> InjectExpressions => Visitor.InjectExpressions;
  private IEnumerable<FunctionDefinitionAst> CallbackFunctions => Visitor.CallbackFunctions;

  public IScriptElement Parse(string script) {
    PrepareAndParseScript(script);
    VisitScriptAst();
    return CreateScriptElementFromVisitor();
  }

  private IScriptElement CreateScriptElementFromVisitor() {
    AddStartupFunctionElementIfDefined();
    AddAllInjectionSiteElements();
    AddAllInjectElements();
    AddAllCallbackElements();

    return ScriptElement;
  }

  private void AddAllCallbackElements() {
    AddAllOnErrorElements();
    AddAllOnLoadedElements();
    AddAllBeforeUnloadElements();
  }

  private void AddAllOnErrorElements() {
    AddAllCallbackElementsWithAttribute<OnErrorAttribute>(AddOnErrorElement);
  }

  private void AddAllOnLoadedElements() {
    AddAllCallbackElementsWithAttribute<OnLoadedAttribute>(AddOnLoadedElement);
  }

  private void AddAllBeforeUnloadElements() {
    AddAllCallbackElementsWithAttribute<BeforeUnloadAttribute>(AddBeforeUnloadElement);
  }

  private void AddOnErrorElement(FunctionDefinitionAst onErrorFnAst) {
    var onErrorFn = ElementFactory.CreateOnError(onErrorFnAst);
    ScriptElement.AddOnError(onErrorFn);
  }

  private void AddOnLoadedElement(FunctionDefinitionAst onLoadedFnAst) {
    var onLoadedFn = ElementFactory.CreateOnLoad(onLoadedFnAst);
    ScriptElement.AddOnLoad(onLoadedFn);
  }

  private void AddBeforeUnloadElement(FunctionDefinitionAst beforeUnloadFnAst) {
    var beforeUnloadFn = ElementFactory.CreateBeforeUnload(beforeUnloadFnAst);
    ScriptElement.AddBeforeUnload(beforeUnloadFn);
  }

  private void AddAllCallbackElementsWithAttribute<T>(
    Action<FunctionDefinitionAst> addCallbackElement
  ) where T : CallbackAttribute {
    var callbackAstsOfType = CallbackFunctions.GetFunctionDefinitionWithAttribute<T>();

    foreach (var callbackAst in callbackAstsOfType) {
      addCallbackElement(callbackAst);
    }
  }

  private void AddAllInjectElements() {
    AddAllInjectVariableElements();
    AddAllInjectCredentialElements();
    AddAllInjectTemplateElements();
  }

  private void AddAllInjectVariableElements() {
    AddAllInjectVariableElementsWithAttribute<InjectAttribute>(AddInjectVariableElement);
  }
  
  private void AddAllInjectCredentialElements() {
    AddAllInjectVariableElementsWithAttribute<InjectCredentialAttribute>(AddInjectCredentialElement);
  }

  private void AddInjectVariableElement(AttributedExpressionAst injectVariableAst) {
    var injectVariable = ElementFactory.CreateInjectVariable(injectVariableAst);
    ScriptElement.AddInjectVariable(injectVariable);
  }

  private void AddInjectCredentialElement(AttributedExpressionAst injectCredentialAst) {
    var injectCredential = ElementFactory.CreateInjectCredential(injectCredentialAst);
    ScriptElement.AddInjectCredential(injectCredential);
  }

  private void AddAllInjectVariableElementsWithAttribute<T>(
    Action<AttributedExpressionAst> addInjectVariableElement
  ) where T : InjectionTargetAttribute {
    var injectAstsOfType = InjectExpressions.GetAttributedVariableExpressionsOfType<T>();

    foreach (var injectAst in injectAstsOfType) {
      addInjectVariableElement(injectAst);
    }
  }
  private void AddAllInjectTemplateElements() {
    AddAllInjectScriptBlockElementsWithAttribute<InjectTemplateAttribute>(AddInjectTemplateElement);
  }

  private void AddInjectTemplateElement(AttributedExpressionAst injectTemplateAst) {
    var injectTemplate = ElementFactory.CreateInjectTemplate(injectTemplateAst);
    ScriptElement.AddInjectTemplate(injectTemplate);
  }

  private void AddAllInjectScriptBlockElementsWithAttribute<T>(
    Action<AttributedExpressionAst> addInjectScriptBlockElement
    ) where T : InjectionTargetAttribute {
    var injectAstsOfType = InjectExpressions.GetAttributedExpressionsOfType<T>();
    
    foreach (var injectAst in injectAstsOfType) {
      addInjectScriptBlockElement(injectAst);
    }
  }
  
  private void AddStartupFunctionElementIfDefined() {
    var startupFunctionAst = GetStartupFunction();

    if (startupFunctionAst is not null) {
      CreateAndSetStartupFunction(startupFunctionAst);
    }
  }

  private void CreateAndSetStartupFunction(FunctionDefinitionAst startupFunctionAst) {
    var startupFn = ElementFactory.CreateStartupFunction(startupFunctionAst);
    ScriptElement.SetStartupFunction(startupFn);
  }

  private FunctionDefinitionAst? GetStartupFunction() {
    var injectionSites = Visitor.InjectionSites;
    return injectionSites.GetFunctionDefinitionWithAttribute<StartupAttribute>().FirstOrDefault();
  }

  private void AddAllInjectionSiteElements() {
    var injectionSiteAsts = Visitor.InjectionSites;

    foreach (var injectionSiteAst in injectionSiteAsts) {
      AddInjectionSiteElementWithParameters(injectionSiteAst);
    }
  }

  private void AddInjectionSiteElementWithParameters(FunctionDefinitionAst injectionSiteAst) {
    var injectionSite = ElementFactory.CreateInjectionSite(injectionSiteAst);
    ScriptElement.AddInjectionSite(injectionSite);

    AddAllParametersToInjectionSite(injectionSite);
  }

  private void AddAllParametersToInjectionSite(IInjectionSiteElement injectionSite) {
    var parameterAsts = Visitor.GetParametersForFunction(injectionSite.Ast as FunctionDefinitionAst);

    foreach (var parameterAst in parameterAsts) {
      AddParametersToInjectionSite(injectionSite, parameterAst);
    }
  }

  private void AddParametersToInjectionSite(IInjectionSiteElement injectionSite, ParameterAst parameterAst) {
    var parameter = ElementFactory.CreateInjectionSiteParameter(parameterAst);
    injectionSite.AddParameter(parameter);
  }

  private void VisitScriptAst() {
    Visitor.Visit(ScriptAst);
  }

  private void PrepareAndParseScript(string script) {
    PrependAssemblyReference(ref script);
    ScriptAst = ParseScriptToAst(script);
  }

  private ScriptBlockAst ParseScriptToAst(string script) {
    ScriptBeforeParsing = script;
    var ast = Parser.ParseInput(script, out _, out _);
    return ast;
  }

  internal static void PrependAssemblyReference(ref string script) {
    var assemblyName = GetAttributeAssemblyNamespace();
    var sb = new StringBuilder();
    sb.AppendLine($"using namespace {assemblyName};");
    sb.AppendLine(script);
    script = sb.ToString();
  }

  private static string GetAttributeAssemblyNamespace() {
    var type = typeof(InjectionSiteAttribute);
    return type.Namespace!;
  }
}