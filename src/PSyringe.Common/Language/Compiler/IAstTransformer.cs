using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Compiler;

public interface IAstTransformer {
  public void ReplaceAst(ref ScriptBlockAst scriptBlock, Ast replacement);
}