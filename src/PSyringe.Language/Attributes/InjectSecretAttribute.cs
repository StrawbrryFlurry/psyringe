using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

[PSAttributeUsage(Variable | Parameter)]
public class InjectSecretAttribute : Attribute, IPSyringeAttribute<InjectSecretElement> {
  public bool AsPlainText;

  public InjectSecretAttribute(Type? Target, bool Optional = false) {
  }

  public InjectSecretAttribute(string? Target, bool Optional = false) {
  }
}