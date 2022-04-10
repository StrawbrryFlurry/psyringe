using PSyringe.Common.DI;

namespace PSyringe.Language.DI;

public readonly struct ScriptProvider : IScriptProvider {
  public bool Optional { init; get; }
  public string? Name { init; get; }
  public Type? Type { init; get; }
  public string? Scope { init; get; }

  public T GetProvider<T>(IDIContainer container) {
    return container.Resolve<T>(this);
  }

  public string GetProviderName() {
    return Name ?? Type!.Name;
  }
}