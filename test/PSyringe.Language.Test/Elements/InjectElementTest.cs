using FluentAssertions;
using PSyringe.Common.Providers;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;
using PSyringe.Language.Elements;
using Xunit;
using static PSyringe.Language.Test.Elements.MockElementFactory<PSyringe.Language.Elements.InjectElement>;

namespace PSyringe.Language.Test.Elements;

public class InjectElementTest {
  private const string InjectVariableExpression_Type = "[Inject([ILogger])]$Logger";
  private const string InjectVariableExpression_Type_Optional = "[Inject([ILogger], Optional)]$Logger";
  private const string InjectVariableExpression_Provider = "[Inject('Logger')]$Logger";
  private const string InjectVariableExpression_Provider_Optional = "[Inject('Logger', Optional)]$Logger";

  private const string InjectVariableAssignment_Type = $"{InjectVariableExpression_Type} = $null";
  private const string InjectVariableAssignment_Type_Optional = $"{InjectVariableExpression_Type_Optional} = $null";
  private const string InjectVariableAssignment_Provider = $"{InjectVariableExpression_Provider} = $null";

  private const string InjectVariableAssignment_Provider_Optional =
    $"{InjectVariableExpression_Provider_Optional} = $null";

  private readonly ScriptTransformer _scriptTransformer = new ElementScriptTransformer();

  [Fact]
  public void TransformAst_ReplacesInjectAttribute_VariableExpressionWithSingleInjectAttribute() {
    var sut = CreateElement("[Inject([ILogger])]$Logger;", out var sb);

    sut.TransformAst(_scriptTransformer);
    var actual = sb.ToStringFromAst();

    actual.Should().StartWith("$Logger");
  }

  [Fact]
  public void TransformAst_ReplacesOnlyInjectAttribute_VariableExpressionWithMultipleAttributes() {
    var sut = CreateElement("[Inject([ILogger])][LogExpression()]$Logger;", out var sb);

    sut.TransformAst(_scriptTransformer);
    var actual = sb.ToStringFromAst();

    actual.Should().StartWith("[LogExpressionAttribute()]$Logger");
  }

  [Fact]
  public void TransformAst_ConvertsToAssignmentWithProviderVariable_VariableExpressionWithSingleInjectAttribute() {
    var sut = CreateElement("[Inject([ILogger])]$Logger;", out var sb);

    sut.TransformAst(_scriptTransformer);
    var actual = sb.ToStringFromAst();

    var providerVariable =
      _scriptTransformer.GetProviderVariableName("Logger", new ProviderResolvable(typeof(ILogger)));

    actual.Should().Be($"$Logger = $script:{providerVariable};");
  }
}