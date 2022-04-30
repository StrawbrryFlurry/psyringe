using System.Management.Automation.Language;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class StartupFunctionElement : ScriptElement {
  private readonly List<InjectParameterElement> _parameters = new();

  public IEnumerable<InjectParameterElement> Parameters => _parameters;

  public StartupFunctionElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }

  public void AddParameter(InjectParameterElement parameterElement) {
    _parameters.Add(parameterElement);
  }


  public override Ast? TransformAst(IScriptTransformer transformer) {
    throw new NotImplementedException();
  }
}