using PSyringe.Common.DI;

namespace PSyringe.Common.Language.Compiler;

/// <summary>
///   Contains information on a provider
///   that will be injected into a script at runtime.
/// </summary>
public interface IScriptVariableDependency {
  /// <summary>
  ///   The provider that the framework will inject into the variable.
  /// </summary>
  public IScriptProvider Provider { get; }

  /// <summary>
  ///   The name of the variable that the provider will be injected into.
  /// </summary>
  public string VariableName { get; }

  public bool Optional { get; }
}