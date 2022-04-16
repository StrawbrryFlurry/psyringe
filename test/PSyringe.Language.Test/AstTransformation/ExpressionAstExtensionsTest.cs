using System.Collections.Generic;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.Compiler;
using PSyringe.Language.Test.Parsing.Utils;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.TransformationConstants;

namespace PSyringe.Language.Test.AstTransformation;

public class ExpressionAstExtensionsTest {
  [Fact]
  public void GetAstAsString_TernaryExpression() {
    var sut = new TernaryExpressionAst(EmptyExtent, FalseExpression, TrueExpression, FalseExpression);
    var actual = sut.GetAstAsString();

    actual.Should().Be(TernaryExpression);
  }

  [Fact]
  public void GetAstAsString_BinaryExpressionAst() {
    var sut = new BinaryExpressionAst(EmptyExtent, NumberOneExpression, TokenKind.Plus, NumberOneExpression,
      EmptyExtent);
    var actual = sut.GetAstAsString();

    actual.Should().Be(BinaryExpression);
  }

  [Fact]
  public void GetAstAsString_DoesntQuoteTheValue_ConstantExpressionAstWithNumber() {
    var sut = new ConstantExpressionAst(EmptyExtent, One);
    var actual = sut.GetAstAsString();

    actual.Should().Be(One.ToString());
  }

  [Fact]
  public void GetAstAsString_ReturnsTrueVariable_ConstantExpressionAstWithTrue() {
    var sut = new ConstantExpressionAst(EmptyExtent, true);
    var actual = sut.GetAstAsString();

    actual.Should().Be(TrueVariable);
  }

  [Fact]
  public void GetAstAsString_ReturnsUnquotedString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.BareWord);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ConstantString);
  }

  [Fact]
  public void GetAstAsString_ReturnsDoubleQuotedString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.DoubleQuoted);
    var actual = sut.GetAstAsString();

    actual.Should().Be(DoubleQuote(ConstantString));
  }

  [Fact]
  public void GetAstAsString_ReturnsSingleQuotedString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.SingleQuoted);
    var actual = sut.GetAstAsString();

    actual.Should().Be(SingleQuote(ConstantString));
  }

  [Fact]
  public void GetAstAsString_ReturnsDoubleQuotedHereString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.DoubleQuotedHereString);
    var actual = sut.GetAstAsString();

    actual.Should().Be($"@{DoubleQuote(ConstantString)}@");
  }

  [Fact]
  public void GetAstAsString_ReturnsSingleQuotedHereString_StringConstantExpressionAst() {
    var sut = new StringConstantExpressionAst(EmptyExtent, ConstantString, StringConstantType.SingleQuotedHereString);
    var actual = sut.GetAstAsString();

    actual.Should().Be($"@{SingleQuote(ConstantString)}@");
  }

  [Fact]
  // Uses the same implementation as StringConstantExpressionAst
  public void GetAstAsString_ExpandableStringExpressionAst() {
    var sut = new ExpandableStringExpressionAst(EmptyExtent, ConstantString, StringConstantType.BareWord);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ConstantString);
  }

  [Fact]
  public void GetAstAsString_ArrayLiteralAst() {
    var sut = new ArrayLiteralAst(EmptyExtent, ArrayOneElement);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ArrayExpressionOneElement);
  }

  [Fact]
  public void GetAstAsString_SeparatesElementsByComma_ArrayLiteralAst() {
    var sut = new ArrayLiteralAst(EmptyExtent, ArrayTwoElements);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ArrayExpressionTwoElements);
  }

  [Fact]
  public void GetAstAsString_UsingExpressionAst() {
    var sut = new UsingExpressionAst(EmptyExtent, VariableExpression(VariableString));
    var actual = sut.GetAstAsString();

    actual.Should().Be(UsingExpression(VariableString));
  }

  [Fact]
  public void GetAstAsString_IndexExpressionAst() {
    var sut = new IndexExpressionAst(EmptyExtent, VariableExpression(VariableName), NumberOneExpression);
    var actual = sut.GetAstAsString();

    actual.Should().Be(IndexExpression(VariableString, One));
  }

  [Fact]
  public void GetAstAsString_NullConditional_IndexExpressionAst() {
    var sut = new IndexExpressionAst(EmptyExtent, VariableExpression(VariableName), NumberOneExpression, true);
    var actual = sut.GetAstAsString();

    actual.Should().Be(IndexExpression(VariableString, One, true));
  }

  [Fact]
  public void GetAstAsString_TypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, TypeNameExpression<string>());
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.String]");
  }

  [Fact]
  public void GetAstAsString_ReflectionTypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, ReflectionTypeNameExpression<string>());
    var actual = sut.GetAstAsString();

    actual.Should().Be("[string]");
  }

  [Fact]
  public void GetAstAsString_ArrayTypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, ArrayTypeNameExpression<string>());
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.String[]]");
  }

  [Fact]
  public void GetAstAsString_ArrayTypeNameDepthTwo_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, ArrayTypeNameExpression<string>(2));
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.String[][]]");
  }

  [Fact]
  public void GetAstAsString_GenericTypeName_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, GenericTypeNameExpression<List<string>>());
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.Collections.Generic.List[System.String]]");
  }

  [Fact]
  public void GetAstAsString_GenericTypeNameTwoArguments_TypeExpressionAst() {
    var sut = new TypeExpressionAst(EmptyExtent, GenericTypeNameExpression<Dictionary<string, string>>());
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.Collections.Generic.Dictionary[System.String,System.String]]");
  }

  [Fact]
  public void GetAstAsString_UnaryIncrement_UnaryExpressionAst() {
    var sut = new UnaryExpressionAst(EmptyExtent, TokenKind.PlusPlus, VariableExpression(VariableName));
    var actual = sut.GetAstAsString();

    actual.Should().Be($"{VariableString}++");
  }

  [Fact]
  public void GetAstAsString_NotOperator_UnaryExpressionAst() {
    var sut = new UnaryExpressionAst(EmptyExtent, TokenKind.Not, VariableExpression(VariableName));
    var actual = sut.GetAstAsString();


    actual.Should().Be($"-not{VariableString}");
  }

  [Fact]
  public void GetAstAsString_NotExclamationMark_UnaryExpressionAst() {
    var sut = new UnaryExpressionAst(EmptyExtent, TokenKind.Exclaim, VariableExpression(VariableName));
    var actual = sut.GetAstAsString();

    actual.Should().Be($"!{VariableString}");
  }

  [Fact]
  public void GetAstAsString_ReturnsEmpty_ErrorExpressionAst() {
    // The ErrorExpressionAst ctor is internal so we just
    // make one using a nonsensical expression.
    var sut = ParsingUtil.ParseScript("$false ? if() {} : else {}")!.FindOfType<ErrorExpressionAst>()!;
    var actual = sut.GetAstAsString();

    actual.Should().Be("");
  }

  /*
   TODO: After CommandElementAst
  [Fact]
  public void GetAstAsString_InvokeMemberExpressionAst() {
    var sut = new MemberExpressionAst(EmptyExtent, );
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.Collections.Generic.Dictionary[System.String,System.String]]");
  }

  
  [Fact]
  public void GetAstAsString_MemberExpressionAst() {
    var sut = new MemberExpressionAst(EmptyExtent, );
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.Collections.Generic.Dictionary[System.String,System.String]]");
  }
  
  */
  /*
   TODO: Confusion
  [Fact]
  public void GetAstAsString_BaseCtorInvokeMemberExpressionAst() {
    var sut = new BaseCtorInvokeMemberExpressionAst(EmptyExtent, GenericTypeNameExpression<Dictionary<string, string>>());
    var actual = sut.GetAstAsString();

    actual.Should().Be("[System.Collections.Generic.Dictionary[System.String,System.String]]");
  }

  */
  /*
TODO: After StatementAst
   [Fact]
  public void GetAstAsString_SubExpressionAst() {
    var sut = new SubExpressionAst(EmptyExtent, ArrayOneElement);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ArrayExpressionOneElement);
  }
*/
  /*
  TODO: After PipelineAst
  [Fact]
  public void GetAstAsString_ParenExpressionAst() {
    var sut = new ParenExpressionAst();
  }
  
  */
  /*
   TODO: After StatementAst
  [Fact]
  public void GetAstAsString_ArrayExpressionAst() {
    var sut = new ArrayExpressionAst(EmptyExtent, ArrayOneElement);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ArrayExpressionOneElement);
  }

  [Fact]
  public void GetAstAsString_SeparatesElementsByComma_ArrayExpressionAst() {
    var sut = new ArrayExpressionAst(EmptyExtent, ArrayTwoElements);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ArrayExpressionTwoElements);
  }
*/
  /*
   TODO: After ScriptBlockAst
  [Fact]
  public void GetAstAsString_ScriptBlockExpressionAst() {
    var sut = new ScriptBlockExpressionAst(EmptyExtent, ConstantString, StringConstantType.BareWord);
    var actual = sut.GetAstAsString();

    actual.Should().Be(ConstantString);
  }
  */
}