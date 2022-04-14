using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class InjectConstantElement : ScriptElement {
  public InjectConstantElement(Ast ast) : base(ast) {
  }

  public InjectConstantElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }

  public override void TransformAst(IAstTransformer transformer) {
    throw new NotImplementedException();
  }
}