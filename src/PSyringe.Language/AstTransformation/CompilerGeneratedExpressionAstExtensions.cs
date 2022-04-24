using System.Text;
using PSyringe.Language.Compiler.AstGeneration;
using static PSyringe.Language.Compiler.CompilerScriptText;

namespace PSyringe.Language.AstTransformation; 

public static class CompilerGeneratedExpressionAstExtensions {
  public static string ToStringFromAst(this CompilerGeneratedExpressionAst ast) {
    var generatedExpression = ast.Expression.ToStringFromAst();

    var expression = new StringBuilder();
    expression.Append(InlineExpression);
    expression.Append(' ');
    expression.Append(generatedExpression);

    return expression.ToString();
  }
}