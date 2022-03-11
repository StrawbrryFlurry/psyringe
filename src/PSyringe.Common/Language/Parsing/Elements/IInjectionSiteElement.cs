using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements.Base;

namespace PSyringe.Common.Language.Parsing.Elements;

public interface IInjectionSiteElement : IElement<FunctionDefinitionAst> {
  public IEnumerable<IInjectionSiteParameter> Parameters { get; }
  public void AddParameter(IInjectionSiteParameter parameter);
}