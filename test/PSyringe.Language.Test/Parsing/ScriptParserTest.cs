using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Common.Language.Parsing;
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
    
    sut.Visitor.InjectionSites.Should().NotBeEmpty();
  }
  
  [Fact]
  public void Parse_CallsCreateScriptElement_WhenCalled() {
    var sut = MakeParserAndParse(ScriptTemplates.WithStartupFunction);
    
    sut.Script.Should().NotBeNull();
  }

  private ScriptParser MakeParserAndParse(string script) {
    var visitor = new ScriptVisitor();
    var parser = new ScriptParser(visitor);
    parser.Parse(script);
    return parser;
  }
}