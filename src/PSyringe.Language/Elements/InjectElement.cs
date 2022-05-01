using System.Management.Automation.Language;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Elements;

public sealed class InjectElement : ScriptElement {
  public InjectElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }

  public bool HasDefaultValue() {
    return false;
  }

  private Ast? TransformVariableAssignment(AssignmentStatementAst ast) {
    var expression = ast.Left;
    return expression;
  }

  private Ast? TransformAttributedExpression(AttributedExpressionAst ast) {
    return ast.Child;
  }

  public override void TransformAst(IScriptTransformer transformer) {
    if (IsAst<AttributedExpressionAst>(out var attributedExpressionAst)) {
    }
  }
}