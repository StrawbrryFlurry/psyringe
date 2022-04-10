using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.Compiler;
using Xunit;

namespace PSyringe.Language.Test.Compiler;

public class CompilerAstExtensionsTest {
  [Fact]
  public void ReplaceAst_ShouldReplaceTheAstReference_WhenTheAstIsAValidAttributedExpressionOfTheScript() {
    var sb = ParseScript("[LogExpression()][string][Log()]$Foo = 10");
    var expressionAst = sb.FindOfType<AttributedExpressionAst>()!;

    var newChild = expressionAst.Child.CopyAs<AttributedExpressionAst>();
    var newAttribute = ((AttributedExpressionAst) expressionAst.Child).Attribute.CopyAs<AttributeBaseAst>();
    var newExpression = new AttributedExpressionAst(expressionAst.Extent, newAttribute, newChild);

    var newSb = sb.ReplaceAst(expressionAst, newExpression);
    
    newSb.FindOfType<AttributedExpressionAst>().Should().Be(newExpression);
  }

  private ScriptBlockAst ParseScript(string script) {
    var sb = Parser.ParseInput(script, out var t, out var e);
    return sb;
  }
}