using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

/// <summary>
///   Injects a constant value from the script environment into a variable.
/// </summary>
[PSAttributeUsage(Variable | Parameter)]
public class InjectConstantAttribute : Attribute, IPSyringeAttribute<InjectConstantElement> {
  public InjectConstantAttribute(string? Target, bool Optional = false) {
  }
}