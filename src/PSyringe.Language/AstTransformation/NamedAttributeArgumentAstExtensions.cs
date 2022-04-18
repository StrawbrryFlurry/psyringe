using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation;

public static class NamedAttributeArgumentAstExtensions {
  public static string ToStringFromAst(this NamedAttributeArgumentAst ast) {
    if (ast.ExpressionOmitted) {
      return ast.ArgumentName;
    }

    var name = ast.ArgumentName;
    var value = ast.Argument.ToStringFromAst();

    return $"{name} = {value}";
  }
}