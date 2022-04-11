using PSyringe.Common.Language.Elements.Base;

namespace PSyringe.Common.Language.Elements;

public interface IInjectionSiteElement : IFunctionElement {
  public IEnumerable<IInjectParameterElement> Parameters { get; }
  public void AddParameter(IInjectParameterElement parameterElement);
}