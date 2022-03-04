using PSyringe.Common.Language.Attributes;

namespace PSyringe.Core.Language.Attributes;

public class InjectionSiteAttribute : IInjectionSiteAttribute {
  internal string Scope;

  public InjectionSiteAttribute(string? Scope = null) {
    this.Scope = Scope;
  }
}