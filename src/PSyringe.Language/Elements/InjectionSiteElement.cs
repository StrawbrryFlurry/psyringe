using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.Attributes;

namespace PSyringe.Language.Elements;

public class InjectionSiteElement : ScriptElement {
  private readonly List<ScriptElement> _parameters = new();

  public InjectionSiteElement(Ast ast) : base(ast) {
  }

  public Type SiteDefinitionAttribute { get; set; } = typeof(StartupAttribute);

  public string Name { get; }

  public string InjectionScope { get; private set; }

  public IEnumerable<ScriptElement> Parameters => _parameters;

  public void AddParameter(ScriptElement parameterElement) {
    _parameters.Add(parameterElement);
  }
}