using System.Management.Automation.Language;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Elements;

public sealed class InjectElement : ScriptElement {
  public InjectElement(Ast ast, AttributeAst attribute) : base(ast, attribute) {
  }

  public bool HasDefaultValue() {
    return false;
  }

  private void TransformVariableAssignment(AssignmentStatementAst ast) {
    var expression = ast.Left;
  }

  private void TransformAttributedExpression(AttributedExpressionAst ast) {
  }

  private void TransformParameter(AttributedExpressionAst ast) {
  }

  public override void TransformAst(IScriptTransformer transformer) {
    if (IsAst<AttributedExpressionAst>(out var attributedExpression)) {
      attributedExpression.ReplaceChild(attributedExpression, attributedExpression.Child);

      if (attributedExpression.Child is VariableExpressionAst variableExpression) {
        var providerVar = transformer.AddProvider(variableExpression.GetName(), new ProviderResolvable(""));
        var expression = GetAssignmentStatementChild();

        expression.SetParent();
        var attribute = GetAttributeParent();

        var assignmentStatement = new AssignmentStatementAst(
          Ast.Extent,
          expression,
          TokenKind.Equals,
          providerVar.ToStatement(),
          Ast.Extent
        );

        attribute.ReplaceChild(attribute, assignmentStatement);
      }
    }
  }

  private ExpressionAst GetAssignmentStatementChild() {
    if (Ast.Parent is not AttributedExpressionAst) {
      return ((AttributedExpressionAst) Ast).Child;
    }

    return (AttributedExpressionAst) Ast.Parent;
  }

  private Ast GetAttributeParent() {
    var parent = Ast;
    while (parent.Parent is AttributedExpressionAst) {
      parent = parent.Parent;
    }

    if (parent.Is(Ast)) {
      return parent.Parent;
    }

    return parent;
  }
}