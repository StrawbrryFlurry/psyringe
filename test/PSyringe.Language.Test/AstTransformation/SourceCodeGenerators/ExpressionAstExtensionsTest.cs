using System;
using System.Collections.Generic;
using System.Management.Automation.Language;
using System.Runtime.Serialization;
using FluentAssertions;
using PSyringe.Language.CodeGen.SourceCodeGenerators;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation.SourceCodeGenerators;

public class ExpressionAstExtensionsTest {
  [Fact]
  public void ToStringFromAst_TernaryExpression() {
    var sut = new TernaryExpressionAst(EmptyExtent, Var(False), Var(True), Var(False));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(TernaryExpression(VarS(False), VarS(True), VarS(False)));
  }

  [Fact]
  public void ToStringFromAst_BinaryExpressionAst() {
    var sut = new BinaryExpressionAst(EmptyExtent, Const(One), TokenKind.Plus, Const(One),
      EmptyExtent);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(BinaryExpression(One, TokenKind.Plus.Text(), One));
  }

  [Fact]
  public void ToStringFromAst_DoesntQuoteTheValue_ConstantExpressionAstWithNumber() {
    var sut = new ConstantExpressionAst(EmptyExtent, One);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(One.ToString());
  }

  [Fact]
  public void ToStringFromAst_ReturnsTrueVariable_ConstantExpressionAstWithTrue() {
    var sut = new ConstantExpressionAst(EmptyExtent, true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(VarS(True));
  }

  [Fact]
  public void ToStringFromAst_ConstantExpressionAstWithString() {
    var sut = CmdStr("foo");
    var actual = sut.ToStringFromAst();

    actual.Should().Be("foo");
  }

  [Fact]
  public void ToStringFromAst_ReturnsUnquotedString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.BareWord);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(ConstantString);
  }

  [Fact]
  public void ToStringFromAst_ReturnsDoubleQuotedString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.DoubleQuoted);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(DoubleQuote(ConstantString));
  }

  [Fact]
  public void ToStringFromAst_ReturnsSingleQuotedString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.SingleQuoted);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(SingleQuote(ConstantString));
  }

  [Fact]
  public void ToStringFromAst_ReturnsDoubleQuotedHereString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.DoubleQuotedHereString);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"@\"{NewLine}{ConstantString}{NewLine}\"@");
  }

  [Fact]
  public void ToStringFromAst_ReturnsSingleQuotedHereString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.SingleQuotedHereString);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"@'{NewLine}{ConstantString}{NewLine}'@");
  }


  [Fact]
  public void ToStringFromAst_EscapedStringSingle_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, "It's me!", StringConstantType.SingleQuoted);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("'It''s me!'");
  }

  [Fact]
  public void ToStringFromAst_EscapedStringDouble_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, "It\"s me!", StringConstantType.DoubleQuoted);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(DoubleQuote("It\"\"s me!"));
  }

  [Fact]
  public void ToStringFromAst_NotEscapeInSubExpression_ExpandableStringExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, "Foo$(\"Some-Command\")Bar",
      StringConstantType.DoubleQuoted);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(DoubleQuote("Foo$(\"Some-Command\")Bar"));
  }

  [Fact]
  public void ToStringFromAst_NotEscapeInMultiSubExpression_ExpandableStringExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, "Foo$(\"Some-Command\")\"$(\"Some-Other\")Bar",
      StringConstantType.DoubleQuoted);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(DoubleQuote("Foo$(\"Some-Command\")\"\"$(\"Some-Other\")Bar"));
  }

  [Fact]
  // Uses the same implementation as StringConstantExpressionAst
  public void ToStringFromAst_ExpandableStringExpressionAst() {
    var sut = new ExpandableStringExpressionAst(EmptyExtent, ConstantString, StringConstantType.BareWord);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(ConstantString);
  }

  [Fact]
  public void ToStringFromAst_VariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, VariableName, false);

    var actual = variableExpression.ToStringFromAst();

    actual.Should().Be(VarS(VariableName));
  }

  [Fact]
  public void ToStringFromAst_ScopedVariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, $"script:{VariableName}", false);

    var actual = variableExpression.ToStringFromAst();

    actual.Should().Be(VarS(VariableName, "script"));
  }

  [Fact]
  public void ToStringFromAst_SplattedVariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, $"{VariableName}", true);
    var actual = variableExpression.ToStringFromAst();

    actual.Should().Be(VarS(VariableName, true));
  }

  [Fact]
  public void ToStringFromAst_SplattedScopedVariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, $"script:{VariableName}", true);

    var actual = variableExpression.ToStringFromAst();

    actual.Should().Be(VarS(VariableName, "script", true));
  }

  [Fact]
  public void ToStringFromAst_ArrayLiteralAst() {
    var sut = new ArrayLiteralAst(EmptyExtent, ExprList(Const(One)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{One}");
  }

  [Fact]
  public void ToStringFromAst_SeparatesElementsByComma_ArrayLiteralAst() {
    var sut = new ArrayLiteralAst(EmptyExtent, ExprList(Const(One), Const(Two)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{One}, {Two}");
  }

  [Fact]
  public void ToStringFromAst_UsingExpressionAst() {
    var sut = new UsingExpressionAst(EmptyExtent, Var(VariableName));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(UsingExpression(VariableName));
  }

  [Fact]
  public void ToStringFromAst_IndexExpressionAst() {
    var sut = new IndexExpressionAst(EmptyExtent, Var(VariableName), Const(One));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(IndexExpression(VarS(VariableName), One));
  }

  [Fact]
  public void ToStringFromAst_NullConditional_IndexExpressionAst() {
    var sut = new IndexExpressionAst(EmptyExtent, Var(VariableName), Const(One), true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(IndexExpression(VarS(VariableName), One, true));
  }

  [Fact]
  public void ToStringFromAst_TypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, MakeTypeName<string>());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.String]");
  }

  [Fact]
  public void ToStringFromAst_ReflectionTypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, MakeReflectionTypeName<string>());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[string]");
  }

  [Fact]
  public void ToStringFromAst_ArrayTypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, MakeArrayTypeName<string>());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.String[]]");
  }

  [Fact]
  public void ToStringFromAst_ArrayTypeNameDepthTwo_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, MakeArrayTypeName<string>(2));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.String[][]]");
  }

  [Fact]
  public void ToStringFromAst_GenericTypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, MakeGenericTypeName<List<string>>());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.Collections.Generic.List[System.String]]");
  }

  [Fact]
  public void ToStringFromAst_GenericTypeNameTwoArguments_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, MakeGenericTypeName<Dictionary<string, string>>());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.Collections.Generic.Dictionary[System.String,System.String]]");
  }

  [Fact]
  public void ToStringFromAst_UnaryIncrement_UnaryExpressionAst() {
    var sut = new UnaryExpressionAst(EmptyExtent, TokenKind.PlusPlus, Var(VariableName));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS(VariableName)}++");
  }

  [Fact]
  public void ToStringFromAst_NotOperator_UnaryExpressionAst() {
    var sut = new UnaryExpressionAst(EmptyExtent, TokenKind.Not, Var(VariableName));
    var actual = sut.ToStringFromAst();


    actual.Should().Be($"-not{VarS(VariableName)}");
  }

  [Fact]
  public void ToStringFromAst_NotExclamationMark_UnaryExpressionAst() {
    var sut = new UnaryExpressionAst(EmptyExtent, TokenKind.Exclaim, Var(VariableName));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"!{VarS(VariableName)}");
  }

  [Fact]
  public void ToStringFromAst_ReturnsEmpty_ErrorExpressionAst() {
    // Internal constructor
    var sut = (ErrorExpressionAst) FormatterServices.GetUninitializedObject(typeof(ErrorExpressionAst));
    var action = () => sut.ToStringFromAst();

    action.Should().Throw<Exception>();
  }

  [Fact]
  public void ToStringFromAst_AttributedExpressionAst() {
    var sut = new AttributedExpressionAst(EmptyExtent, TypeAttr<string>(), Var(VariableName));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[string]{VarS(VariableName)}");
  }

  [Fact]
  public void ToStringFromAst_ConvertExpressionAst() {
    var sut = new ConvertExpressionAst(EmptyExtent, TypeAttr<string>(), Var(VariableName));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[string]{VarS(VariableName)}");
  }

  [Fact]
  public void ToStringFromAst_StringCast_ConvertExpressionAst() {
    var sut = new ConvertExpressionAst(EmptyExtent, TypeAttr<string>(), Const(ConstantString));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[string]{DoubleQuote(ConstantString)}");
  }

  [Fact]
  public void ToStringFromAst_CommandExpressionAst() {
    var sut = new CommandExpressionAst(EmptyExtent, CmdStr("Command"), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Command");
  }

  [Fact]
  public void ToStringFromAst_WithRedirection_CommandExpressionAst() {
    var redirections = List(new FileRedirectionAst(EmptyExtent, RedirectionStream.Output, Const("File"), false));
    var sut = new CommandExpressionAst(EmptyExtent, CmdStr("Command"), redirections);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(@"Command > ""File""");
  }

  [Fact]
  public void ToStringFromAst_HashtableAst() {
    var sut = new HashtableAst(EmptyExtent, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"@{{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Element_HashtableAst() {
    var elements = List(
      Tuple<ExpressionAst, StatementAst>(CmdStr("Prop"), Statement(Const("Value")))
    );
    var sut = new HashtableAst(EmptyExtent, elements);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"@{{{NewLine}" +
                       $"Prop = {DoubleQuote("Value")};{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_MultiElements_HashtableAst() {
    var elements = List(
      Tuple<ExpressionAst, StatementAst>(CmdStr("Prop1"), Statement(Const("Value"))),
      Tuple<ExpressionAst, StatementAst>(CmdStr("Prop2"), Statement(Const("Value")))
    );
    var sut = new HashtableAst(EmptyExtent, elements);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"@{{{NewLine}" +
                       $"Prop1 = {DoubleQuote("Value")};{NewLine}" +
                       $"Prop2 = {DoubleQuote("Value")};{NewLine}" +
                       "}");
  }

  #region ScriptBlockExpressionAst

  [Fact]
  public void ToStringFromAst_ScriptBlockExpressionAst() {
    var sut = new ScriptBlockExpressionAst(EmptyExtent, new ScriptBlockAst(EmptyExtent, null, EmptyBlock(), false));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("{"
                       + NewLine +
                       "}");
  }

  #endregion

  #region ArrayExpressionAst

  [Fact]
  public void ToStringFromAst_ArrayExpressionAst() {
    var sut = new ArrayExpressionAst(EmptyExtent, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("@()");
  }

  [Fact]
  public void ToStringFromAst_OneElement_ArrayExpressionAst() {
    var sut = new ArrayExpressionAst(EmptyExtent, Block(Const("Foo")));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"@({DoubleQuote("Foo")})");
  }

  [Fact]
  public void ToStringFromAst_TwoElements_ArrayExpressionAst() {
    var sut = new ArrayExpressionAst(EmptyExtent, Block(Const("Foo"), Const("Bar")));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"@({DoubleQuote("Foo")}, {DoubleQuote("Bar")})");
  }

  #endregion

  #region SubExpressionAst

  [Fact]
  public void ToStringFromAst_SubExpressionAst() {
    var sut = new SubExpressionAst(EmptyExtent, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("$()");
  }

  [Fact]
  public void ToStringFromAst_SubExpression_SubExpressionAst() {
    var sut = new SubExpressionAst(EmptyExtent, Block(Const(1)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("$(1;"
                       + NewLine +
                       ")");
  }

  #endregion

  #region ParenExpressionAst

  [Fact]
  public void ToStringFromAst_ParenExpressionAst() {
    var sut = new ParenExpressionAst(EmptyExtent, Pipeline(Const(1)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("(1)");
  }

  [Fact]
  public void ToStringFromAst_Command_ParenExpressionAst() {
    var sut = new ParenExpressionAst(EmptyExtent, Pipeline(CmdStr("Get-Foo")));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("(Get-Foo)");
  }

  #endregion

  #region MemberExpressionAst

  [Fact]
  public void ToStringFromAst_MemberExpressionAst() {
    var sut = new MemberExpressionAst(
      EmptyExtent,
      Var("Foo"),
      CmdStr("Invoke"),
      false
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Foo")}.Invoke");
  }

  [Fact]
  public void ToStringFromAst_NullConditional_MemberExpressionAst() {
    var sut = new MemberExpressionAst(
      EmptyExtent,
      Var("Foo"),
      CmdStr("Invoke"),
      false,
      true
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Foo")}?.Invoke");
  }

  [Fact]
  public void ToStringFromAst_Static_MemberExpressionAst() {
    var sut = new MemberExpressionAst(
      EmptyExtent,
      TypeExpression<string>(),
      CmdStr("NullOrEmpty"),
      true
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.String]::NullOrEmpty");
  }

  [Fact]
  public void ToStringFromAst_StaticGeneric_MemberExpressionAst() {
    var sut = new MemberExpressionAst(
      EmptyExtent,
      TypeExpression<Array>(),
      CmdStr("Empty"),
      true,
      false,
      List(MakeTypeName<string>())
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.Array]::Empty[System.String]");
  }

  [Fact]
  public void ToStringFromAst_StaticGenerics_MemberExpressionAst() {
    var sut = new MemberExpressionAst(
      EmptyExtent,
      TypeExpression<Array>(),
      CmdStr("Empty"),
      true,
      false,
      List(MakeTypeName<string>(), MakeTypeName<int>())
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.Array]::Empty[System.String, System.Int32]");
  }

  #endregion

  #region InvokeMemberExpressionAst

  // Some tests are already covered by MemberExpressionAst

  [Fact]
  public void ToStringFromAst_InvokeMemberExpressionAst() {
    var sut = new InvokeMemberExpressionAst(
      EmptyExtent,
      Var("Foo"),
      CmdStr("Invoke"),
      ExprList(),
      false
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Foo")}.Invoke()");
  }

  [Fact]
  public void ToStringFromAst_Argument_InvokeMemberExpressionAst() {
    var sut = new InvokeMemberExpressionAst(
      EmptyExtent,
      Var("Foo"),
      CmdStr("Invoke"),
      List(Const(1)),
      false
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Foo")}.Invoke(1)");
  }

  [Fact]
  public void ToStringFromAst_Arguments_InvokeMemberExpressionAst() {
    var sut = new InvokeMemberExpressionAst(
      EmptyExtent,
      Var("Foo"),
      CmdStr("Invoke"),
      List(Const(1), Const(2)),
      false
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Foo")}.Invoke(1, 2)");
  }

  [Fact]
  public void ToStringFromAst_Static_InvokeMemberExpressionAst() {
    var sut = new InvokeMemberExpressionAst(
      EmptyExtent,
      TypeExpression<string>(),
      CmdStr("NullOrEmpty"),
      ExprList(),
      true
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[System.String]::NullOrEmpty()");
  }

  [Fact]
  public void ToStringFromAst_StaticArgument_InvokeMemberExpressionAst() {
    var sut = new InvokeMemberExpressionAst(
      EmptyExtent,
      TypeExpression<string>(),
      CmdStr("NullOrEmpty"),
      List(Var("Str")),
      true
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[System.String]::NullOrEmpty({VarS("Str")})");
  }

  [Fact]
  public void ToStringFromAst_StaticArguments_InvokeMemberExpressionAst() {
    var sut = new InvokeMemberExpressionAst(EmptyExtent,
      TypeExpression<string>(),
      CmdStr("NullOrEmpty"),
      List(Var("Str"), Var("false")),
      true
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[System.String]::NullOrEmpty({VarS("Str")}, {VarS("false")})");
  }

  #endregion
}