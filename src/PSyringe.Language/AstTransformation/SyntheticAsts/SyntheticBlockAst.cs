using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SyntheticAsts;

public class SyntheticBlockAst : StatementBlockAst {
  public SyntheticBlockAst(
    IScriptExtent extent,
    IEnumerable<StatementAst>? statements,
    IEnumerable<TrapStatementAst>? traps
  ) : base(extent, statements, traps) {
  }
}