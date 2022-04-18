using System.Management.Automation.Language;
using static PSyringe.Language.Compiler.CompilerScriptText;

namespace PSyringe.Language.AstTransformation;

public static class ConstantExpressionAstExtensions {
  public static string ToStringFromAst(this ConstantExpressionAst ast) {
    var value = ast.Value;

    // The value is only a boolean when an attribute
    // has an implicit parameter e.g. [Parameter(ValueFromPipeline)]
    // in which case the value is true.
    if (ast.StaticType == typeof(bool)) {
      return TrueVariable;
    }

    return value.ToString()!;
  }
}