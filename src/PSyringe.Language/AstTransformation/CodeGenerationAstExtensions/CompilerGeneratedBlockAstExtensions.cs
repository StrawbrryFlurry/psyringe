using System.Text;
using PSyringe.Language.AstTransformation.SyntheticAsts;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class CompilerGeneratedBlockAstExtensions {
  public static string ToStringFromAst(this SyntheticBlockAst ast) {
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