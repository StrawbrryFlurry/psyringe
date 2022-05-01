using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class IndexExpressionAstExtensions {
  public static string ToStringFromAst(this IndexExpressionAst ast) {
    var nullConditional = ast.NullConditional ? "?" : "";
    var target = ast.Target.ToStringFromAst();
    var index = ast.Index.ToStringFromAst();

    return $"{target}{nullConditional}[{index}]";
  }
}