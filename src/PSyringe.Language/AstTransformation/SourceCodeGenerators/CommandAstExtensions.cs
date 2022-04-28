using System.Management.Automation.Language;
using System.Text;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class CommandAstExtensions {
  public static string ToStringFromAst(this CommandAst ast) {
    var invocationOperator = ast.InvocationOperator;
    var commandElements = ast.CommandElements.ToStringFromAstJoinBy(" ");
    var redirections = ast.Redirections.ToStringFromAstJoinBy(" ");

    var command = new StringBuilder();

    if (invocationOperator != TokenKind.Unknown) {
      command.Append(invocationOperator.Text());
      command.Append(' ');
    }

    if (commandElements is not null) {
      command.Append(commandElements);
    }

    if (redirections is not null) {
      command.Append(' ');
      command.Append(redirections);
    }

    return command.ToString();
  }
}