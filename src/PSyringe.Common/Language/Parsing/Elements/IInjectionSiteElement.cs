using PSyringe.Common.Language.Parsing.Elements.Base;

namespace PSyringe.Common.Language.Parsing.Elements;

public interface IInjectionSiteElement : IElement {
  public IEnumerable<IInjectionSiteParameter> Parameters { get; }
  public void AddParameter(IInjectionSiteParameter parameter);
}