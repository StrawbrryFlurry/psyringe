using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation.SourceCodeGenerators;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation.SourceCodeGenerators;

public class CommandAstExtensionsTest {
  [Fact]
  public void ToStringFromAst_CommandParameterAst() {
    var sut = new CommandParameterAst(EmptyExtent, "Path", null, EmptyExtent);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("-Path");
  }

  [Fact]
  public void ToStringFromAst_WithParameter_CommandParameterAst() {
    var sut = new CommandParameterAst(EmptyExtent, "Path", Const("Arg"), EmptyExtent);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"-Path:{DoubleQuote("Arg")}");
  }

  [Fact]
  public void ToStringFromAst_MergingRedirectionAst() {
    var sut = new MergingRedirectionAst(EmptyExtent, RedirectionStream.Error, RedirectionStream.Output);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("2>&1");
  }

  [Fact]
  public void ToStringFromAst_FileRedirectionAst() {
    var sut = new FileRedirectionAst(EmptyExtent, RedirectionStream.Error, Const("File"), false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"2> {DoubleQuote("File")}");
  }

  [Fact]
  public void ToStringFromAst_OmitsDefaultStream_FileRedirectionAst() {
    var sut = new FileRedirectionAst(EmptyExtent, RedirectionStream.Output, Const("File"), false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"> {DoubleQuote("File")}");
  }

  [Fact]
  public void ToStringFromAst_Append_FileRedirectionAst() {
    var sut = new FileRedirectionAst(EmptyExtent, RedirectionStream.Output, Const("File"), true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($">> {DoubleQuote("File")}");
  }

  [Fact]
  public void ToStringFromAst_CommandAst() {
    var sut = new CommandAst(EmptyExtent, List(CmdStr("Command")), TokenKind.Unknown, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Command");
  }

  [Fact]
  public void ToStringFromAst_WithRedirection_CommandAst() {
    var redirection = new FileRedirectionAst(EmptyExtent, RedirectionStream.Output, Const("File"), false);
    var sut = new CommandAst(EmptyExtent, List(CmdStr("Command")), TokenKind.Unknown, List(redirection));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"Command > {DoubleQuote("File")}");
  }

  [Fact]
  public void ToStringFromAst_WithInvocation_CommandAst() {
    var sut = new CommandAst(EmptyExtent, List(CmdStr("Command")), TokenKind.Ampersand, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("& Command");
  }

  [Fact]
  public void ToStringFromAst_WithParameter_CommandAst() {
    var parameter = new CommandParameterAst(EmptyExtent, "Path", null, EmptyExtent);
    var elements = List<CommandElementAst>(CmdStr("Command"), parameter);
    var sut = new CommandAst(EmptyExtent, elements, TokenKind.Unknown, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Command -Path");
  }

  [Fact]
  public void ToStringFromAst_WithParameterAndArg_CommandAst() {
    var parameter = new CommandParameterAst(EmptyExtent, "Path", null, EmptyExtent);
    var argument = Const("Arg");
    var elements = List<CommandElementAst>(CmdStr("Command"), parameter, argument);
    var sut = new CommandAst(EmptyExtent, elements, TokenKind.Unknown, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(@"Command -Path ""Arg""");
  }
}