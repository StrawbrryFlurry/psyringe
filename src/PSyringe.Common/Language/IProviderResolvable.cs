namespace PSyringe.Common.Language;

public interface IProviderResolvable {
  public string? Name { get; }
  public Type? Type { get; }

  public bool IsOptional();
  public string GetScope();

  public string ToString();
}