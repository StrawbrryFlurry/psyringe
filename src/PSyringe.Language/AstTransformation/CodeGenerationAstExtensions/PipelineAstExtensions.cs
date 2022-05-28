using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class PipelineAstExtensions {
  public static string ToStringFromAst(this PipelineAst ast) {
    var pipeline = ast.PipelineElements.ToStringFromAstJoinBy(" | ")!;

    if (ast.Background) {
      return $"{pipeline} &";
    }

    return pipeline;
  }

  public static bool ReplaceChildCore(this PipelineAst ast, CommandBaseAst child, Ast replacement) {
    // If the replacement is an entirely new statement; we
    // replace this pipeline with the statement.
    if (replacement is StatementAst and not CommandBaseAst) {
      return ast.ReplaceChild(ast, replacement);
    }

    var idx = ast.PipelineElements.IndexOf(child);

    if (idx == -1) {
      return false;
    }

    var newElements = ast.PipelineElements.ToList();
    newElements[idx] = (CommandBaseAst) replacement;

    ast.SetPrivateProperty(nameof(ast.PipelineElements), newElements);
    return true;
  }
}