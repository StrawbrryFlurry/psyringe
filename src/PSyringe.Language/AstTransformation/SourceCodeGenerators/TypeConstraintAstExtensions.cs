using System.Management.Automation.Language;

namespace PSyringe.Language.CodeGen.SourceCodeGenerators;

public static class TypeConstraintAstExtensions {
  public static string ToStringFromAst(this TypeConstraintAst ast) {
    var typeString = ast.TypeName.ToStringFromAst();
    return typeString;
  }
}