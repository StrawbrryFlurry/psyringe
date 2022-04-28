using System.Management.Automation.Language;
using PSyringe.Language.Compiler;
using Xunit;

namespace PSyringe.Language.Test.AstTransformation;

public class CompilerAstExtensionsTest {
  [Fact]
  public void ReplaceAst_ShouldReplaceTheAst_WhenTheAstIsAValidAttributedExpressionOfTheScript() {
    var sb = ParseScript("[LogExpression()][string][Log()]$Foo = 10");
    var expressionAst = sb.FindOfType<AttributedExpressionAst>()!;

    var newChild = expressionAst.Child.CopyAs<AttributedExpressionAst>();
    var newAttribute = ((AttributedExpressionAst) expressionAst.Child).Attribute.CopyAs<AttributeBaseAst>();
    var newExpression = new AttributedExpressionAst(expressionAst.Extent, newAttribute, newChild);

    // var newSb = sb.ReplaceAst(ast => ast == expressionAst, (AttributedExpressionAst _) => newExpression);
    // 
    // newSb.FindOfType<AttributedExpressionAst>().Should().Be(newExpression);
  }

  [Fact]
  public void ReplaceAst_ShouldNotReplaceTheAst_WhenTheAstIsAValidAttributedExpressionOfTheScript() {
    var sb = ParseScript("[LogExpression()][string][Log()]$Foo = 10");
    var expressionAst = sb.FindOfType<AttributedExpressionAst>()!;

    // var newSb = sb.ReplaceAst(ast => ast == expressionAst, (AttributedExpressionAst _) => null);
    // 
    // var newAttributedExpressionAst = newSb.FindOfType<AttributedExpressionAst>()!;
    // // We cannot compare the references because the ReplaceAst method copies the AST
    // newAttributedExpressionAst.Attribute.TypeName.Extent.Text.Should().Be("LogExpression");
  }

  private ScriptBlockAst ParseScript(string script) {
    var sb = Parser.ParseInput(script, out var t, out var e);
    return sb;
  }
}