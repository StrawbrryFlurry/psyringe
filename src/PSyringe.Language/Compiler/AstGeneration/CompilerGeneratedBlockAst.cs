using System.Management.Automation.Language;

namespace PSyringe.Language.Compiler.AstGeneration; 

public class CompilerGeneratedBlockAst : StatementBlockAst {
  public CompilerGeneratedBlockAst(
    IScriptExtent extent,
    IEnumerable<StatementAst>? statements,
    IEnumerable<TrapStatementAst>? traps
    ) : base(extent, statements, traps) {
  }
}