namespace PSyringe.Language.TypeLoader.Parameters;

public class PositionalParameter : IParameter {
  public int Position { get; init; }
  public object Value { get; init; }
  public Type Type { get; init; }
}