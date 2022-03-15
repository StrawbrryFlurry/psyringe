using System.Management.Automation;
using System.Management.Automation.Language;
using System.Security;
using FluentAssertions;
using PSyringe.Common.Providers;
using PSyringe.Common.Test.Scripts;
using PSyringe.Language.Attributes;
using PSyringe.Language.Test.Parsing.Utils;
using PSyringe.Language.TypeLoader;
using Xunit;

namespace PSyringe.Language.Test.TypeLoader; 

public class AttributeTypeLoaderTest {
  
  [Fact]
  public void CreateAttributeInstanceFromAst_WithTargetProvider_WhenTargetIsString() {
    var script = @"[Inject(Target = 'Provider')]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);

    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);
    
    attribute.Should().BeOfType<InjectAttribute>();
    attribute.TargetProviderName.Should().Be("Provider");
  }

  [Fact]
  public void CreateAttributeInstanceFromAst_CreatesAttributeInstanceWithTargetType_WhenTargetIsTypeReference() {
    var script = @"[Inject(Target = [ILogger])]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);

    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);
    
    attribute.TargetType.Should().Be(typeof(ILogger));
  }
  
  [Fact]
  public void CreateAttributeInstanceFromAst_CreatesAttributeInstanceWithOptional_WhenOptionalExplicitlyIsSet() {
    var script = @"[Inject(Target = [ILogger], Optional = $true)]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);
    
    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);
    
    attribute.IsOptional.Should().BeTrue();
  }
  
  [Fact]
  public void CreateAttributeInstanceFromAst_CreatesAttributeInstanceWithOptional_WhenOptionalImplicitlyIsSet() {
    var script = @"[Inject(Target = [ILogger], Optional)]$Variable;";
    var attributeAst = GetAttributeAstFromScript(script);

    var attribute = AttributeTypeLoader.CreateAttributeInstanceFromAst<InjectAttribute>(attributeAst);

    attribute.IsOptional.Should().BeTrue();
  }

  private AttributeAst GetAttributeAstFromScript(string script) {
    var ast = ParsingUtil.GetAttributedExpressionAstFromScript(script);
    return ast.Attribute as AttributeAst;
  }
}