using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Language.Attributes;

namespace PSyringe.Language.Elements;

public class InjectionSiteElement : IInjectionSiteElement {
  private readonly List<IInjectParameterElement> _parameters = new();

  public InjectionSiteElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public Type SiteDefinitionAttribute { get; set; } = typeof(StartupAttribute);

  public string Name { get; }

  public string InjectionScope { get; private set; }

  public Ast Ast { get; }

  public IEnumerable<IInjectParameterElement> Parameters => _parameters;

  public void AddParameter(IInjectParameterElement parameterElement) {
    _parameters.Add(parameterElement);
  }
}