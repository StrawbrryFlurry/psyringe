namespace PSyringe.Common.Language.Attributes;

public interface IInjectionTargetAttribute {
  public bool IsOptional { get; }
  public string? TargetProviderName { get; }
  public Type? TargetType { get; }
}