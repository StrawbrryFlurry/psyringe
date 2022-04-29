using System.Management.Automation.Language;
using System.Text;
using static System.Management.Automation.Language.PropertyAttributes;

namespace PSyringe.Language.AstTransformation.SourceCodeGenerators;

public static class PropertyMemberAstExtensions {
  public static string ToStringFromAst(this PropertyMemberAst ast) {
    var name = ast.Name;
    var value = ast.InitialValue?.ToStringFromAst();
    var typeConstraint = ast.PropertyType?.ToStringFromAst() ?? "";
    var attributes = ast.Attributes?.ToStringFromAstJoinBy("") ?? "";
    var accessModifiers =
      ast.PropertyAttributes.GetAllSetEnumFlags(None | Public | Literal);

    // The casing doesn't matter. This is just to make it consistent with other keywords.
    var accessModifierString = accessModifiers.Select(a => a.ToLower()).JoinBy(" ");

    var property = new StringBuilder();

    if (!string.IsNullOrWhiteSpace(accessModifierString)) {
      property.Append(accessModifierString);
      property.Append(' ');
    }

    property.Append(attributes);
    property.Append(typeConstraint);

    // Literals are not variables and therefore don't have a $ prefix.
    if (!ast.PropertyAttributes.HasFlag(Literal)) {
      property.Append('$');
    }

    property.Append(name);

    if (value is not null) {
      property.Append(" = ");
      property.Append(value);
    }

    property.Append(';');
    return property.ToString();
  }
}