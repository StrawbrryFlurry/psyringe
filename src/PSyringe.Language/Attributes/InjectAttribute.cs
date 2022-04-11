using PSyringe.Common.Language.Attributes;
using PSyringe.Language.DI;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

// ReSharper disable all InconsistentNaming
[PSAttributeUsage(Variable | Parameter)]
public class InjectAttribute : Attribute, IPSyringeAttribute<InjectElement> {
  public InjectAttribute(string? Target = null, bool Optional = false) {
    Provider = new ScriptProvider {
      Name = Target,
      Optional = Optional
    };
  }

  public InjectAttribute(Type Target, bool Optional = false) {
    Provider = new ScriptProvider {
      Type = Target,
      Optional = Optional
    };
  }

  public ScriptProvider Provider { get; set; }
}