using FluentAssertions;
using PSyringe.Language.AstTransformation.SourceCodeGenerators;
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

  [Fact]
  public void TransformAst_ReplacesInjectAttribute_WhenExpressionHasExplicitTarget() {
    var injectElement = CreateElement("[Inject([ILogger])]$Logger;");
    var ast = injectElement.TransformAst(injectElement.Ast)!;

    var actual = ast.ToStringFromAst();

    actual.Should().Be("$Logger = $null;");
  }
}