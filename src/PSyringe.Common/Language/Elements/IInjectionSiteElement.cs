using PSyringe.Common.Language.Elements;
using PSyringe.Common.Language.Parsing.Elements.Base;

namespace PSyringe.Common.Language.Parsing.Elements;

public interface IInjectionSiteElement : IFunctionElement {
  public IEnumerable<IInjectParameterElement> Parameters { get; }
  public void AddParameter(IInjectParameterElement parameterElement);
}