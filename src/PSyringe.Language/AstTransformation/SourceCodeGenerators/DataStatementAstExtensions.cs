using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class DataStatementAstExtensions {
  public static string ToStringFromAst(this DataStatementAst ast) {
    var allowedCommands = ast.CommandsAllowed?.ToStringFromAstJoinBy(", ");
    var variable = ast.Variable;
    var body = ast.Body.ToStringFromAst();

    var commandString = allowedCommands is null ? "" : $" -SupportedCommand {allowedCommands}";

    return $"data {variable}{commandString} {body}";
  }
}