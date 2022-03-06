using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements.Properties;
using PSyringe.Core.Language.Parsing.Elements;

namespace PSyringe.Language.Elements.Properties;

public class AttributeCollection : IAttributeCollection {
  private IReadOnlyCollection<AttributeBaseAst> _attributes;

  public AttributeCollection(IReadOnlyCollection<AttributeBaseAst> attributes) {
    _attributes = attributes;
  }

  public InjectionTarget? GetInjectionTarget() {
    return null;
  }
}