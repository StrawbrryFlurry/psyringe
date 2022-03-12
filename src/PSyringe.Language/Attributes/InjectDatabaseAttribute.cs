namespace PSyringe.Language.Attributes;

/// <summary>
///   TODO:
/// </summary>
public class InjectDatabaseAttribute : InjectionTargetAttribute {
  public bool IsProviderConnectionString { get; private set; }

  public InjectDatabaseAttribute(string ConnectionStrting, bool IsConnectionString = false) : base(ConnectionStrting) {
    IsProviderConnectionString = IsConnectionString;
  }
}