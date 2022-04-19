using System.Management.Automation;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation;
using Xunit;
using static PSyringe.Language.Test.AstTransformation.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation;

/// <summary>
///   Generic AST elements that don't belong to
///   any specific category and derive directly from `Ast`.
/// </summary>
public class AstExtensionsTest {
  [Fact]
  public void ToStringFromAst_NamedAttributeArgumentAst() {
    var sut = NamedArg("Name", Const(One));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"Name = {One}");
  }

  [Fact]
  public void ToStringFromAst_WithoutExpression_NamedAttributeArgumentAst() {
    var sut = NamedArg("Name", Const(One), true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Name");
  }

  [Fact]
  public void ToStringFromAst_AttributeAst() {
    var sut = Attr<ParameterAttribute>();
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[{nameof(ParameterAttribute)}()]");
  }

  [Fact]
  public void ToStringFromAst_NamedArgument_AttributeAst() {
    var sut = Attr<ParameterAttribute>(null, List(NamedArg("Mandatory", Var(True))));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[{nameof(ParameterAttribute)}(Mandatory = {VarS(True)})]");
  }

  [Fact]
  public void ToStringFromAst_NamedArgumentWithoutExpression_AttributeAst() {
    var sut = Attr<ParameterAttribute>(null, List(NamedArg("Mandatory", Var(True), true)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[{nameof(ParameterAttribute)}(Mandatory)]");
  }

  [Fact]
  public void ToStringFromAst_NamedArguments_AttributeAst() {
    var sut = Attr<ParameterAttribute>(null, List(
      NamedArg("Mandatory", Var(True)), NamedArg("Position", Const(Zero)))
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[{nameof(ParameterAttribute)}(Mandatory = {VarS(True)}, Position = {Zero})]");
  }

  [Fact]
  public void ToStringFromAst_PositionalArgument_AttributeAst() {
    var sut = Attr<ParameterAttribute>(List(Const(Zero)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[{nameof(ParameterAttribute)}({Zero})]");
  }

  [Fact]
  public void ToStringFromAst_PositionalArguments_AttributeAst() {
    var sut = Attr<ParameterAttribute>(List(Const(Zero), Const(One)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[{nameof(ParameterAttribute)}({Zero}, {One})]");
  }

  [Fact]
  public void ToStringFromAst_PositionalAndNamedArguments_AttributeAst() {
    var sut = Attr<ParameterAttribute>(List(Const(One)), List(NamedArg("Mandatory", Const(true))));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[{nameof(ParameterAttribute)}({One}, Mandatory = {VarS(True)})]");
  }

  [Fact]
  public void ToStringFromAst_ParameterAst() {
    var sut = Param(Var(VariableName));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS(VariableName)}");
  }

  /// <summary>
  ///   We don't need to test all the attribute scenarios because
  ///   they're covered by the AttributeAstTests.
  /// </summary>
  [Fact]
  public void ToStringFromAst_WithAttribute_ParameterAst() {
    var sut = Param(Var(VariableName), AttrList(TypeAttr<string>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[string]{VarS(VariableName)}");
  }

  [Fact]
  public void ToStringFromAst_WithAttributes_ParameterAst() {
    var sut = Param(Var(VariableName),
      AttrList(TypeAttr<string>(), Attr<ParameterAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[string]{AttrS<ParameterAttribute>()}{VarS(VariableName)}");
  }

  [Fact]
  public void ToStringFromAst_WithDefaultValue_ParameterAst() {
    var sut = Param(Var(VariableName), AttrList(), Const(1));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS(VariableName)} = {One}");
  }

  [Fact]
  public void ToStringFromAst_WithAttributeAndDefaultValue_ParameterAst() {
    var sut = Param(Var(VariableName), AttrList(TypeAttr<string>()), Const(1));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[string]{VarS(VariableName)} = {One}");
  }

  [Fact]
  public void ToStringFromAst_ParamBlockAst() {
    var sut = new ParamBlockAst(EmptyExtent, null, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("param()");
  }

  [Fact]
  public void ToStringFromAst_WithAttribute_ParamBlockAst() {
    var sut = new ParamBlockAst(EmptyExtent, List(Attr<CmdletBindingAttribute>()), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{AttrS<CmdletBindingAttribute>()}{NewLine}" +
                       "param()");
  }

  [Fact]
  public void ToStringFromAst_WithAttributes_ParamBlockAst() {
    var sut = new ParamBlockAst(EmptyExtent, List(Attr<CmdletBindingAttribute>(), Attr<OutputTypeAttribute>()), null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{AttrS<CmdletBindingAttribute>()}{NewLine}" +
                       $"{AttrS<OutputTypeAttribute>()}{NewLine}" +
                       "param()");
  }

  [Fact]
  public void ToStringFromAst_WithParam_ParamBlockAst() {
    var sut = new ParamBlockAst(EmptyExtent, null, List(Param(Var(VariableName))));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"param({NewLine}" +
                       $"{VarS(VariableName)}{NewLine}" +
                       ")");
  }

  [Fact]
  public void ToStringFromAst_WithParams_ParamBlockAst() {
    var sut = new ParamBlockAst(EmptyExtent, null, List(Param(Var(VariableName)), Param(Var(VariableName))));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"param({NewLine}" +
                       $"{VarS(VariableName)},{NewLine}" +
                       $"{VarS(VariableName)}{NewLine}" +
                       ")");
  }

  #region ScriptBlock

  [Fact]
  public void ToStringFromAst_NamedBlockAst() {
    var sut = new NamedBlockAst(EmptyExtent, TokenKind.Begin, EmptyBlock(), false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"begin {{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Unnamed_NamedBlockAst() {
    var sut = new NamedBlockAst(EmptyExtent, TokenKind.Process, EmptyBlock(), true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{{{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Statement_NamedBlockAst() {
    var block = Block(
      Const("Statement")
    );
    var sut = new NamedBlockAst(EmptyExtent, TokenKind.Begin, block, false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"begin {{{NewLine}" +
                       $"{DoubleQuote("Statement")};{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_TrapStatement_NamedBlockAst() {
    var block = Block(
      List(Trap(Statement(Const("Error")))
      ));
    var sut = new NamedBlockAst(EmptyExtent, TokenKind.Begin, block, false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"begin {{{NewLine}" +
                       $"trap {{{NewLine}" +
                       $"{DoubleQuote("Error")};{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_TrapAndStatement_NamedBlockAst() {
    var block = Block(
      List(Trap(Statement(Const("Error")))),
      Statement(Const("Statement"))
    );

    var sut = new NamedBlockAst(EmptyExtent, TokenKind.Begin, block, false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"begin {{{NewLine}" +
                       $"{DoubleQuote("Statement")};{NewLine}" +
                       $"trap {{{NewLine}" +
                       $"{DoubleQuote("Error")};{NewLine}" +
                       $"}}{NewLine}" +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_ScriptBlockAst() {
    var sut = new ScriptBlockAst(EmptyExtent, ParamBlock(), null, null, null, null);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{{{NewLine}" +
                       "}");
  }

  private ParamBlockAst ParamBlock(params ParameterAst[] parameters) {
    return new ParamBlockAst(EmptyExtent, null, parameters);
  }

  private NamedBlockAst NamedBlock(TokenKind name, params StatementAst[] statements) {
    return new NamedBlockAst(EmptyExtent, name, Block(statements), false);
  }

  #endregion
}