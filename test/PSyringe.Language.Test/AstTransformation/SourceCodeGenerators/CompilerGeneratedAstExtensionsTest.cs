using FluentAssertions;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.AstTransformation.SourceCodeGenerators;
using PSyringe.Language.AstTransformation.SyntheticAsts;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation.SourceCodeGenerators;

public class CompilerGeneratedAstExtensionsTest {
  [Fact]
  public void ToStringFromAst_CompilerGeneratedExpressionAst() {
    var sut = new SyntheticExpressionAst(EmptyExtent, Var("Variable"));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{CodeGenConstants.InlineExpression} {VarS("Variable")}");
  }

  [Fact]
  public void ToStringFromAst_CompilerGeneratedBlockAst() {
    var sut = new SyntheticBlockAst(EmptyExtent, List(Statement(Var("Variable"))), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(CodeGenConstants.BlockOpen
                       + NewLine + "{"
                       + NewLine + VarS("Variable")
                       + NewLine + "}"
                       + NewLine +
                       CodeGenConstants.BlockClose);
  }
}