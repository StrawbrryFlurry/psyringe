using System.Management.Automation.Language;
using System.Text;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Attributes;

namespace PSyringe.Language.Parsing;

public class ScriptParser : IScriptParser {
  internal readonly IElementFactory ElementFactory;

  public ScriptParser(IElementFactory elementFactory) {
    ElementFactory = elementFactory;
  }

  public IScriptElement Parse(string script, IScriptVisitor visitor) {
    var scriptBlockAst = PrepareAndParseScript(script);
    visitor.Visit(scriptBlockAst);
    
    var scriptElement = ElementFactory.CreateScript(scriptBlockAst);
    var startupFunction = GetStartupFunction(visitor);
    
    AddStartupFunctionElementToScriptIfDefined(scriptElement, startupFunction);
    AddAllInjectionSiteElementsToScript(scriptElement, visitor.InjectionSites);
    AddAllInjectElementsToScript(scriptElement, visitor.InjectExpressions);
    AddAllCallbackElementsToScript(scriptElement, visitor.CallbackFunctions);

    return scriptElement;
  }
  
  private FunctionDefinitionAst? GetStartupFunction(IScriptVisitor visitor) {
    var injectionSites = visitor.InjectionSites;
    return injectionSites.GetFunctionDefinitionWithAttribute<StartupAttribute>().FirstOrDefault();
  }
  
  private void AddStartupFunctionElementToScriptIfDefined(
    IScriptElement scriptElement,
    FunctionDefinitionAst? startupFunctionAst
  ) {
    if (startupFunctionAst is null) {
      return;
    }

    var startupFn = ElementFactory.CreateStartupFunction(startupFunctionAst);
    scriptElement.SetStartupFunction(startupFn);
  }
  
  private void AddAllInjectionSiteElementsToScript(
    IScriptElement scriptElement, 
    IEnumerable<FunctionDefinitionAst> injectionSiteAsts
  ) {
    foreach (var injectionSiteAst in injectionSiteAsts) {
      var injectionSite = ElementFactory.CreateInjectionSite(injectionSiteAst);
      scriptElement.AddInjectionSite(injectionSite);
      
      var parameterAsts = injectionSiteAst.GetParameters();
      AddAllParametersToInjectionSite(injectionSite, parameterAsts);
    }
  }

  private void AddAllParametersToInjectionSite(
    IInjectionSiteElement injectionSite,
    IEnumerable<ParameterAst> parameterAsts
  ) {
    foreach (var parameterAst in parameterAsts) {
      var parameter = ElementFactory.CreateInjectionSiteParameter(parameterAst);
      injectionSite.AddParameter(parameter);    
    }
  }
  
  private void AddAllInjectElementsToScript(
    IScriptElement scriptElement,
    IEnumerable<AttributedExpressionAst> injectExpressionAsts
    ) {
    foreach(var injectExpressionAst in injectExpressionAsts) {
      if (IsInjectVariableExpression(injectExpressionAst)) {
        var injectVariable = ElementFactory.CreateInjectVariable(injectExpressionAst);
        scriptElement.AddInjectVariable(injectVariable);
      } 
      
      else if (IsInjectCredentialExpression(injectExpressionAst)) {
        var injectCredential = ElementFactory.CreateInjectCredential(injectExpressionAst);
        scriptElement.AddInjectCredential(injectCredential);
      }
      
      else if (IsInjectTemplateExpression(injectExpressionAst)) {
        var injectTemplate = ElementFactory.CreateInjectTemplate(injectExpressionAst);
        scriptElement.AddInjectTemplate(injectTemplate);
      }
    }
  }

  private bool IsInjectVariableExpression(AttributedExpressionAst injectExpressionAst) {
    return injectExpressionAst.IsAttributedVariableExpressionOfType<InjectAttribute>();
  }
  
  private bool IsInjectCredentialExpression(AttributedExpressionAst injectExpressionAst) {
    return injectExpressionAst.IsAttributedVariableExpressionOfType<InjectCredentialAttribute>();
  }
  
  private bool IsInjectTemplateExpression(AttributedExpressionAst injectExpressionAst) {
    return injectExpressionAst.IsAttributedScriptBlockExpressionOfType<InjectTemplateAttribute>();
  }
  
  private void AddAllCallbackElementsToScript(
    IScriptElement scriptElement,
    IEnumerable<FunctionDefinitionAst> callbackFunctionAsts
  ) {
    foreach (var functionAst in callbackFunctionAsts) {
      if (IsOnErrorCallbackFunction(functionAst)) {
        var onErrorFn = ElementFactory.CreateOnError(functionAst);
        scriptElement.AddOnError(onErrorFn);
      }

      else if (IsBeforeUnloadCallbackFunction(functionAst)) {
        var beforeUnloadFn = ElementFactory.CreateBeforeUnload(functionAst);
        scriptElement.AddBeforeUnload(beforeUnloadFn);
      }
      
      else if (IsOnLoadedCallbackFunction(functionAst)) {
        var onLoadedFn = ElementFactory.CreateOnLoad(functionAst);
        scriptElement.AddOnLoad(onLoadedFn);
      }
    }
  }

  private bool IsOnErrorCallbackFunction(FunctionDefinitionAst callbackFunctionAst) {
    return callbackFunctionAst.HasAttributeOfType<OnErrorAttribute>();
  }

  private bool IsBeforeUnloadCallbackFunction(FunctionDefinitionAst callbackFunctionAst) {
    return callbackFunctionAst.HasAttributeOfType<BeforeUnloadAttribute>();
  }
  
  private bool IsOnLoadedCallbackFunction(FunctionDefinitionAst callbackFunctionAst) {
    return callbackFunctionAst.HasAttributeOfType<OnLoadedAttribute>();
  }
  
  private ScriptBlockAst PrepareAndParseScript(string script) {
    PrependAssemblyReference(ref script);
    var scriptAst = Parser.ParseInput(script, out _, out _);
    return scriptAst;
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