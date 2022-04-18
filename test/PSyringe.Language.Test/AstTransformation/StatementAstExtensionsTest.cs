using System;
using System.Management.Automation.Language;
using System.Runtime.Serialization;
using FluentAssertions;
using PSyringe.Language.AstTransformation;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation;

public class StatementAstExtensionsTest {
  [Fact]
  public void ToStringFromAst_ErrorStatementAst() {
    // Internal constructor
    var sut = (ErrorStatementAst) FormatterServices.GetUninitializedObject(typeof(ErrorStatementAst));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("");
  }

  [Fact]
  public void ToStringFromAst_TrapStatementAst() {
    var sut = new TrapStatementAst(EmptyExtent, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"trap {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithAttribute_TrapStatementAst() {
    var sut = new TrapStatementAst(EmptyExtent, TypeAttr<Exception>(), EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"trap {TypeS<Exception>()}{{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_StatementBlockAst() {
    var sut = new StatementBlockAst(EmptyExtent, null, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_SingleStatement_StatementBlockAst() {
    var sut = new StatementBlockAst(EmptyExtent, List(Statement(Const("Hi!"))), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{{{NewLine}" +
                       $"{DoubleQuote("Hi!")};{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_MultiStatements_StatementBlockAst() {
    var sut = new StatementBlockAst(EmptyExtent, List(Statement(Const("Hi!")), Statement(Const("Bye!"))), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{{{NewLine}" +
                       $"{DoubleQuote("Hi!")};{NewLine}" +
                       $"{DoubleQuote("Bye!")};{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithTrap_StatementBlockAst() {
    var sut = new StatementBlockAst(EmptyExtent, null, List(Trap(Statement(Const("Hi!")))));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{{{NewLine}" +
                       $"trap {{{NewLine}" +
                       $"{DoubleQuote("Hi!")};{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithMultiTrap_StatementBlockAst() {
    var sut = new StatementBlockAst(
      EmptyExtent,
      null,
      List(
        Trap(Statement(Const("Hi!"))),
        Trap(Statement(Const("Bye!")))
      )
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{{{NewLine}" +
                       $"trap {{{NewLine}" +
                       $"{DoubleQuote("Hi!")};{NewLine}" +
                       $"}}{NewLine}" +
                       NewLine +
                       $"trap {{{NewLine}" +
                       $"{DoubleQuote("Bye!")};{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_DataStatementAst() {
    // var sut = new DataStatementAst();
  }

  [Fact]
  public void ToStringFromAst_IfStatementAst() {
    var condition = Pipeline(Command(Condition(Const(1), TokenKind.Ieq, Const(1))));
    var ifStatement = List(IfBlock(condition, EmptyBlock()));
    var sut = new IfStatementAst(EmptyExtent, ifStatement, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"if (1 -eq 1) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithElseIf_IfStatementAst() {
    var condition = Pipeline(Command(Condition(Const(1), TokenKind.Ieq, Const(1))));
    var ifStatement = List(IfBlock(condition, EmptyBlock()), IfBlock(condition, EmptyBlock()));
    var sut = new IfStatementAst(EmptyExtent, ifStatement, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"if (1 -eq 1) {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"elseif (1 -eq 1) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithElse_IfStatementAst() {
    var condition = Pipeline(Command(Condition(Const(1), TokenKind.Ieq, Const(1))));
    var ifStatement = List(IfBlock(condition, EmptyBlock()));
    var sut = new IfStatementAst(EmptyExtent, ifStatement, Block(Const("End!")));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"if (1 -eq 1) {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"else {{{NewLine}" +
                       $"{DoubleQuote("End!")};{NewLine}" +
                       "}");
  }

  private Tuple<PipelineBaseAst, StatementBlockAst> IfBlock(PipelineBaseAst condition, StatementBlockAst block) {
    return new Tuple<PipelineBaseAst, StatementBlockAst>((PipelineBaseAst) condition.Copy(), block);
  }

  [Fact]
  public void ToStringFromAst_ThrowStatementAst() {
    var sut = new ThrowStatementAst(EmptyExtent, Pipeline(Const("Boo!")));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"throw {DoubleQuote("Boo!")};");
  }

  [Fact]
  public void ToStringFromAst_ExitStatementAst() {
    var sut = new ExitStatementAst(EmptyExtent, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("exit;");
  }

  [Fact]
  public void ToStringFromAst_WithExpression_ExitStatementAst() {
    var sut = new ExitStatementAst(EmptyExtent, Pipeline(Const(0)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("exit 0;");
  }

  [Fact]
  public void ToStringFromAst_ReturnStatementAst() {
    var sut = new ReturnStatementAst(EmptyExtent, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("return;");
  }

  [Fact]
  public void ToStringFromAst_WithExpression_ReturnStatementAst() {
    var sut = new ReturnStatementAst(EmptyExtent, Pipeline(Const(0)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("return 0;");
  }


  [Fact]
  public void ToStringFromAst_ContinueStatementAst() {
    var sut = new ContinueStatementAst(EmptyExtent, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("continue;");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_ContinueStatementAst() {
    var sut = new ContinueStatementAst(EmptyExtent, CmdStr("label"));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("continue label;");
  }

  [Fact]
  public void ToStringFromAst_BreakStatementAst() {
    var sut = new BreakStatementAst(EmptyExtent, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("break;");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_BreakStatementAst() {
    var sut = new BreakStatementAst(EmptyExtent, CmdStr("label"));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("break label;");
  }

  [Fact]
  public void ToStringFromAst_ForEachStatementAst() {
    var sut = new ForEachStatementAst(EmptyExtent, null, ForEachFlags.None, null, Var("i"), Pipeline(Var("arr")),
      EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"foreach ({VarS("i")} in {VarS("arr")}) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_ForEachStatementAst() {
    var sut = new ForEachStatementAst(EmptyExtent, "Label", ForEachFlags.None, null, Var("i"), Pipeline(Var("arr")),
      EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($":Label foreach ({VarS("i")} in {VarS("arr")}) {{{NewLine}" +
                       "}");
  }
}