using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SyntheticAsts;

public class SyntheticVariableExpression : VariableExpressionAst {
  public SyntheticVariableExpression(IScriptExtent extent, string variablePath, bool splatted = false)
    : base(extent, variablePath, splatted) {
  }
}