using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.AstTransformation;

namespace PSyringe.Language.Elements;

public sealed class InjectElement : ScriptElement {
  public InjectElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }

  public bool HasDefaultValue() {
    return false;
  }

  public override Ast? TransformAst<T>(T source) {
    ScriptTransformer x;
    // x.AddDependency(this.Dependency);
    if (IsAst<AttributedExpressionAst>(out var attributedExpressionAst)) {
      var ast = (AttributedExpressionAst) Ast;
    }

    return default;
  }

  private Ast? TransformVariableAssignment(AssignmentStatementAst ast) {
    var expression = ast.Left;
    return expression;
  }

  private Ast? TransformAttributedExpression(AttributedExpressionAst ast) {
    return ast.Child;
  }
}