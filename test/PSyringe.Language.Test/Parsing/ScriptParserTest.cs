using System.Linq;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Common.Test.Scripts;
using PSyringe.Language.Parsing;
using Xunit;

namespace PSyringe.Language.Test.Parsing;

public class ScriptParserTest {
  [Fact]
  public void Parse_PrependsAssemblyReference_BeforeParsing() {
    var sut = MakeParserAndParse(ScriptTemplates.EmptyScript);

    sut.ScriptBeforeParsing.Should().StartWith("using namespace PSyringe.Language.Attributes;");
  }

  [Fact]
  public void Parse_CreatesScriptBlockAst_WhenCalled() {
    var sut = MakeParserAndParse(ScriptTemplates.EmptyScript);

    sut.ScriptAst.Should().BeOfType<ScriptBlockAst>();
  }

  [Fact]
  public void Parse_CallsScriptVisitor_WhenCalled() {
    var sut = MakeParserAndParse(ScriptTemplates.WithStartupFunction);

    sut.Visitor.HasVisited.Should().BeTrue();
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
    var factory = new ElementFactory();
    var parser = new ScriptParser(visitor, factory);
    
    parser.Parse(script);
    return parser;
  }
  
  private ScriptParser MakeParserAndParse(string script, out IScriptElement scriptElement) {
    var visitor = new ScriptVisitor();
    var factory = new ElementFactory();
    var parser = new ScriptParser(visitor, factory);
    
    scriptElement = parser.Parse(script);
    return parser;
  }
}