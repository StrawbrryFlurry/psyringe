using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Common.Language.Compiler;

public interface IAstTransformer {
  /// <summary>
  ///   Runs compiler transformation on a child
  ///   element.
  /// </summary>
  /// <param name="scriptElement"></param>
  public void TransformChild(ScriptElement scriptElement);

  public void ReplaceAst(ref ScriptBlockAst scriptBlock, Ast replacement);

  public void InsertStatement(ref ScriptBlockAst scriptBlock, StatementAst statement);
  public StatementAst CreateStatement();
}