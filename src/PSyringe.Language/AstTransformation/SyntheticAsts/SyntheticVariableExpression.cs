using System.Management.Automation.Language;
using static PSyringe.Language.AstTransformation.SyntheticAsts.SyntheticScriptExtent;

namespace PSyringe.Language.AstTransformation.SyntheticAsts;

public class SyntheticVariableExpression : VariableExpressionAst {
  protected SyntheticVariableExpression(IScriptExtent extent, string variablePath, bool splatted = false)
    : base(extent, variablePath, splatted) {
  }

  public static SyntheticVariableExpression Create(string variablePath, bool splatted = false) {
    var synthInstance = new SyntheticVariableExpression(EmptyScriptExtent, variablePath, splatted);
    return UpdateScriptExtent(synthInstance);
  }
}