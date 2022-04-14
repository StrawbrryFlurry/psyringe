using System.Management.Automation;
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
  ///   The variable that the provider will be injected into.
  /// </summary>
  public PSVariable Variable { get; }

  public bool Optional { get; }
}