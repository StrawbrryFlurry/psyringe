namespace PSyringe.Language.Attributes;

/// <summary>
///   TODO:
/// </summary>
public class InjectDatabaseAttribute : InjectionTargetAttribute {
  public InjectDatabaseAttribute(string Target) {
  }

  public InjectDatabaseAttribute(string ConnectionStrting, bool IsConnectionString = false) {
  }
}