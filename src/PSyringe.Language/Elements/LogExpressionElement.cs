using System.Management.Automation.Language;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public class LogExpressionElement : ScriptElement {
  public LogExpressionElement(Ast ast) : base(ast) {
  }

  public LogExpressionElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }

  public override void TransformAst(IScriptTransformer transformer) {
    throw new NotImplementedException();
  }
}