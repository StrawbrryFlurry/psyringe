using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Common.Language.Parsing;

public interface IElementFactory {
  public IScriptElement CreateScript();
  public IInjectionSiteElement CreateInjectionSite(FunctionDefinitionAst functionDefinitionAst);
  public IInjectionSiteParameter CreateInjectionSiteParameter(ParameterAst parameterAst);
  public IStartupElement CreateStartupFunction(FunctionDefinitionAst functionDefinitionAst);
  public IInjectVariableElement CreateInjectVariable(AttributedExpressionAst attributedExpressionAst);
  public IInjectCredentialElement CreateInjectCredential(AttributedExpressionAst attributedExpressionAst);
  public IInjectTemplateElement CreateInjectTemplate(AttributedExpressionAst attributedExpressionAst);
  public IBeforeUnloadElement CreateBeforeUnload(FunctionDefinitionAst functionDefinitionAst);
  public IOnLoadElement CreateOnLoad(FunctionDefinitionAst functionDefinitionAst);
  public IOnErrorElement CreateOnError(FunctionDefinitionAst functionDefinitionAst);
}