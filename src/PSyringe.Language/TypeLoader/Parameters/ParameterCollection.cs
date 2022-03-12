namespace PSyringe.Language.TypeLoader.Parameters; 

public class ParameterCollection {
  public IList<NamedParameter> NamedParameters { get;  }
  public IList<PositionalParameter> PositionalParameters { get; private set; }

  private List<PositionalParameter> _constructorParameters = new ();

  public ParameterCollection(
    IList<NamedParameter> namedParameters,
    IList<PositionalParameter> positionalParameters
    ) {
    NamedParameters = namedParameters;
    PositionalParameters = positionalParameters;
  }

  public IList<PositionalParameter> GetPositionalParametersWithoutNamedParameterIndexes(
    IList<int> positionsCoveredByNamedParameters
    ) {
    var sortedPositionalParameters = PositionalParameters.OrderBy(p => p.Position);
    var updatedPositionalParameters = new List<PositionalParameter>();
    var newPosition = 0;
    
    foreach(var parameter in sortedPositionalParameters) {
      while (positionsCoveredByNamedParameters.Contains(newPosition)) {
        newPosition++;
      }
      
      updatedPositionalParameters.Add(
        new PositionalParameter {
          Position = newPosition,
          Type = parameter.Type,
          Value = parameter.Value
        });
      
      newPosition++;
    }

    return updatedPositionalParameters;
  }

  public int GetParameterCount() {
    return NamedParameters.Count + PositionalParameters.Count;
  }
  
  public void AddConstructorParameter(PositionalParameter parameter) {
    _constructorParameters.Add(parameter);
  }

  public object[] GetConstructorParameters() {
    return _constructorParameters.OrderBy(p => p.Position)
                                 .Select(p => p.Value)
                                 .ToArray();
  }

  public ParameterCollection Clone() {
    return new ParameterCollection(NamedParameters, PositionalParameters);
  }
}