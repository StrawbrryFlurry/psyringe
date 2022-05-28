using PSyringe.Common.Language.Elements;
using PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

namespace PSyringe.Language.Test.Elements;

public static class TestElementExtensions {
  public static string ToStringFromAst(this ScriptElement element) {
    return element.Ast.Parent.ToStringFromAst();
  }
}