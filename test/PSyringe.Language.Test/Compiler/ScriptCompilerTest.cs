using FluentAssertions;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.Compiler;
using PSyringe.Language.Parsing;
using Xunit;

namespace PSyringe.Language.Test.Compiler;

public class ScriptCompilerTest {
  [Fact]
  public void CompileScript_ConvertsScriptElement_ToCompiledScript() {
    var scriptElement = MakeScriptElement("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var compiledScript = sut.CompileScriptElement(scriptElement);

    compiledScript.Should().BeAssignableTo<ICompiledScript>();
  }

  [Fact]
  public void CompileScript_ConvertsScriptElement_ToCompiledScriptWithScriptElement() {
    var scriptElement = MakeScriptElement("[Inject([ILogger])]$Logger;");
    var sut = new ScriptCompiler();

    var script = sut.CompileScriptElement(scriptElement);

    script.ScriptDefinition.Should().Be(scriptElement);
  }

  public void InsertInjectionSites_AddsInjectionSiteFromScript_InCompiledScriptAst() {
    var scriptElement = MakeScriptElement(@"
function TestInjectionSite() {
  [InjectionSite()]
  param()
}
");
    var sut = new ScriptCompiler();

    var script = sut.CompileScriptElement(scriptElement);
  }

  private IScriptDefinition MakeScriptElement(string script) {
    var elementFactory = new ElementFactory();
    var parser = new ScriptParser(elementFactory);
    var visitor = new ScriptParserVisitor();

    return parser.Parse(script, visitor);
  }
}