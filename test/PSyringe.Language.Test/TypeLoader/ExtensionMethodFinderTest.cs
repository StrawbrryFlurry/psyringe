using FluentAssertions;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.TypeLoader;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.Utils.StringConstants;

namespace PSyringe.Language.Test.TypeLoader;

public class ExtensionMethodFinderTest {
  [Fact]
  public void GetExtensionMethodOverloadDerivedAstType_ReturnsCorrectMethodOverload_ForExpressionAst() {
    var actual =
      ExtensionMethodFinder.GetExtensionMethodOverloadForConcreteType(Var(True),
        nameof(VariableExpressionAstExtensions.ToStringFromAst));
    var expected = typeof(VariableExpressionAstExtensions).GetMethods();

    expected.Should().Contain(actual);
  }
}