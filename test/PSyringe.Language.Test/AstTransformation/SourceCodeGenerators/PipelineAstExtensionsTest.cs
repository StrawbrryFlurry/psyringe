using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation.SourceCodeGenerators;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation.SourceCodeGenerators;

public class PipelineAstExtensionsTest {
  [Fact]
  public void ToStringFromAst_PipelineAst() {
    var sut = new PipelineAst(EmptyExtent, Command(CmdStr("Command")), false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Command");
  }

  [Fact]
  public void ToStringFromAst_MultipleElements_PipelineAst() {
    var commands = List(Command(CmdStr("Command")), Command(CmdStr("PipelineCommand")));
    var sut = new PipelineAst(EmptyExtent, commands, false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Command | PipelineCommand");
  }

  [Fact]
  public void ToStringFromAst_Background_PipelineAst() {
    var sut = new PipelineAst(EmptyExtent, Command(CmdStr("Command")), true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Command &");
  }

  [Fact]
  public void ToStringFromAst_PipelineChainAst() {
    var sut = new PipelineChainAst(
      EmptyExtent,
      Pipeline(Command(CmdStr("Foo"))),
      Pipeline(Command(CmdStr("Bar"))),
      TokenKind.AndAnd
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Foo && Bar");
  }

  [Fact]
  public void ToStringFromAst_Background_PipelineChainAst() {
    var sut = new PipelineChainAst(
      EmptyExtent,
      Pipeline(Command(CmdStr("Foo"))),
      Pipeline(Command(CmdStr("Bar"))),
      TokenKind.AndAnd,
      true
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Foo && Bar &");
  }

  [Fact]
  public void ToStringFromAst_AssignmentStatementAst() {
    var sut = new AssignmentStatementAst(EmptyExtent, Var("Variable"), TokenKind.Equals, Statement(Const(1)),
      EmptyExtent);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Variable")} = 1");
  }
}