using PSyringe.Common.Language.Attributes;

namespace PSyringe.Language.Attributes;

public abstract class InjectionTargetAttribute : Attribute, IInjectionTargetAttribute {
  public readonly Type? TargetType;
  public readonly string? TargetProviderName;

  public readonly bool IsOptional;
  
  public InjectionTargetAttribute(Type? targetType, bool optional = false) {
    TargetType = targetType;
    IsOptional = optional;
  }

  public InjectionTargetAttribute(string? targetProviderName, bool optional = false) {
    TargetProviderName = targetProviderName;
    IsOptional = optional;
  }
}