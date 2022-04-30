using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

// ReSharper disable all InconsistentNaming
[PSAttributeUsage(Variable | Parameter)]
public class InjectAttribute : Attribute, IPSyringeAttribute<InjectElement> {
  public Prov Provider { get; set; }

  public InjectAttribute(string? Target = null, bool Optional = false) {
    Provider = new Prov {
      Name = Target,
      Optional = Optional
    };
  }

  public InjectAttribute(Type Target, bool Optional = false) {
    Provider = new Prov {
      Type = Target,
      Optional = Optional
    };
  }

  public class Prov {
    public string Name { get; init; }
    public Type Type { get; init; }
    public bool Optional { get; init; }
  }
}