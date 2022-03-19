using PSyringe.Language.Attributes.Base;

namespace PSyringe.Language.Attributes;

public class InjectTemplateAttribute : InjectionTargetAttribute {
  public InjectTemplateAttribute(string? targetProviderName, bool optional = false) :
    base(targetProviderName, optional) {
  }
}