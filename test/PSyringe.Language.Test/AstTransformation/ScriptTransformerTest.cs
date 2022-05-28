using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Providers;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;
using PSyringe.Language.Attributes;
using PSyringe.Language.Elements;
using PSyringe.Language.Extensions;
using PSyringe.Language.Test.Parsing.Utils;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.CodeGenerationAstExtensions.Utils.MakeAstUtils;

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
  public void SetParent_SetsParentForAst_WhenParentIsAValidAstElement() {
    var ast = Parse<AttributedExpressionAst>("[Inject()]$var");
    ast.Child.SetParent();
    var child = ast.Child;
    var replacementParent = new AttributedExpressionAst(Extent(), Attr<LogExpressionAttribute>(), child);

    child.SetParent(replacementParent);

    child.Parent.Should().Be(replacementParent);
  }

  [Fact]
  public void ReplaceAttributeInNestedExpression_DoesNothing_WhenAttributeTreeDoesNotContainAttribute() {
    var attributedExpression = Parse<AttributedExpressionAst>("[LogExpression()]$var");

    sut.ReplaceAttributeInNestedExpression(attributedExpression, typeof(InjectAttribute));

    attributedExpression.Should().Be(attributedExpression);
  }

  [Fact]
  public void ReplaceAttributeInNestedExpression_ReplacesItself_WhenTypeIsTheAst() {
    var attributedExpression = Parse<AttributedExpressionAst>("[Inject()]$var");

    sut.ReplaceAttributeInNestedExpression(attributedExpression, typeof(InjectAttribute));
    // we need to call ToStringFromAst from the parent
    // because the expression itself was not changed
    var actual = attributedExpression.Parent.ToStringFromAst();

    actual.Should().Be("$var");
  }

  [Fact]
  public void ReplaceAttributeInNestedExpression_Returns_WhenTypeIsAParentOfTheAst() {
    var parentExpression = Parse<AttributedExpressionAst>("[Inject()][LogExpression()]$var");
    var attributedExpression = (AttributedExpressionAst) parentExpression.Child;

    sut.ReplaceAttributeInNestedExpression(attributedExpression, typeof(InjectAttribute));

    var actual = parentExpression.Parent.ToStringFromAst();
    actual.Should().Be("[LogExpressionAttribute()]$var");
  }

  [Fact]
  public void ReplaceAttributeInNestedExpression_ReplacesChildInParent_WhenTypeIsAChildOfTheAst() {
    var attributedExpression = Parse<AttributedExpressionAst>("[LogExpression()][Inject()]$var");
    var variable = attributedExpression.GetNestedChildAssignableToType<VariableExpressionAst>()!;

    sut.ReplaceAttributeInNestedExpression(attributedExpression, typeof(InjectAttribute));
    var actual = variable.Parent.ToStringFromAst();

    actual.Should().Be("[LogExpressionAttribute()]$var");
  }

  [Fact]
  public void ReplaceAttributeInNestedExpression_ReplacesTheParentOfTheChild_WhenTypeIsAChildOfTheAst() {
    var attributedExpression = Parse<AttributedExpressionAst>("[LogExpression()][Inject()]$var");
    var variable = attributedExpression.GetNestedChildAssignableToType<VariableExpressionAst>()!;

    sut.ReplaceAttributeInNestedExpression(attributedExpression, typeof(InjectAttribute));

    variable.Parent.Should().BeSameAs(attributedExpression);
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