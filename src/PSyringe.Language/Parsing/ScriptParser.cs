using System.Management.Automation.Language;
using System.Text;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Attributes;
using PSyringe.Language.Extensions;

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

    return injectionSites?.FirstOrDefault(
      site => site.HasAttributeOfType<StartupAttribute>()
    );
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
    foreach (var expressionAst in injectExpressionAsts) {
      if (IsVariableExpressionWithAttribute<InjectAttribute>(expressionAst)) {
        var injectVariable = ElementFactory.CreateInjectVariable(expressionAst);
        scriptElement.AddInjectVariable(injectVariable);
      }
      else if (IsVariableExpressionWithAttribute<InjectCredentialAttribute>(expressionAst)) {
        var injectCredential = ElementFactory.CreateInjectCredential(expressionAst);
        scriptElement.AddInjectCredential(injectCredential);
      }
      else if (IsVariableExpressionWithAttribute<InjectDatabaseAttribute>(expressionAst)) {
        var injectDatabase = ElementFactory.CreateInjectDatabase(expressionAst);
        scriptElement.AddInjectDatabase(injectDatabase);
      }
      else if (IsScriptBlockExpressionWithAttribute<InjectTemplateAttribute>(expressionAst)) {
        var injectTemplate = ElementFactory.CreateInjectTemplate(expressionAst);
        scriptElement.AddInjectTemplate(injectTemplate);
      }
    }
  }

  // Variable Expressions [InjectX()]$Variable;
  private bool IsVariableExpressionWithAttribute<T>(AttributedExpressionAst expressionAst) where T : Attribute {
    var isVariableExpression = expressionAst.Child is VariableExpressionAst;
    return isVariableExpression && expressionAst.Attribute.IsOfExactType<T>();
  }

  // ScriptBlock Expressions [InjectX()]{ ... };
  private bool IsScriptBlockExpressionWithAttribute<T>(AttributedExpressionAst expressionAst) where T : Attribute {
    var isScriptBlockExpression = expressionAst.Child is ScriptBlockExpressionAst;
    return isScriptBlockExpression && expressionAst.Attribute.IsOfExactType<T>();
  }

  private void AddAllCallbackElementsToScript(
    IScriptElement scriptElement,
    IEnumerable<FunctionDefinitionAst> callbackFunctionAsts
  ) {
    foreach (var functionAst in callbackFunctionAsts) {
      if (IsCallbackFunctionWithAttribute<OnErrorAttribute>(functionAst)) {
        var onErrorFn = ElementFactory.CreateOnError(functionAst);
        scriptElement.AddOnErrorFunction(onErrorFn);
      }

      else if (IsCallbackFunctionWithAttribute<BeforeUnloadAttribute>(functionAst)) {
        var beforeUnloadFn = ElementFactory.CreateBeforeUnload(functionAst);
        scriptElement.AddBeforeUnloadFunction(beforeUnloadFn);
      }

      else if (IsCallbackFunctionWithAttribute<OnLoadedAttribute>(functionAst)) {
        var onLoadedFn = ElementFactory.CreateOnLoad(functionAst);
        scriptElement.AddOnLoadFunction(onLoadedFn);
      }
    }
  }

  private bool IsCallbackFunctionWithAttribute<T>(FunctionDefinitionAst ast) where T : Attribute {
    return ast.HasAttributeOfType<T>();
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