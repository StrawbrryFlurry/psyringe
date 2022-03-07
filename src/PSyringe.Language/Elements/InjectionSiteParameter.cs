using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class InjectionSiteParameterElement : IInjectionSiteParameter {
  public string Target { get; }
  
  public InjectionSiteParameterElement(ParameterAst ast) {
    Ast = ast;
    Target ??= ast.Name.VariablePath.ToString();
  }

  public Ast Ast { get; }
}