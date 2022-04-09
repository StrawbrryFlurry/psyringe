using PSyringe.Common.Language.Attributes;

namespace PSyringe.Language.Attributes.Base;

public abstract class InjectionTargetAttribute : Attribute, IInjectionTargetAttribute {
  public InjectionTargetAttribute(Type? targetType, bool optional = false) {
    TargetType = targetType;
    IsOptional = optional;
  }

  public InjectionTargetAttribute(string? targetProviderName, bool optional = false) {
    TargetProviderName = targetProviderName;
    IsOptional = optional;
  }

  public bool IsOptional { get; }
  public string? TargetProviderName { get; }
  public Type? TargetType { get; }
}