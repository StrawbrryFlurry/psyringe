using PSyringe.Common.Language.Attributes;
using PSyringe.Language.Elements;
using static PSyringe.Common.Language.Attributes.PSAttributeTargets;

namespace PSyringe.Language.Attributes;

/// <summary>
///   TODO:
/// </summary>
[PSAttributeUsage(Variable | Parameter)]
public class InjectDatabaseAttribute : Attribute, IPSyringeAttribute<InjectDatabaseElement> {
  public bool IsProviderConnectionString { get; }

  public InjectDatabaseAttribute(string ConnectionStrting, bool IsConnectionString = false) {
    IsProviderConnectionString = IsConnectionString;
  }
}