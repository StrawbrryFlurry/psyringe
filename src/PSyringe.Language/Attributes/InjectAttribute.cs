using PSyringe.Common.Language.Attributes;

namespace PSyringe.Core.Language.Attributes;

public class InjectAttribute : IInjectionTargetAttribute {
  /// <summary>
  /// </summary>
  /// <param name="Target"></param>
  /// <param name="Optional">Optional is automatically set to true if the target has a default value</param>
  public InjectAttribute(string? Target = null, bool Optional = false) {
    this.Target = Target;
    this.Optional = Optional;
  }

  public string? Target { get; set; }
  public bool Optional { get; set; }
}