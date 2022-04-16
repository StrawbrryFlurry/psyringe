using System.Management.Automation;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.TransformationConstants;

namespace PSyringe.Language.Test.AstTransformation;

public class VariableExpressionAstExtensionsTest {
  [Fact]
  public void GetAstAsString_VariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, MakeVariable(VariableName), false);

    var actual = variableExpression.GetAstAsString();

    actual.Should().Be(VariableString);
  }

  [Fact]
  public void GetAstAsString_ScopedVariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, MakeVariable(ScopedVariableName), false);

    var actual = variableExpression.GetAstAsString();

    actual.Should().Be(ScopedVariableString);
  }

  [Fact]
  public void GetAstAsString_SplattedVariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, MakeVariable(VariableName), true);

    var actual = variableExpression.GetAstAsString();

    actual.Should().Be(SplattedVariableString);
  }

  [Fact]
  public void GetAstAsString_SplattedScopedVariableExpression() {
    var variableExpression = new VariableExpressionAst(EmptyExtent, MakeVariable(ScopedVariableName), true);

    var actual = variableExpression.GetAstAsString();

    actual.Should().Be(SplattedScopedVariableString);
  }

  private VariablePath MakeVariable(string fullName) {
    return new VariablePath(fullName);
  }
}