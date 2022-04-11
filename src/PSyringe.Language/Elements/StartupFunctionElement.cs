using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;
using PSyringe.Common.Language.Parsing.Elements;

namespace PSyringe.Language.Elements;

public class StartupFunctionElement : IStartupFunctionElement {
  private readonly List<IInjectParameterElement> _parameters = new();

  public StartupFunctionElement(FunctionDefinitionAst ast) {
    Ast = ast;
  }

  public IEnumerable<IInjectParameterElement> Parameters => _parameters;

  public FunctionDefinitionAst Ast { get; }

  public void AddParameter(IInjectParameterElement parameterElement) {
    _parameters.Add(parameterElement);
  }
}