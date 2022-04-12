using System.Linq;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Language.Elements;
using PSyringe.Common.Test.Scripts;
using PSyringe.Language.Elements;
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
    script.Should().Contain("using namespace PSyringe.Common.Providers;");
  }

  [Fact]
  public void Parse_CreatesScriptBlockAst_WhenCalled() {
    MakeParserAndParse(ScriptTemplates.EmptyScript, out var scriptElement);

    scriptElement.ScriptBlock.Should().BeAssignableTo<ScriptBlockAst>();
  }

  [Fact]
  public void AddAllFunctionDefinitionElementsToScript_AddsStartupFunctionToScript_WhenScriptHasStartupFunction() {
    MakeParserAndParse(ScriptTemplates.WithStartupFunction, out var script);

    script.Elements.First().Should().BeOfType<StartupFunctionElement>();
  }

  [Fact]
  public void AddAllFunctionDefinitionElementsToScript_AddsInjectionSiteToScript_WhenScriptHasInjectionSite() {
    MakeParserAndParse(ScriptTemplates.WithInjectionSiteFunction, out var script);

    script.Elements.First().Should().BeOfType<InjectionSiteElement>();
  }

  [Fact]
  public void
    AddAllVariableExpressionElementsToScript_AddsInjectVariableToScript_WhenScriptHasInjectVariableExpression() {
    MakeParserAndParse(ScriptTemplates.WithInjectVariableExpression_NoTarget, out var script);

    script.Elements.First().Should().BeOfType<InjectElement>();
  }


  [Fact]
  public void
    AddAllVariableExpressionElementsToScript_AddsInjectVariableToScript_WhenScriptHasInjectVariableAssignment() {
    MakeParserAndParse(ScriptTemplates.WithInjectVariableAssigment_NoTarget, out var script);

    script.Elements.First().Should().BeOfType<InjectElement>();
  }


  [Fact]
  public void AddAllVariableExpressionElementsToScript_AddsInjectCredentialToScript_WhenScriptHasInjectCredential() {
    MakeParserAndParse(ScriptTemplates.WithInjectCredentialVariable_NoTarget, out var script);

    script.Elements.First().Should().BeOfType<InjectSecretElement>();
  }

  [Fact]
  public void AddAllVariableExpressionElementsToScript_AddsInjectDatabaseToScript_WhenScriptHasInjectDatabase() {
    MakeParserAndParse(ScriptTemplates.WithInjectDatabaseVariable_ConnectionString, out var script);

    script.Elements.First().Should().BeOfType<InjectDatabaseElement>();
  }


  [Fact]
  public void AddAllVariableExpressionElementsToScript_AddsInjectConstantToScript_WhenScriptHasInjectConstant() {
    MakeParserAndParse(ScriptTemplates.WithInjectConstantVariable_NoTarget, out var script);

    script.Elements.First().Should().BeOfType<InjectConstantElement>();
  }

  [Fact]
  public void AddAllFunctionDefinitionElementsToScript_AddsOnErrorCallbackToScript_WhenScriptHasOnErrorCallbackFn() {
    MakeParserAndParse(ScriptTemplates.WithOnErrorFunction, out var script);

    script.Elements.First().Should().BeOfType<OnErrorCallbackElement>();
  }

  [Fact]
  public void AddAllFunctionDefinitionElementsToScript_AddsOnLoadedCallbackToScript_WhenScriptHasOnLoadedCallbackFn() {
    MakeParserAndParse(ScriptTemplates.WithOnLoadedFunction, out var script);

    script.Elements.First().Should().BeOfType<OnLoadedCallbackElement>();
  }

  [Fact]
  public void
    AddAllFunctionDefinitionElementsToScript_AddsBeforeUnloadCallbackToScript_WhenScriptHasBeforeUnloadCallbackFn() {
    MakeParserAndParse(ScriptTemplates.WithBeforeUnloadFunction, out var script);

    script.Elements.First().Should().BeOfType<BeforeUnloadCallbackElement>();
  }

  private ScriptParser MakeParserAndParse(string script) {
    var visitor = new ScriptParserVisitor();
    var parser = new ScriptParser(_elementFactory);

    parser.Parse(script, visitor);
    return parser;
  }

  private ScriptParser MakeParserAndParse(string script, out IScriptDefinition scriptElement) {
    var visitor = new ScriptParserVisitor();
    var parser = new ScriptParser(_elementFactory);

    scriptElement = parser.Parse(script, visitor);
    return parser;
  }
}