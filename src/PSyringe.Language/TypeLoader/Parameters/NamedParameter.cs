namespace PSyringe.Language.TypeLoader.Parameters; 

public class NamedParameter : IParameter {
  public string Name { get; init; }
  public Type Type { get; init; }
  public object Value { get; init; }
}