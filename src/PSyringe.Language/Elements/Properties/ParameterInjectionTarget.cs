using PSyringe.Common.Language.Parsing.Elements.Properties;

namespace PSyringe.Language.Elements.Properties; 

public class ParameterInjectionTarget : IInjectionTarget {
  public bool HasDefaultValue() {
    throw new NotImplementedException();
  }
}