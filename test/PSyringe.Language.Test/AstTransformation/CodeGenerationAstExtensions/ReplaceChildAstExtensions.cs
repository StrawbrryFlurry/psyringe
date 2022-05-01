using System;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.CodeGenerationAstExtensions.Utils.MakeAstUtils;

namespace PSyringe.Language.Test.AstTransformation.CodeGenerationAstExtensions;

public class ReplaceChildAstExtensions {
  # region CommandExpressionAst

  [Fact]
  public void CommandExpressionAst_ReplacesChildExpression_WhenChildIsExpression() {
    var expression = StubConst();
    var replacement = StubConst();
    var sut = StubCommandExpr(expression);

    sut.ReplaceChild(expression, replacement);

    sut.Expression.Should().BeSameAs(replacement);
    replacement.Parent.Should().BeSameAs(sut);
  }

  #endregion

  #region AttributedExpressionAst

  [Fact]
  public void AttributedExpressionAst_ReplacesSelfInParent_WhenChildIsSelf() {
    var sut = StubAttrExpr();
    var replacement = StubAttrExpr();
    var parent = StubAttrExpr(null, sut);

    sut.ReplaceChild(sut, replacement);

    parent.Child.Should().BeSameAs(replacement);
    replacement.Parent.Should().BeSameAs(parent);
  }

  [Fact]
  public void AttributedExpressionAst_ReplacesAttribute_WhenChildIsAttribute() {
    var attribute = Attr<string>();
    var replacement = Attr<string>();

    var sut = StubAttrExpr(attribute);

    sut.ReplaceChild(attribute, replacement);

    sut.Attribute.Should().BeSameAs(replacement);
    replacement.Parent.Should().BeSameAs(sut);
  }

  [Fact]
  public void AttributedExpressionAst_ReplacesExpression_WhenChildIsExpression() {
    var expression = StubConst();
    var replacement = StubConst();

    var sut = StubAttrExpr(null, expression);

    sut.ReplaceChild(expression, replacement);

    sut.Child.Should().BeSameAs(replacement);
    replacement.Parent.Should().BeSameAs(sut);
  }

  #endregion

  # region StatementBlockAst

  [Fact]
  public void StatementBlockAst_ReplacesSelfInParent_WhenChildIsSelf() {
    var replacement = StubBlock(out _, out _);
    var block = StubBlock(out var statement, out _);

    var stubParent = Const("");
    block.SetParent(stubParent);

    // block.ReplaceChild(block, replacement);
  }

  [Fact]
  public void StatementBlockAst_ReplacesStatement_WhenChildIsInStatements() {
    var replacement = StubStatement();
    var block = StubBlock(out var replace, out _);

    // block.ReplaceChild(replace, replacement);

    // block.Statements.Should().NotContain(replace);
  }

  [Fact]
  public void StatementBlockAst_ReplacesTrapStatement_WhenChildIsInTraps() {
    var replacement = Const("replacement");
    var block = StubBlock(out _, out var replace);

    // block.ReplaceChild(replace, replacement);

    // block.Traps.Should().NotContain(replace);
  }

  # endregion

  # region Stubs

  private CommandExpressionAst StubCommandExpr(ExpressionAst expression) {
    return new CommandExpressionAst(EmptyExtent, expression, null);
  }

  private AttributeAst StubAttr() {
    return default;
  }

  private AttributedExpressionAst StubAttrExpr(AttributeBaseAst? attribute = null, ExpressionAst? child = null) {
    return new AttributedExpressionAst(EmptyExtent, attribute ?? Attr<string>(), child ?? Const(""));
  }

  private StatementBlockAst StubBlock(out StatementAst replaceStatement, out TrapStatementAst replaceTrapStatement) {
    replaceStatement = StubStatement();
    replaceTrapStatement = Trap(StubStatement(), StubStatement());

    var traps = new[] {
      Trap(StubStatement(), StubStatement()),
      replaceTrapStatement,
      Trap(StubStatement(), StubStatement())
    };

    var block = Block(
      traps,
      StubStatement(),
      replaceStatement,
      StubStatement()
    );

    return block;
  }

  private StatementAst StubStatement() {
    return Statement(StubConst());
  }

  private ExpressionAst StubConst() {
    var value = Guid.NewGuid().ToString();
    return Const(value);
  }

  #endregion
}