using System.Management.Automation.Language;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class InjectSecretElement : ScriptElement {
  public InjectSecretElement(Ast ast) : base(ast) {
  }

  public InjectSecretElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }


  public override Ast? TransformAst(IScriptTransformer transformer) {
    throw new NotImplementedException();
  }
}