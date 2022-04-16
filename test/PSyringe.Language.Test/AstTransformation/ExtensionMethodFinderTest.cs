using FluentAssertions;
using PSyringe.Language.AstTransformation;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.TransformationConstants;

namespace PSyringe.Language.Test.AstTransformation;

public class AstBaseClassExtensionsTest {
  [Fact]
  public void GetExtensionMethodOverloadDerivedAstType_ReturnsCorrectMethodOverload_ForExpressionAst() {
    var actual =
      ExtensionMethodFinder.GetExtensionMethodOverloadForConcreteType(TrueExpression,
        nameof(ExpressionAstExtensions.GetAstAsString));
    var expected = typeof(ExpressionAstExtensions).GetMethods();

    expected.Should().Contain(actual);
  }
}