using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation;
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