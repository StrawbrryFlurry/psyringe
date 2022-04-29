using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class AssignmentStatementAstExtensions {
  public static string ToStringFromAst(this AssignmentStatementAst ast) {
    var left = ast.Left.ToStringFromAst();
    var right = ast.Right.ToStringFromAst();
    var op = ast.Operator.Text();

    return $"{left} {op} {right}";
  }
}