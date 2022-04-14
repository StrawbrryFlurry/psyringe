using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.Compiler;
using PSyringe.Language.Extensions;
using PSyringe.Language.Parsing;
using Xunit;

namespace PSyringe.Language.Test.Compiler;

public class ScriptCompilerTest {
  [Fact]
  public void CompileScript_ConvertsScriptElement_ToCompiledScript() {
    var scriptElement = MakeScriptElement("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var compiledScript = sut.CompileScriptDefinition(scriptElement);

    compiledScript.Should().BeAssignableTo<ICompiledScript>();
  }

  [Fact]
  public void CompileScript_ConvertsScriptElement_ToCompiledScriptWithScriptElement() {
    var scriptElement = MakeScriptElement("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var script = sut.CompileScriptDefinition(scriptElement);

    script.ScriptDefinition.Should().Be(scriptElement);
  }

  [Fact]
  public void UpdateFunctionDefinitions_RemovesAllPSyringeAttributes_InCompiledScriptAst() {
    var scriptElement = MakeScriptElement(@"
function TestInjectionSite() {
  [InjectionSite()]
  param()
}
");

    var sut = new ScriptCompiler();
    var script = sut.CompileScriptDefinition(scriptElement);

    var functionDefinition = script.ScriptBlock.FindOfType<FunctionDefinitionAst>()!;
    functionDefinition.GetAttributes().Should().BeEmpty();
  }

  private IScriptDefinition MakeScriptElement(string script) {
    var elementFactory = new ElementFactory();
    var parser = new ScriptParser(elementFactory);

    return parser.Parse(script);
  }
}