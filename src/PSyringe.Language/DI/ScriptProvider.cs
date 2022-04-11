using PSyringe.Common.DI;

namespace PSyringe.Language.DI;

public struct ScriptProvider : IScriptProvider {
  public bool Optional { init; get; }
  public string? Name { init; get; }
  public Type? Type { init; get; }
  public string? Scope { private set; get; }

  public T GetProvider<T>(IDIContainer container) {
    return container.Resolve<T>(this);
  }

  public string GetProviderName() {
    return Name ?? Type!.Name;
  }

  public void SetScope(string scope) {
    Scope = scope;
  }
}