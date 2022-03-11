using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Attributes;

namespace PSyringe.Language.Elements;

public class InjectionSiteElement : IInjectionSiteElement {
  private readonly List<IInjectionSiteParameter> _parameters = new();

  public InjectionSiteElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public Type SiteDefinitionAttribute { get; set; } = typeof(StartupAttribute);

  public string Name { get; }

  public string InjectionScope { get; private set; }

  public FunctionDefinitionAst Ast { get; }

  public IEnumerable<IInjectionSiteParameter> Parameters => _parameters;

  public void AddParameter(IInjectionSiteParameter parameter) {
    _parameters.Add(parameter);
  }
}