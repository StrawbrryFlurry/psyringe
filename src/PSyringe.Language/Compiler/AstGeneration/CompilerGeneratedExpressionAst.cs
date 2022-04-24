using System.Management.Automation.Language;

namespace PSyringe.Language.Compiler.AstGeneration; 

public class CompilerGeneratedExpressionAst : CommandExpressionAst {
  public CompilerGeneratedExpressionAst(IScriptExtent extent, ExpressionAst generatedExpression) 
    : base(extent, generatedExpression, null) {
  }
}