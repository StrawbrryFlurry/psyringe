using System.Linq;
using System.Management.Automation.Language;
using FluentAssertions;
using Moq;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Common.Test.Scripts;
using PSyringe.Language.Parsing;
using Xunit;

namespace PSyringe.Language.Test.Parsing;

public class ScriptParserTest {
  private IElementFactory _elementFactory = new ElementFactory();
  
  [Fact]
  public void Parse_PrependsAssemblyReference_BeforeParsing() {
    var script = ScriptTemplates.EmptyScript;
    
    ScriptParser.PrependAssemblyReference(ref script);

    script.Should().StartWith("using namespace PSyringe.Language.Attributes;");
  }

  [Fact]
  public void Parse_CreatesScriptBlockAst_WhenCalled() {
    MakeParserAndParse(ScriptTemplates.EmptyScript, out var scriptElement);
    
    scriptElement.ScriptBlockAst.Should().BeAssignableTo<ScriptBlockAst>();
  }

  [Fact]
  public void AddStartupFunctionIfDefined_SetsStartupFunctionInScript_WhenScriptHasStartupFunction() {
    MakeParserAndParse(ScriptTemplates.WithStartupFunction, out var script);

    script.StartupFunction.Should().BeAssignableTo<IStartupFunctionElement>();
  }
  
  [Fact]
  public void AddStartupFunctionIfDefined_DoesNotSetAnythingInScript_WhenScriptHasNoStartupFunctionDefined() {
    MakeParserAndParse(ScriptTemplates.WithInjectionSiteFunction, out var script);

    script.StartupFunction.Should().BeNull();
  }
  
  [Fact]
  public void AddAllInjectionSiteElements_AddsInjectionSiteInScript_WhenScriptHasInjectionSite() {
    MakeParserAndParse(ScriptTemplates.WithInjectionSiteFunction, out var script);

    script.InjectionSites.Should().NotBeEmpty();
  }
  
  [Fact]
  public void AddAllInjectionSiteElements_AddsParametersToSite_WhenScriptHasParameters() {
    MakeParserAndParse(ScriptTemplates.WithInjectParameterFunction_NoTarget, out var script);
    
    var site = script.InjectionSites.First();
    site.Parameters.Should().NotBeEmpty();
  }
  
  [Fact]
  public void AddAllInjectVariableElements_AddsInjectVariableInScript_WhenScriptHasInjectVariable() {
    MakeParserAndParse(ScriptTemplates.WithInjectVariableExpression_NoTarget, out var script);

    script.InjectVariables.Should().NotBeEmpty();
  }
  
  [Fact]
  public void AddAllInjectCredentialElements_AddsInjectCredentialInScript_WhenScriptHasInjectCredential() {
    MakeParserAndParse(ScriptTemplates.WithInjectCredentialVariable_NoTarget, out var script);

    script.InjectCredentials.Should().NotBeEmpty();
  }
  
  [Fact]
  public void AddAllInjectTemplateElements_AddsInjectTemplateInScript_WhenScriptHasInjectTemplate() {
    MakeParserAndParse(ScriptTemplates.WithInjectTemplateAttribute_NoTarget, out var script);

    script.InjectTemplates.Should().NotBeEmpty();
  }
  
  private ScriptParser MakeParserAndParse(string script) {
    var visitor = new ScriptVisitor();
    var parser = new ScriptParser(_elementFactory);
    
    parser.Parse(script, visitor);
    return parser;
  }
  
  private ScriptParser MakeParserAndParse(string script, out IScriptElement scriptElement) {
    var visitor = new ScriptVisitor();
    var parser = new ScriptParser(_elementFactory);
    
    scriptElement = parser.Parse(script, visitor);
    return parser;
  }
}