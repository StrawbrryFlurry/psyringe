using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation.SyntheticAsts;
using Xunit;

namespace PSyringe.Language.Test.AstTransformation.SyntheticAsts;

public class SyntheticScriptExtentTest {
  [Fact]
  public void ReplaceExtent_ReplacesScriptExtentProperty_OfAnyAstInstance() {
    var variableAst = new VariableExpressionAst(SyntheticScriptExtent.EmptyScriptExtent, "variable", false);
    SyntheticScriptExtent.UpdateScriptExtent(variableAst);
    ReferenceEquals(SyntheticScriptExtent.EmptyScriptExtent, variableAst.Extent).Should().BeFalse();
  }
}