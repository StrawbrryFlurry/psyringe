using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.Compiler.CompilerScriptText;

namespace PSyringe.Language.AstTransformation;

public static class ParamBlockAstExtensions {
  public static string ToStringFromAst(this ParamBlockAst ast) {
    var attributes = ast.Attributes.Select(a => a.ToStringFromAst()).ToList();
    var parameters = ast.Parameters.Select(a => a.ToStringFromAst()).ToList();

    // [Some()]
    // [Attributes()]
    // param(
    //  <Param1>,
    //  <Param2>,
    //  <Param3>
    // )
    var paramBlock = new StringBuilder();

    if (attributes.Any()) {
      paramBlock.AppendLine(string.Join(NewLine, attributes));
    }

    paramBlock.Append("param(");

    if (parameters.Any()) {
      paramBlock.Append(NewLine);
      paramBlock.Append(string.Join($",{NewLine}", parameters));
      // Formatting
      paramBlock.Append($"{NewLine})");
    }
    else {
      paramBlock.Append(')');
    }

    return paramBlock.ToString();
  }
}