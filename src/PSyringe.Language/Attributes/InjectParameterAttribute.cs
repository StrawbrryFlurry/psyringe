namespace PSyringe.Language.Attributes;

public class InjectParameterAttribute : InjectionTargetAttribute {
  public InjectParameterAttribute(string? targetProviderName, bool optional = false) : base(targetProviderName,
    optional) {
  }
}