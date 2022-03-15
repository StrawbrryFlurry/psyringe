using PSyringe.Common.Language.Attributes;

namespace PSyringe.Language.Attributes;

public abstract class InjectionTargetAttribute : Attribute, IInjectionTargetAttribute {
  public readonly bool IsOptional;
  public readonly string? TargetProviderName;
  public readonly Type? TargetType;

  public InjectionTargetAttribute(Type? targetType, bool optional = false) {
    TargetType = targetType;
    IsOptional = optional;
  }

  public InjectionTargetAttribute(string? targetProviderName, bool optional = false) {
    TargetProviderName = targetProviderName;
    IsOptional = optional;
  }
}