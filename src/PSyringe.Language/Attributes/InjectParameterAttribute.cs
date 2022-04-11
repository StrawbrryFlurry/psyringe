using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Parameter)]
public class InjectParameterAttribute : Attribute, IPSyringeAttribute<InjectParameterElement> {
  public InjectParameterAttribute(string? targetProviderName, bool optional = false) {
  }
}