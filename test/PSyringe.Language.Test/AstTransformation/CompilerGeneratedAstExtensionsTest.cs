using FluentAssertions;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.Compiler.AstGeneration;
using Xunit;
using  PSyringe.Language.Compiler;
using static PSyringe.Language.Test.AstTransformation.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation; 

public class CompilerGeneratedAstExtensionsTest {
  [Fact]
  public void ToStringFromAst_CompilerGeneratedExpressionAst() {
    var sut = new CompilerGeneratedExpressionAst(EmptyExtent, Var("Variable"));
    var actual = sut.ToStringFromAst();
    
    actual.Should().Be($"{CompilerScriptText.InlineExpression} {VarS("Variable")}"); 
  }
  
  [Fact]
  public void ToStringFromAst_CompilerGeneratedBlockAst() {
    var sut = new CompilerGeneratedBlockAst(EmptyExtent, List(Statement(Var("Variable"))), null);
    var actual = sut.ToStringFromAst();
    
    actual.Should().Be(CompilerScriptText.BlockOpen
                       + NewLine + "{" 
                       + NewLine + VarS("Variable")
                       + NewLine + "}" 
                       + NewLine + 
                       CompilerScriptText.BlockClose); 
  }
}