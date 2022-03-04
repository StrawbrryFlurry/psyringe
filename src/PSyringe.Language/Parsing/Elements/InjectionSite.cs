using System.Management.Automation.Language;
using PSyringe.Core.Language.Attributes;

namespace PSyringe.Core.Language.Parsing.Elements;

public class InjectionSiteElement {
  public InjectionSiteElement(FunctionDefinitionAst ast) {
    Name = ast.Name;
    FunctionDefinition = ast;
  }

  public Type SiteDefinitionAttribute { get; set; } = typeof(StartupAttribute);

  public string Name { get; }
  public string InjectionScope { get; private set; }
  public FunctionDefinitionAst FunctionDefinition { get; }

  public List<InjectionSiteParameterElement> Parameters { get; } = new();

  public void AddParameter(InjectionSiteParameterElement parameter) {
    Parameters.Add(parameter);
  }
}