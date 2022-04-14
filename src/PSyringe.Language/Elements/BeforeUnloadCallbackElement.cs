using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class BeforeUnloadCallbackElement : ScriptElement {
  public BeforeUnloadCallbackElement(Ast ast) : base(ast) {
  }

  public BeforeUnloadCallbackElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }

  public override void TransformAst(IAstTransformer transformer) {
    throw new NotImplementedException();
  }
}