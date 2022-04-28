using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class ForStatementAstExtensions {
  public static string ToStringFromAst(this ForStatementAst ast) {
    var initializer = ast.Initializer?.ToStringFromAst() ?? "";
    var condition = ast.Condition?.ToStringFromAst() ?? "";
    var iterator = ast.Iterator?.ToStringFromAst() ?? "";

    var body = ast.Body.ToStringFromAst();
    var label = ast.GetLabel();

    return $"{label}for ({initializer}; {condition}; {iterator}) {body}";
  }
}