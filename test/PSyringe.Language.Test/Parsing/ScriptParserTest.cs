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
  private readonly IElementFactory _elementFactory = new ElementFactory();

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
  public void AddStartupFunctionIfDefined_SetsStartupFunctionToScript_WhenScriptHasStartupFunction() {
    MakeParserAndParse(ScriptTemplates.WithStartupFunction, out var script);

    script.StartupFunction.Should().BeAssignableTo<IStartupFunctionElement>();
  }

  [Fact]
  public void AddStartupFunctionIfDefined_DoesNotSetAnythingToScript_WhenScriptHasNoStartupFunctionDefined() {
    MakeParserAndParse(ScriptTemplates.WithInjectionSiteFunction, out var script);

    script.StartupFunction.Should().BeNull();
  }

  [Fact]
  public void AddAllInjectionSiteElementsToScript_AddsInjectionSiteToScript_WhenScriptHasInjectionSite() {
    MakeParserAndParse(ScriptTemplates.WithInjectionSiteFunction, out var script);

    script.InjectionSiteElements.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllInjectionSiteElementsToScript_AddsParametersToSite_WhenScriptHasParameters() {
    MakeParserAndParse(ScriptTemplates.WithInjectParameterFunction_NoTarget, out var script);

    var site = script.InjectionSiteElements.First();
    site.Parameters.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllInjectElementsToScript_AddsInjectVariableToScript_WhenScriptHasInjectVariable() {
    MakeParserAndParse(ScriptTemplates.WithInjectVariableExpression_NoTarget, out var script);

    script.InjectVariableElements.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllInjectElementsToScript_AddsInjectCredentialToScript_WhenScriptHasInjectCredential() {
    MakeParserAndParse(ScriptTemplates.WithInjectCredentialVariable_NoTarget, out var script);

    script.InjectCredentialElements.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllInjectElementsToScript_AddsInjectTemplateToScript_WhenScriptHasInjectTemplate() {
    MakeParserAndParse(ScriptTemplates.WithInjectTemplateAttribute_NoTarget, out var script);

    script.InjectTemplateElements.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllInjectElementsToScript_AddsInjectDatabaseToScript_WhenScriptHasInjectDatabase() {
    MakeParserAndParse(ScriptTemplates.WithInjectDatabaseVariable_ConnectionString, out var script);

    script.InjectDatabaseElements.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllCallbackElementsToScript_AddsOnErrorCallbackToScript_WhenScriptHasOnErrorCallbackFn() {
    MakeParserAndParse(ScriptTemplates.WithOnErrorFunction, out var script);

    script.OnErrorFunctions.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllCallbackElementsToScript_AddsOnLoadedCallbackToScript_WhenScriptHasOnLoadedCallbackFn() {
    MakeParserAndParse(ScriptTemplates.WithOnLoadedFunction, out var script);

    script.OnLoadFunctions.Should().NotBeEmpty();
  }

  [Fact]
  public void AddAllCallbackElementsToScript_AddsBeforeUnloadCallbackToScript_WhenScriptHasBeforeUnloadCallbackFn() {
    MakeParserAndParse(ScriptTemplates.WithBeforeUnloadFunction, out var script);

    script.BeforeUnloadFunctions.Should().NotBeEmpty();
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