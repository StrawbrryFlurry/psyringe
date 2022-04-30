using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Providers;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.AstTransformation.SourceCodeGenerators;
using PSyringe.Language.Attributes;
using PSyringe.Language.Elements;
using PSyringe.Language.Extensions;
using PSyringe.Language.Test.Parsing.Utils;
using Xunit;

namespace PSyringe.Language.Test.AstTransformation;

public class ScriptTransformerTest {
  public readonly ScriptTransformer sut = new ElementScriptTransformer();

  [Fact(Skip = "Implement elements first")]
  public void Transform_TransformsAllElementsInScriptDefinition() {
  }

  [Fact]
  public void MakeVariable_CreatesScriptScopedVariableExpression_WithOnlyName() {
    var variable = sut.MakeVariable("var");

    variable.GetName().Should().Be("script:var");
  }

  [Fact]
  public void MakeVariable_CreatesScopedVariableExpression_WithNameAndScope() {
    var variable = sut.MakeVariable("var", "local");

    variable.GetName().Should().Be("local:var");
  }

  [Fact]
  public void MakeVariable_CreatesSplattedScriptScopedVariable_WithNameAndSplatted() {
    var variable = sut.MakeVariable("var", null, true);

    variable.GetName().Should().Be("script:var");
    variable.Splatted.Should().BeTrue();
  }

  [Fact]
  public void AddProvider_CreatesVariableWithCorrectName_WhenProviderIsAString() {
    var provider = new ProviderResolvable("provider");
    var variable = sut.AddProvider("var", provider);
    var expected = GetProviderVariableName("var", provider);

    variable.GetName().Should().Be(expected);
  }

  [Fact]
  public void AddProvider_CreatesVariableWithCorrectName_WhenProviderIsAType() {
    var provider = new ProviderResolvable(typeof(ILogger));
    var variable = sut.AddProvider("var", provider);
    var expected = GetProviderVariableName("var", provider);

    variable.GetName().Should().Be(expected);
  }

  private string GetProviderVariableName(string variableName, ProviderResolvable provider) {
    var name = sut.GetProviderVariableName(variableName, provider);
    return $"script:{name}";
  }

  [Fact]
  public void AddProvider_AddsProviderDependencyToInternalDictionary() {
    var provider = new ProviderResolvable(typeof(ILogger));
    var variable = sut.AddProvider("var", provider);

    sut.Providers[variable].Should().Be(provider);
  }

  [Fact]
  public void SpliceAttributeFromAttributeTree_ReturnsSameAst_WhenAttributeTreeDoesNotContainAttribute() {
    var attributedExpression = Parse<AttributedExpressionAst>("[LogExpression()]$var");

    var withoutAttribute =
      sut.SpliceAttributeFromAttributedExpression(attributedExpression, typeof(InjectAttribute));

    withoutAttribute.Should().Be(attributedExpression);
  }

  [Fact]
  public void SpliceAttributeFromAttributeTree_ReturnsSameAst_WhenTypeIsTheAst() {
    var attributedExpression = Parse<AttributedExpressionAst>("[Inject()]$var");

    var withoutAttribute =
      sut.SpliceAttributeFromAttributedExpression(attributedExpression, typeof(InjectAttribute));
    var actual = withoutAttribute.ToStringFromAst();

    actual.Should().Be("$var");
  }

  [Fact]
  public void SpliceAttributeFromAttributeTree_ReturnsSameAst_WhenTypeIsAParentOfTheAst() {
    var parentExpression = Parse<AttributedExpressionAst>("[Inject()][LogExpression()]$var");
    var attributedExpression = (AttributedExpressionAst) parentExpression.Child;

    var withoutAttribute =
      sut.SpliceAttributeFromAttributedExpression(attributedExpression, typeof(InjectAttribute));
    var actual = withoutAttribute.ToStringFromAst();

    actual.Should().Be("[LogExpression()]$var");
  }

  [Fact]
  public void SpliceAttributeFromAttributeTree_ReturnsSameAst_WhenTypeIsAChildOfTheAst() {
    var attributedExpression = Parse<AttributedExpressionAst>("[LogExpression()][Inject()]$var");

    var withoutAttribute =
      sut.SpliceAttributeFromAttributedExpression(attributedExpression, typeof(InjectAttribute));
    var actual = withoutAttribute.ToStringFromAst();

    actual.Should().Be("[LogExpression()]$var");
  }

  private T Parse<T>(string script) where T : Ast {
    return ParsingUtil.ParseScript(script).FindOfType<T>()!;
  }
}

internal static class UtilityExtensions {
  public static string GetName(this VariableExpressionAst variable) {
    return variable.VariablePath.UserPath;
  }
}