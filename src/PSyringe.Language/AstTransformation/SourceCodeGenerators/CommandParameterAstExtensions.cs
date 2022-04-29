using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class CommandParameterAstExtensions {
  public static string ToStringFromAst(this CommandParameterAst ast) {
    var argument = ast.Argument;
    var parameterName = ast.ParameterName;

    if (argument is null) {
      return $"-{parameterName}";
    }

    var argumentString = argument.ToStringFromAst();
    // Arguments such as `-Path "Foo"` are not handled by this AST.
    return $"-{parameterName}:{argumentString}";
  }
}