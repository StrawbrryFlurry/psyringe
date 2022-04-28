using System.Management.Automation.Language;
using PSyringe.Language.Test.Parsing.Utils;
using Xunit;

namespace PSyringe.Language.Test.AstTransformation;

public class AstTransformerTest {
  [Fact]
  public void ReplaceScriptExtent_ReplacesExtentTextInAst() {
    // var sb = MakeVariableExpressionScript();
    // var sut = new ScriptTransformer();

    // sut.ReplaceScriptExtent();
  }

  private VariableExpressionAst MakeReplacementVariableExpressionAst(Ast ast) {
    /*var variableExpressionAst = new VariableExpressionAst(
      ast.Extent,);
*/
    //  return variableExpressionAst;
    return default;
  }

  private ScriptBlockAst MakeVariableExpressionScript() {
    return ParsingUtil.ParseScript("[Inject([ILogger])]$Logger");
  }
}