using System.Management.Automation;
using System.Management.Automation.Language;

namespace PSyringe.Language.AstTransformation.CodeGenerationAstExtensions;

public static class ErrorStatementAstExtensions {
  public static string ToStringFromAst(this ErrorStatementAst ast) {
    throw new PSInvalidOperationException();
  }
}