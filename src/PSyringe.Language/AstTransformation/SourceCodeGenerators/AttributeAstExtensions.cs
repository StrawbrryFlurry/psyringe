using System.Management.Automation.Language;
using System.Text;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class AttributeAstExtensions {
  public static string ToStringFromAst(this AttributeAst ast) {
    // We use the reflection type rather than the full name to
    // make the resulting name more readable. We fall back to
    // the name in case the type is not found.
    var name = ast.TypeName.GetReflectionType()?.Name ?? ast.TypeName.FullName;
    var namedArguments = ast.NamedArguments.Select(a => a.ToStringFromAst()).ToList();
    var positionalArguments = ast.PositionalArguments.Select(a => a.ToStringFromAst()).ToList();

    var arguments = positionalArguments.Concat(namedArguments);

    var sb = new StringBuilder();
    // [ParameterAttribute(0, 1, NamedParameter = 10)]
    sb.Append($"[{name}(");

    sb.Append(string.Join(", ", arguments));

    sb.Append(")]");
    return sb.ToString();
  }
}