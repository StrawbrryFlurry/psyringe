using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SyntheticAsts;

public class SyntheticExpressionAst : CommandExpressionAst {
  public SyntheticExpressionAst(IScriptExtent extent, ExpressionAst generatedExpression)
    : base(extent, generatedExpression, null) {
  }
}