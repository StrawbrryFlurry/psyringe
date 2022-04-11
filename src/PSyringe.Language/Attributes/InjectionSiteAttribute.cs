using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Function)]
public class InjectionSiteAttribute : Attribute, IPSyringeAttribute<InjectionSiteElement> {
  internal string Scope;

  public InjectionSiteAttribute(string? Scope = null) {
    this.Scope = Scope;
  }
}