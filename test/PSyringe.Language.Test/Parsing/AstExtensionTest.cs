using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.Attributes;
using PSyringe.Language.Extensions;
using PSyringe.Language.Test.Parsing.Utils;
using Xunit;

namespace PSyringe.Language.Test.Parsing;

public class AstExtensionTest {
  [Fact]
  public void GetReflectedType_ReturnsCoreAttributeType_WhenItExists() {
    var script = "[Inject()]$TestAttribute;";
    var ast = ParsingUtil.ParseScript(script);
    var attributeExpression = ast.Find(e => e is AttributedExpressionAst, true) as AttributedExpressionAst;

    var type = attributeExpression?.Attribute.GetAttributeType();

    type.Should().NotBeNull();
    type.Should().Be(typeof(InjectAttribute));
  }
}