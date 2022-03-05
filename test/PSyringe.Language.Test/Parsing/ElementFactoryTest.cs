using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Language.Parsing.Elements;
using PSyringe.Common.Test.Scripts;
using PSyringe.Language.Parsing;
using PSyringe.Language.Test.Parsing.Utils;
using Xunit;

namespace PSyringe.Language.Test.Parsing;

public class ElementFactoryTest {
  
  [Fact]
  public void CreateScript_ShouldCreateInstanceOfScriptElement_WhenCalled() {
    var visitor = MakeVisitorAndVisit(ScriptTemplates.EmptyScript);
    
    var script = ElementFactory.CreateScript(visitor);

    script.Should().BeAssignableTo<IScriptElement>();
  }
  
  [Fact]
  public void CreateInjectionSite_ShouldCreateInstanceOfInjectionSiteElement_WhenCalled() {
    var visitor = MakeVisitorAndVisit(ScriptTemplates.WithStartupFunction);

    var injectionSite = visitor.InjectionSites.First();
    var site = ElementFactory.CreateInjectionSite(injectionSite);

    site.Should().BeAssignableTo<IInjectionSiteElement>();
  }


  private IScriptVisitor MakeVisitorAndVisit(string script) {
    var ast = ParsingUtil.ParseScript(script);
    var visitor = new ScriptVisitor();
    visitor.Visit(ast);
    return visitor;
  }
  
}