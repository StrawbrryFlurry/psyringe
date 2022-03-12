namespace PSyringe.Language.TypeLoader.Parameters; 

public class PositionalParameter : IParameter {
  public Type Type { get; init; }
  public int Position { get; init; }
  public object Value { get; init; }
}