using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

/// <summary>
///   AttributedExpressionChild is either a VariableExpressionAst
///   or a VariableAssignmentExpression
/// </summary>
/// <param name="ast"></param>
public class InjectElement : ScriptElement {
  public InjectElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }


  public bool HasDefaultValue() {
    return false;
  }

  public override void TransformAst(IAstTransformer transformer) {
    throw new NotImplementedException();
  }
}