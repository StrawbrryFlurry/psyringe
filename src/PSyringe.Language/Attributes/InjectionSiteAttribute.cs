using PSyringe.Common.Language.Attributes;

namespace PSyringe.Language.Attributes;

public class InjectionSiteAttribute : IInjectionSiteAttribute {
  internal string Scope;

  public InjectionSiteAttribute(string? Scope = null) {
    this.Scope = Scope;
  }
}