using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class StartupFunctionElement : ScriptElement {
  private readonly List<InjectParameterElement> _parameters = new();

  public StartupFunctionElement(Ast ast) : base(ast) {
  }

  public IEnumerable<InjectParameterElement> Parameters => _parameters;

  public void AddParameter(InjectParameterElement parameterElement) {
    _parameters.Add(parameterElement);
  }
}