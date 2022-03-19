using PSyringe.Language.Attributes.Base;

namespace PSyringe.Language.Attributes;

/// <summary>
///   TODO:
/// </summary>
public class InjectDatabaseAttribute : InjectionTargetAttribute {
  public InjectDatabaseAttribute(string ConnectionStrting, bool IsConnectionString = false) : base(ConnectionStrting) {
    IsProviderConnectionString = IsConnectionString;
  }

  public bool IsProviderConnectionString { get; }
}