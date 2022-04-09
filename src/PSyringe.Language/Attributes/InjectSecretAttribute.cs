using PSyringe.Language.Attributes.Base;

namespace PSyringe.Language.Attributes;

public class InjectSecretAttribute : InjectionTargetAttribute {
  public bool AsPlainText; 
  
  public InjectSecretAttribute(Type? Target, bool Optional = false) : base(Target, Optional) {
  }

  public InjectSecretAttribute(string? Target, bool Optional = false) : base(Target, Optional) {
  }
}