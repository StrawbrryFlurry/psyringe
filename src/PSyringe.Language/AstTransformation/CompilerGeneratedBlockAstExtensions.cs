using System.Text;
using PSyringe.Language.Compiler.AstGeneration;
using static PSyringe.Language.Compiler.CompilerScriptText;

namespace PSyringe.Language.AstTransformation;

public static class CompilerGeneratedBlockAstExtensions {
  public static string ToStringFromAst(this CompilerGeneratedBlockAst ast) {
    var statements = ast.Statements?.ToStringFromAstJoinBy(NewLine);
    var traps = ast.Traps?.ToStringFromAstJoinBy(NewLine);

    var block = new StringBuilder();

    block.AppendLine(BlockOpen);
    block.AppendLine("{");

    if (statements is not null) {
      block.AppendLine(statements);
    }

    if (traps is not null) {
      block.AppendLine(traps);
    }
    
    block.AppendLine("}");
    block.Append(BlockClose);

    return block.ToString();
  }
}