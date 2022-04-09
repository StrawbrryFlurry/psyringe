namespace PSyringe.Common.DI;

public interface IScriptProvider {
  public bool Optional { get; }
  public string? ProviderName { get; }
  public string? Scope { get; }
  public Type? ProviderType { get; }

  public T GetProvider<T>(IDIContainer container);
}