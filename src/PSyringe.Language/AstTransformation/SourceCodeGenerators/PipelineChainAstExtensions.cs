using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class PipelineChainAstExtensions {
  public static string ToStringFromAst(this PipelineChainAst ast) {
    var left = ast.LhsPipelineChain.ToStringFromAst();
    var chainOp = ast.Operator.Text();
    var right = ast.RhsPipeline.ToStringFromAst();

    var background = ast.Background ? /* Include whitespace */ " &" : "";

    // Command && Command
    return $"{left} {chainOp} {right}{background}";
  }
}