using System.Management.Automation.Language;
using System.Security;
using FluentAssertions;
using PSyringe.Common.Test.Scripts;
using PSyringe.Language.Attributes;
using PSyringe.Language.Test.Parsing.Utils;
using PSyringe.Language.TypeLoader;
using Xunit;

namespace PSyringe.Language.Test.TypeLoader; 

public class AttributeTypeLoaderTest {
  
  [Fact]
  public void CreateAttributeInstanceFromAst() {
    var script = @"[Inject(Target = 'Provider')]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);

    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);
    attribute.Should().BeOfType<InjectAttribute>();
    attribute.TargetProviderName.Should().Be("Provider");
  }

  [Fact]
  public void CreateAttributeInstanceFromAst_CreatesAttributeInstance_WhenTargetIsTypeReference() {
    var script = @"[Inject(Target = [ILogger])]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);

    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);
    attribute.Should().BeOfType<InjectAttribute>();
    attribute.TargetProviderName.Should().Be("Provider");
  }
  
  
  [Fact]
  public void CreateAttributeInstanceFromAst_CreatesAttributeInstance_WhenOptionalExplicitlyIsSet() {
    var script = @"[Inject(Target = [ILogger], Optional = $true)]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);

    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);
    attribute.Should().BeOfType<InjectAttribute>();
    attribute.TargetProviderName.Should().Be("Provider");
  }
  
  [Fact]
  public void CreateAttributeInstanceFromAst_CreatesAttributeInstance_WhenOptionalImplicitlyIsSet() {
    var script = @"[Inject(Target = [ILogger], Optional)]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);

    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);
    attribute.Should().BeOfType<InjectAttribute>();
    attribute.TargetProviderName.Should().Be("Provider");
  }
  
  private AttributeAst GetAttributeAstFromScript(string script) {
    var ast = ParsingUtil.GetAttributedExpressionAstFromScript(script);
    return ast.Attribute as AttributeAst;
  }
}