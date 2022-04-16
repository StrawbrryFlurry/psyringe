using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstExtensions;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.TransformationConstants;

namespace PSyringe.Language.Test.AstTransformation;

public class TernaryExpressionAstExtensionsTest {
  [Fact]
  public void GetAstAsString_TernaryExpression() {
    var sut = new TernaryExpressionAst(EmptyExtent, FalseExpression, TrueExpression, FalseExpression);
    var actual = sut.GetAstAsString();

    TernaryExpression.Should().Be(actual);
  }
}