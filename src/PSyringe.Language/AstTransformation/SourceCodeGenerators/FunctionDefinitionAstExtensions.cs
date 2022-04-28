using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class FunctionDefinitionAstExtensions {
  public static string ToStringFromAst(this FunctionDefinitionAst ast) {
    var keyword = GetFunctionKeywordUsed(ast);
    var functionName = ast.Name;
    var body = ast.Body.ToStringFromAst();
    var parameters = ast.Parameters?.ToStringFromAstJoinBy(", ");

    var parameterString = parameters is null ? "" : $" ({parameters})";

    return $"{keyword} {functionName}{parameterString} {body}";
  }

  private static string GetFunctionKeywordUsed(FunctionDefinitionAst ast) {
    return ast switch {
      {IsFilter: true} => "filter",
      {IsWorkflow: true} => "workflow",
      _ => "function"
    };
  }
}