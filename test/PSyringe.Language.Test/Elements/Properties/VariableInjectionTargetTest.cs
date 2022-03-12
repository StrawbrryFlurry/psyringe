using System.Linq;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Providers;
using PSyringe.Common.Test.Scripts;
using PSyringe.Language.Attributes;
using PSyringe.Language.Elements.Properties;
using PSyringe.Language.Test.Parsing.Utils;
using Xunit;

namespace PSyringe.Language.Test.Elements.Properties;

public class VariableInjectionTargetTest {
  [Fact]
  public void HasDefaultValue_ReturnsTrue_WhenVariableIsPartOfAssignmentExpression() {
    var sut = MakeInjectionTargetFromAttributedExpressionInScript(ScriptTemplates.WithInjectVariableAssigment_NoTarget);

    sut.HasDefaultValue().Should().BeTrue();
  }

  [Fact]
  public void HasDefaultValue_ReturnsFalse_WhenVariableIsNotPartOfAssignmentExpression() {
    var sut = MakeInjectionTargetFromAttributedExpressionInScript(
      ScriptTemplates.WithInjectVariableExpression_NoTarget
      );

    sut.HasDefaultValue().Should().BeFalse();
  }

  [Fact]
  public void GetVariableTypeConstraint_ReturnsTypeConstraint_WhenVariableHasTypeConstraint() {
    var sut = MakeInjectionTargetFromAttributedExpressionInScript(
      ScriptTemplates.WithInjectVariableExpression_ImplicitTarget
    );

    sut.GetVariableTypeConstraint().Should().Be(typeof(ILogger));
  }

  [Fact]
  public void GetVariableTypeConstraint_ReturnsNull_WhenVariableHasNoTypeConstraint() {
    var sut = MakeInjectionTargetFromAttributedExpressionInScript(
      ScriptTemplates.WithInjectVariableExpression_ExplicitTarget
    );

    sut.GetVariableTypeConstraint().Should().BeNull();
  }

  [Fact]
  public void GetInjectAttributeInstance_ReturnsNull_WhenVariableHasNoTypeConstraint() {
    var sut = MakeInjectionTargetFromAttributedExpressionInScript(
      ScriptTemplates.WithInjectVariableExpression_ExplicitTarget
    );

    sut.GetInjectAttributeInstance<InjectAttribute>().Should().BeNull();
  }

  private VariableInjectionTarget MakeInjectionTargetFromAttributedExpressionInScript(string script) {
    var attributedVariableExpressionAst = ParsingUtil.GetAttributedExpressionAstFromScript(script);
    return new VariableInjectionTarget(attributedVariableExpressionAst!);
  }
}