using PSyringe.Language.Attributes.Base;

namespace PSyringe.Language.Attributes;

public class InjectAttribute : InjectionTargetAttribute {
  public InjectAttribute(string? Target = null, bool Optional = false) : base(Target, Optional) {
  }

  public InjectAttribute(Type Target, bool Optional = false) : base(Target, Optional) {
  }
}