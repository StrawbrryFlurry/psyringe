using System.Linq;
using FluentAssertions;
using PSyringe.Language.AstExtensions;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.TransformationConstants;

namespace PSyringe.Language.Test.AstTransformation;

public class AstBaseClassExtensionsTest {
  [Fact]
  public void GetExtensionMethodOverloadDerivedAstType_ReturnsCorrectMethodOverload_ForExpressionAst() {
    var actual = AstBaseClassExtensions.GetExtensionMethodOverloadDerivedAstType(TrueExpression);
    var expected = typeof(VariableExpressionAstExtension).GetMethods().First();

    actual.Should().BeSameAs(expected);
  }
}