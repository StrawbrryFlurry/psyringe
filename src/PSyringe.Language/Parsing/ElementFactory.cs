using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;
using PSyringe.Core.Language.Parsing.Elements;
using PSyringe.Language.Elements;

namespace PSyringe.Language.Parsing;

public class ElementFactory {
  public static IScriptElement CreateScript(IScriptVisitor visitor) {
    return new ScriptElement(visitor);
  }
  
  public static StartupElement CreateStartup(FunctionDefinitionAst ast) {
    return new StartupElement();
  }

  public static InjectionSiteElement CreateInjectionSite(FunctionDefinitionAst ast) {
    return new InjectionSiteElement(ast);
  }

  public static void AddParameterToInjectionSite(InjectionSiteElement injectionSite, ParameterAst ast) {
    var parameter = new InjectionSiteParameterElement(ast);
    injectionSite.Parameters.Add(parameter);
  }

  public static InjectionVariableElement CreateInjectionVariable(AttributedExpressionAst ast) {
    return new InjectionVariableElement(ast);
  }

  public static InjectTemplateElement CreateInjectTemplate(AttributedExpressionAst ast) {
    return new InjectTemplateElement(ast);
  }
}