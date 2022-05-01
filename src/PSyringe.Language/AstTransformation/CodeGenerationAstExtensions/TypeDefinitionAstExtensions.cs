using System.Management.Automation.Language;
using System.Text;
using static PSyringe.Language.AstTransformation.CodeGenConstants;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class TypeDefinitionAstExtensions {
  public static string ToStringFromAst(this TypeDefinitionAst ast) {
    var attributes = ast.Attributes.ToStringFromAstJoinBy(NewLine);
    var keyword = ast switch {
      {IsClass: true} => "class",
      {IsInterface: true} => "interface",
      {IsEnum: true} => "enum",
      _ => "class"
    };

    var name = ast.Name;

    var baseTypes = ast.BaseTypes.Select(t => t.ToStringFromAst().TrimStart('[').TrimEnd(']')).JoinBy(", ");
    var members = ast.Members.ToStringFromAstJoinBy(NewLine);

    var typeDefinition = new StringBuilder();

    if (!string.IsNullOrWhiteSpace(attributes)) {
      typeDefinition.AppendLine($"{attributes}");
    }

    typeDefinition.Append($"{keyword} {name}");

    if (!string.IsNullOrWhiteSpace(baseTypes)) {
      typeDefinition.Append(" : ");
      typeDefinition.Append(baseTypes);
    }

    typeDefinition.AppendLine(" {");

    if (!string.IsNullOrWhiteSpace(members)) {
      typeDefinition.AppendLine(members);
    }

    typeDefinition.AppendLine("}");

    return typeDefinition.ToString();
  }
}