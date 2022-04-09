using PSyringe.Common.DI;

namespace PSyringe.Language.DI;

public readonly struct ScriptProvider : IScriptProvider {
  public bool Optional { get; }
  public string? ProviderName { get; }
  public string? Scope { get; }
  public Type? ProviderType { get; }

  public T GetProvider<T>(IDIContainer container) {
    return container.Resolve<T>(this);
  }
}