using System.Management.Automation.Language;

namespace PSyringe.Core.Language.Parsing.Elements;

public class AttributeCollection {
  private IReadOnlyCollection<AttributeBaseAst> _attributes;

  public AttributeCollection(IReadOnlyCollection<AttributeBaseAst> attributes) {
    _attributes = attributes;
  }

  public InjectionTarget? GetInjectionTarget() {
    return null;
  }
}