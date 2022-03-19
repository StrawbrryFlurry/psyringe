using PSyringe.Language.Attributes.Base;

namespace PSyringe.Language.Attributes;

public class InjectCredentialAttribute : InjectionTargetAttribute {
  public InjectCredentialAttribute(Type? Target, bool Optional = false) : base(Target, Optional) {
  }

  public InjectCredentialAttribute(string? Target, bool Optional = false) : base(Target, Optional) {
  }
}