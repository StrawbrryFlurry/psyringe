using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Elements;

namespace PSyringe.Language.Parsing;

public class ElementFactory : IElementFactory {
  public IScriptElement CreateScript(ScriptBlockAst ast) {
    return new ScriptElement(ast);
  }

  public IInjectionSiteElement CreateInjectionSite(FunctionDefinitionAst functionDefinitionAst) {
    return new InjectionSiteElement(functionDefinitionAst);
  }

  public IInjectionSiteParameter CreateInjectionSiteParameter(ParameterAst parameterAst) {
    return new InjectionSiteParameterElement(parameterAst);
  }

  public IStartupFunctionElement CreateStartupFunction(FunctionDefinitionAst functionDefinitionAst) {
    return new StartupFunctionElement(functionDefinitionAst);
  }

  public IInjectVariableElement CreateInjectVariable(AttributedExpressionAst attributedExpressionAst) {
    return new InjectVariableElement(attributedExpressionAst);
  }

  public IInjectCredentialElement CreateInjectCredential(AttributedExpressionAst attributedExpressionAst) {
    return new InjectCredentialElement(attributedExpressionAst);
  }

  public IInjectDatabaseElement CreateInjectDatabase(AttributedExpressionAst attributedExpressionAst) {
    return new InjectDatabaseElement(attributedExpressionAst);
  }

  public IInjectTemplateElement CreateInjectTemplate(AttributedExpressionAst attributedExpressionAst) {
    return new InjectTemplateElement(attributedExpressionAst);
  }

  public IBeforeUnloadCallbackElement CreateBeforeUnload(FunctionDefinitionAst functionDefinitionAst) {
    return new BeforeUnloadCallbackElement(functionDefinitionAst);
  }

  public IOnLoadCallbackElement CreateOnLoad(FunctionDefinitionAst functionDefinitionAst) {
    return new OnLoadCallbackElement(functionDefinitionAst);
  }

  public IOnErrorCallbackElement CreateOnError(FunctionDefinitionAst functionDefinitionAst) {
    return new OnErrorCallbackElement(functionDefinitionAst);
  }
}