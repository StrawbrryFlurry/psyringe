using System.Management.Automation.Language;
using System.Text;

namespace PSyringe.Language.AstTransformation;

public static class ParameterAstExtensions {
  public static string ToStringFromAst(this ParameterAst ast) {
    var variable = ast.Name.ToStringFromAst();
    var attributes = ast.Attributes.Select(a => a.ToStringFromAst());
    var defaultValue = ast.DefaultValue?.ToStringFromAst();

    var sb = new StringBuilder();
    sb.Append(string.Join("", attributes));
    sb.Append(variable);

    if (defaultValue is null) {
      return sb.ToString();
    }

    sb.Append(" = ");
    sb.Append(defaultValue);

    return sb.ToString();
  }
}