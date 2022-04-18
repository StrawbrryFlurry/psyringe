using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class PipelineAstExtensions {
  public static string ToStringFromAst(this PipelineAst ast) {
    var pipeline = ast.PipelineElements.ToStringFromAstJoinBy(" | ")!;

    if (ast.Background) {
      return $"{pipeline} &";
    }

    return pipeline;
  }
}