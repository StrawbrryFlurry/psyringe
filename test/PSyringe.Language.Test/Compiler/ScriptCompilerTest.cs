using System.Linq;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.Attributes;
using PSyringe.Language.Compiler;
using PSyringe.Language.Parsing;
using Xunit;

namespace PSyringe.Language.Test.Compiler;

public class ScriptCompilerTest {
  [Fact]
  public void CompileScript_ConvertsScriptElement_ToCompiledScript() {
    var scriptDefinition = MakeScriptDefinition("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var compiledScript = sut.CompileScriptDefinition(scriptDefinition);

    compiledScript.Should().BeAssignableTo<ICompiledScript>();
  }

  [Fact]
  public void CompileScript_ConvertsScriptElement_ToCompiledScriptWithScriptElement() {
    var scriptDefinition = MakeScriptDefinition("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var script = sut.CompileScriptDefinition(scriptDefinition);

    script.ScriptDefinition.Should().Be(scriptDefinition);
  }

  [Fact]
  public void
    RemoveElementAttributeFromAst_RemovesTheElementAttributeFromTheScriptBlockAst_WhenElementIsAttributedExpression() {
    var scriptDefinition = MakeScriptDefinition("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var script = sut.CompileScriptDefinition(scriptDefinition);

    script.ScriptBlock.FindOfType<AttributedExpressionAst>().Should().BeNull();
    script.ScriptBlock.FindOfType<VariableExpressionAst>().Should().NotBeNull();
  }

  [Fact]
  public void
    RemoveElementAttributeFromAst_RemovesTheElementAttributeFromTheScriptBlockAst_WhenElementIsAttributedExpressionWithMultipleAttributes() {
    var scriptDefinition = MakeScriptDefinition("[Inject([ILogger])][LogExpression()]$Logger;");
    var sut = new ScriptCompiler();

    var script = sut.CompileScriptDefinition(scriptDefinition);

    var attributedExpressions = script.ScriptBlock.FindAllOfType<AttributedExpressionAst>().ToList();
    attributedExpressions.Should().HaveCount(1);
    attributedExpressions.First().Attribute.GetType().Should().Be(typeof(LogExpressionAttribute));
  }

  [Fact]
  public void RemoveElementAttributeFromAst_ReplacesTheExtentOfTheExpression_WhenElementIsAttributedExpression() {
    var scriptDefinition = MakeScriptDefinition("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var script = sut.CompileScriptDefinition(scriptDefinition);

    // ToString returns the script extent
    script.ScriptBlock.ToString().Should().Be("$Logger;");
  }

  //   [Fact]
  //   public void UpdateFunctionDefinitions_RemovesAllPSyringeAttributes_InCompiledScriptAst() {
  //     var scriptElement = MakeScriptElement(@"
  // function TestInjectionSite() {
  //   [InjectionSite()]
  //   param()
  // }
  // ");
  // 
  //     var sut = new ScriptCompiler();
  //     var script = sut.CompileScriptDefinition(scriptElement);
  // 
  //     var functionDefinition = script.ScriptBlock.FindOfType<FunctionDefinitionAst>()!;
  //     functionDefinition.GetAttributes().Should().BeEmpty();
  //   }

  private IScriptDefinition MakeScriptDefinition(string script) {
    var elementFactory = new ElementFactory();
    var parser = new ScriptParser(elementFactory);

    return parser.Parse(script);
  }
}