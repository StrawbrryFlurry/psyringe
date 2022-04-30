using System.Management.Automation.Language;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class InjectConstantElement : ScriptElement {
  public InjectConstantElement(Ast ast) : base(ast) {
  }

  public InjectConstantElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }


  public override Ast? TransformAst(IScriptTransformer transformer) {
    throw new NotImplementedException();
  }
}