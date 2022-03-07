using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class StartupFunctionElement : IStartupFunctionElement {
  private readonly List<IInjectionSiteParameter> _parameters = new();

  public StartupFunctionElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public Ast Ast { get; }
  public IEnumerable<IInjectionSiteParameter> Parameters => _parameters;

  public void AddParameter(IInjectionSiteParameter parameter) {
    _parameters.Add(parameter);
  }
}