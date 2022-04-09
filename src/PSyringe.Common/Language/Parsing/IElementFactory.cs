using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Common.Language.Parsing;

public interface IElementFactory {
  public IScriptElement CreateScript(ScriptBlockAst ast);
  public IInjectionSiteElement CreateInjectionSite(FunctionDefinitionAst functionDefinitionAst);
  public IInjectionSiteParameter CreateInjectionSiteParameter(ParameterAst parameterAst);
  public IStartupFunctionElement CreateStartupFunction(FunctionDefinitionAst functionDefinitionAst);
  public IInjectVariableElement CreateInjectVariable(AttributedExpressionAst attributedExpressionAst);
  public IInjectCredentialElement CreateInjectCredential(AttributedExpressionAst attributedExpressionAst);
  public IInjectDatabaseElement CreateInjectDatabase(AttributedExpressionAst attributedExpressionAst);
  public IInjectConstantElement CreateInjectConstant(AttributedExpressionAst attributedExpressionAst);

  public IInjectTemplateElement CreateInjectTemplate(AttributedExpressionAst attributedExpressionAst);
  
  public IBeforeUnloadCallbackElement CreateBeforeUnload(FunctionDefinitionAst functionDefinitionAst);
  public IOnLoadCallbackElement CreateOnLoad(FunctionDefinitionAst functionDefinitionAst);
  public IOnErrorCallbackElement CreateOnError(FunctionDefinitionAst functionDefinitionAst);
}