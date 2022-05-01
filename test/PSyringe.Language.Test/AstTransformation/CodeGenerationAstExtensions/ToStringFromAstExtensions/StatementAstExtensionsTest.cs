using System;
using System.Management.Automation.Language;
using System.Runtime.Serialization;
using FluentAssertions;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.CodeGenerationAstExtensions.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.CodeGenerationAstExtensions.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation.CodeGenerationAstExtensions.ToStringFromAstExtensions;

public class StatementAstExtensionsTest {
  # region ErrorStatementAst

  [Fact]
  public void ToStringFromAst_ErrorStatementAst() {
    // Internal constructor
    var sut = (ErrorStatementAst) FormatterServices.GetUninitializedObject(typeof(ErrorStatementAst));
    var action = () => sut.ToStringFromAst();

    action.Should().Throw<Exception>();
  }

  #endregion

  #region UsingStatementAst

  [Fact]
  public void ToStringFromAst_UsingStatementAst() {
    var sut = new UsingStatementAst(EmptyExtent, UsingStatementKind.Namespace, Const("System.Reflection"));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"using namespace {DoubleQuote("System.Reflection")};");
  }

  #endregion

  # region DscAsts

  [Fact(Skip = "Not sure if DSC should be supported")]
  public void ToStringFromAst_ConfigurationDefinitionAst() {
    var _ = new ConfigurationDefinitionAst(EmptyExtent, null, ConfigurationType.Meta, Const(""));
    var __ = new DynamicKeywordStatementAst(EmptyExtent, List(Const("")));
  }

  #endregion

  # region DataStatementAst

  [Fact]
  public void ToStringFromAst_DataStatementAst() {
    var sut = new DataStatementAst(EmptyExtent, "Foo", null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("data Foo {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_SupportedCommand_DataStatementAst() {
    var supportedCommands = List(CmdStr("Get-Foo"));
    var sut = new DataStatementAst(EmptyExtent, "Foo", supportedCommands, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("data Foo -SupportedCommand Get-Foo {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_SupportedCommands_DataStatementAst() {
    var supportedCommands = List(CmdStr("Get-Foo"), CmdStr("Set-Foo"));
    var sut = new DataStatementAst(EmptyExtent, "Foo", supportedCommands, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("data Foo -SupportedCommand Get-Foo, Set-Foo {"
                       + NewLine +
                       "}");
  }

  #endregion

  # region TryStatementAst

  [Fact]
  public void ToStringFromAst_CatchClauseAst() {
    var sut = new CatchClauseAst(EmptyExtent, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"catch {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Type_CatchClauseAst() {
    var sut = new CatchClauseAst(EmptyExtent, List(TypeAttr<Exception>()), EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"catch {TypeS<Exception>()}{{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_MultiType_CatchClauseAst() {
    var sut = new CatchClauseAst(EmptyExtent, List(TypeAttr<Exception>(), TypeAttr<Exception>()), EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"catch {TypeS<Exception>()}{TypeS<Exception>()}{{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_TryStatementAst() {
    var sut = new TryStatementAst(EmptyExtent, EmptyBlock(), List(Catch()), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"try {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"catch {{{NewLine}" +
                       $"}}{NewLine}");
  }

  [Fact]
  public void ToStringFromAst_MultiCatch_TryStatementAst() {
    var sut = new TryStatementAst(EmptyExtent, EmptyBlock(), List(Catch(), Catch()), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"try {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"catch {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"catch {{{NewLine}" +
                       $"}}{NewLine}");
  }

  [Fact]
  public void ToStringFromAst_Finally_TryStatementAst() {
    var sut = new TryStatementAst(EmptyExtent, EmptyBlock(), List<CatchClauseAst>(), EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"try {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"finally {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_CatchFinally_TryStatementAst() {
    var sut = new TryStatementAst(EmptyExtent, EmptyBlock(), List(Catch()), EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"try {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"catch {{{NewLine}" +
                       $"}}{NewLine}" +
                       $"finally {{{NewLine}" +
                       "}");
  }

  private CatchClauseAst Catch(params TypeConstraintAst[] types) {
    return new CatchClauseAst(EmptyExtent, types, EmptyBlock());
  }

  # endregion

  #region IfStatementAst

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

  #endregion

  # region SingleLineStatements

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

  #endregion

  # region GenericLoops

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
    var sut = new ForEachStatementAst(EmptyExtent, "label", ForEachFlags.None, null, Var("i"), Pipeline(Var("arr")),
      EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($":label foreach ({VarS("i")} in {VarS("arr")}) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_ForStatementAst() {
    var initializer = Assign(Var("i"), Const(0));
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var iterator = Pipeline(Unary(Var("i"), PlusPlus));

    var sut = new ForStatementAst(EmptyExtent, null, initializer, condition, iterator, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"for ({VarS("i")} = 0; {VarS("i")} -eq 10; {VarS("i")}++) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_NoInitializer_ForStatementAst() {
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var iterator = Pipeline(Unary(Var("i"), PlusPlus));

    var sut = new ForStatementAst(EmptyExtent, null, null, condition, iterator, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"for (; {VarS("i")} -eq 10; {VarS("i")}++) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_NoCondition_ForStatementAst() {
    var initializer = Assign(Var("i"), Const(0));
    var iterator = Pipeline(Unary(Var("i"), PlusPlus));

    var sut = new ForStatementAst(EmptyExtent, null, initializer, null, iterator, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"for ({VarS("i")} = 0; ; {VarS("i")}++) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_NoIterator_ForStatementAst() {
    var initializer = Assign(Var("i"), Const(0));
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));

    var sut = new ForStatementAst(EmptyExtent, null, initializer, condition, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"for ({VarS("i")} = 0; {VarS("i")} -eq 10; ) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_ForStatementAst() {
    var sut = new ForStatementAst(EmptyExtent, "label", null, null, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($":label for (; ; ) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_DoWhileStatementAst() {
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var sut = new DoWhileStatementAst(EmptyExtent, null, condition, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"do {{{NewLine}" +
                       $"}} while ({VarS("i")} -eq 10);");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_DoWhileStatementAst() {
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var sut = new DoWhileStatementAst(EmptyExtent, "label", condition, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($":label do {{{NewLine}" +
                       $"}} while ({VarS("i")} -eq 10);");
  }

  [Fact]
  public void ToStringFromAst_WhileStatementAst() {
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var sut = new WhileStatementAst(EmptyExtent, null, condition, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"while ({VarS("i")} -eq 10) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_WhileStatementAst() {
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var sut = new WhileStatementAst(EmptyExtent, "label", condition, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($":label while ({VarS("i")} -eq 10) {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_DoUntilStatementAst() {
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var sut = new DoUntilStatementAst(EmptyExtent, null, condition, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"do {{{NewLine}" +
                       $"}} until ({VarS("i")} -eq 10);");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_DoUntilStatementAst() {
    var condition = Pipeline(Condition(Var("i"), TokenKind.Ieq, Const(10)));
    var sut = new DoUntilStatementAst(EmptyExtent, "label", condition, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($":label do {{{NewLine}" +
                       $"}} until ({VarS("i")} -eq 10);");
  }

  #endregion

  #region StatementBlockAst

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
  public void ToStringFromAst_BlockStatementAst() {
    var sut = new BlockStatementAst(EmptyExtent, Token(TokenKind.Parallel), EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"parallel {{{NewLine}" +
                       "}");
  }

  #endregion

  #region SwitchStatementAst

  [Fact]
  public void ToStringFromAst_SwitchStatementAst() {
    var condition = Pipeline(Var("true"));
    var sut = new SwitchStatementAst(EmptyExtent, null, condition, SwitchFlags.None, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"switch ({VarS("true")}) {{{NewLine}" +
                       $"default {{{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_OneFlag_SwitchStatementAst() {
    var condition = Pipeline(Var("true"));
    var sut = new SwitchStatementAst(EmptyExtent, null, condition, SwitchFlags.File, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"switch -File ({VarS("true")}) {{{NewLine}" +
                       $"default {{{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_MultiFlags_SwitchStatementAst() {
    var condition = Pipeline(Var("true"));
    var flags = SwitchFlags.File | SwitchFlags.Regex;
    var sut = new SwitchStatementAst(EmptyExtent, null, condition, flags, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"switch -File -Regex ({VarS("true")}) {{{NewLine}" +
                       $"default {{{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_OneClause_SwitchStatementAst() {
    var condition = Pipeline(Var("true"));
    var clauses = List(CaseBlock(Var("true"), Block(Const("Hi!"))));
    var sut = new SwitchStatementAst(EmptyExtent, null, condition, SwitchFlags.None, clauses, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"switch ({VarS("true")}) {{{NewLine}" +
                       $"{VarS("true")} {{{NewLine}" +
                       $"{DoubleQuote("Hi!")};{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_MultiClauses_SwitchStatementAst() {
    var condition = Pipeline(Var("true"));
    var clauses = List(
      CaseBlock(Var("true"), Block(Const("Hi!"))),
      CaseBlock(Var("false"), Block(Const("Bye!")))
    );
    var sut = new SwitchStatementAst(EmptyExtent, null, condition, SwitchFlags.None, clauses, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"switch ({VarS("true")}) {{{NewLine}" +
                       $"{VarS("true")} {{{NewLine}" +
                       $"{DoubleQuote("Hi!")};{NewLine}" +
                       $"}}{NewLine}" +
                       $"{VarS("false")} {{{NewLine}" +
                       $"{DoubleQuote("Bye!")};{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_OneClauseWithDefault_SwitchStatementAst() {
    var condition = Pipeline(Var("true"));
    var clauses = List(CaseBlock(Var("true"), Block(Const("Hi!"))));
    var sut = new SwitchStatementAst(EmptyExtent, null, condition, SwitchFlags.None, clauses, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"switch ({VarS("true")}) {{{NewLine}" +
                       $"{VarS("true")} {{{NewLine}" +
                       $"{DoubleQuote("Hi!")};{NewLine}" +
                       $"}}{NewLine}" +
                       $"default {{{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_WithLabel_SwitchStatementAst() {
    var condition = Pipeline(Var("true"));
    var sut = new SwitchStatementAst(EmptyExtent, "label", condition, SwitchFlags.None, null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($":label switch ({VarS("true")}) {{{NewLine}" +
                       $"default {{{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  private Tuple<ExpressionAst, StatementBlockAst> CaseBlock(ExpressionAst condition, StatementBlockAst block) {
    return new Tuple<ExpressionAst, StatementBlockAst>((ExpressionAst) condition.Copy(), block);
  }

  #endregion
}