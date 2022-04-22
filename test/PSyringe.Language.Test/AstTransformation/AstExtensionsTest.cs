using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using System.Reflection;
using System.Runtime.Serialization;
using FluentAssertions;
using PSyringe.Language.AstTransformation;
using PSyringe.Language.Test.Parsing.Utils;
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
  # region AttributeAst

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

  #endregion

  # region ParameterAst

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

  #endregion

  #region StatementBlockAst

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

  #endregion

  #region ScriptBlockAst

  [Fact]
  public void ToStringFromAst_EmptyRoot_ScriptBlockAst() {
    var sut = ScriptBlock(true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("");
  }

  [Fact]
  public void ToStringFromAst_SingleImplicitBlockRoot_ScriptBlockAst() {
    var sut = ScriptBlock(null, Block(Pipeline(Const("Hi!"))), true);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(DoubleQuote("Hi!") + NewLine);
  }

  [Fact]
  public void ToStringFromAst_RootParam_ScriptBlockAst() {
    var sut = ScriptBlock(true, null, ParamBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("param()" + NewLine);
  }

  [Fact]
  public void ToStringFromAst_RootSingleBlock_ScriptBlockAst() {
    var sut = ScriptBlock(true, null, null, NamedBlock(TokenKind.Begin));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("begin {" + NewLine +
                       "}" + NewLine);
  }

  [Fact]
  public void ToStringFromAst_RootAllBlocks_ScriptBlockAst() {
    var sut = ScriptBlock(true, null, null,
      NamedBlock(TokenKind.Begin),
      NamedBlock(TokenKind.Process),
      NamedBlock(TokenKind.End),
      NamedBlock(TokenKind.Clean),
      NamedBlock(TokenKind.Dynamicparam)
    );
    var actual = sut.ToStringFromAst();

    actual.Should().Be("dynamicparam {" + NewLine +
                       "}" + NewLine +
                       "begin {" + NewLine +
                       "}" + NewLine +
                       "process {" + NewLine +
                       "}" + NewLine +
                       "end {" + NewLine +
                       "}" + NewLine +
                       "clean {" + NewLine +
                       "}" + NewLine);
  }

  [Fact]
  public void ToStringFromAst_RootBlockAndParam_ScriptBlockAst() {
    var sut = ScriptBlock(true, null, ParamBlock(), NamedBlock(TokenKind.Begin));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("param()" + NewLine +
                       "begin {" + NewLine +
                       "}" + NewLine);
  }

  /// <summary>
  ///   We only need to check using statements for "root" Script Blocks
  ///   because they're not allowed in any other context.
  /// </summary>
  [Fact]
  public void ToStringFromAst_RootUsingStatements_ScriptBlockAst() {
    var usingStatements = List(
      new UsingStatementAst(EmptyExtent, UsingStatementKind.Namespace, Const("System.Reflection"))
    );
    var sut = ScriptBlock(true, usingStatements);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"using namespace {DoubleQuote("System.Reflection")};"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_RootUsingStatementsParamBlock_ScriptBlockAst() {
    var usingStatements = List(
      new UsingStatementAst(EmptyExtent, UsingStatementKind.Namespace, Const("System.Reflection"))
    );
    var sut = ScriptBlock(true, usingStatements, ParamBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"using namespace {DoubleQuote("System.Reflection")};"
                       + NewLine +
                       "param()"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_RootScriptRequirements_ScriptBlockAst() {
    // All setters are internal so this is
    // the easiest way to get access to all of them.
    var requirements = @"#requires -Assembly System.Reflection;
#requires -Module @{ ModuleName = 'PSReadLine'; ModuleVersion = '0.12.0' };
#requires -PSEdition Core;
#requires -PSSnapin DiskSnapin -Version 1.2;
#requires -RunAsAdministrator;
#requires -Version 7.0;";
    var requirementsAst = ParsingUtil.ParseScript(requirements).ScriptRequirements;
    var usingStatements = List(
      new UsingStatementAst(EmptyExtent, UsingStatementKind.Namespace, Const("System.Reflection"))
    );
    var sut = ScriptBlock(true, usingStatements);
    SetScriptBlockRequirements(sut, requirementsAst);
    var actual = sut.ToStringFromAst();

    actual.Should().Be(requirements
                       + NewLine +
                       $"using namespace {DoubleQuote("System.Reflection")};"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_Empty_ScriptBlockAst() {
    var sut = ScriptBlock(false);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("{"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Block_ScriptBlockAst() {
    var sut = ScriptBlock(null, null, NamedBlock(TokenKind.Begin));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("{" +
                       NewLine + "begin {" +
                       NewLine + "}" +
                       NewLine +
                       "}");
  }


  [Fact]
  public void ToStringFromAst_Param_ScriptBlockAst() {
    var sut = ScriptBlock(null, ParamBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("{" +
                       NewLine + "param()" +
                       NewLine +
                       "}");
  }


  [Fact]
  public void ToStringFromAst_BlockAndParam_ScriptBlockAst() {
    var sut = ScriptBlock(null, ParamBlock(), NamedBlock(TokenKind.Begin));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("{" +
                       NewLine + "param()" +
                       NewLine + "begin {" +
                       NewLine + "}" + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_StatementsOnly_ScriptBlockAst() {
    var sut = ScriptBlock(null, EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("{"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_ParamBlockStatementsOnly_ScriptBlockAst() {
    var sut = ScriptBlock(ParamBlock(), EmptyBlock());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("{"
                       + NewLine +
                       "param()"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Attribute_ScriptBlockAst() {
    var sut = ScriptBlock(List(Attr<ParameterAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{AttrS<ParameterAttribute>()}{{"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Attributes_ScriptBlockAst() {
    var sut = ScriptBlock(
      List(
        Attr<ParameterAttribute>(),
        Attr<CmdletAttribute>()
      ));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{AttrS<ParameterAttribute>()}{AttrS<CmdletAttribute>()}{{"
                       + NewLine +
                       "}");
  }

  #endregion

  # region ScriptBlockAstUtils

  private static ParamBlockAst ParamBlock(params ParameterAst[] parameters) {
    return new ParamBlockAst(EmptyExtent, null, parameters);
  }

  private static NamedBlockAst NamedBlock(TokenKind name, params StatementAst[] statements) {
    return new NamedBlockAst(EmptyExtent, name, Block(statements), false);
  }

  private static void SetParent(Ast ast) {
    var mockParent = FormatterServices.GetUninitializedObject(typeof(ScriptBlockAst));
    var setParentBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;
    var astParentSetter = typeof(Ast).GetProperty("Parent", setParentBindingFlags)!;
    astParentSetter.SetValue(ast, mockParent);
  }

  /// <summary>
  ///   The ScriptBlockAst has a bunch of different overloads for the constructor
  ///   that we don't really care about. The main ones are those that take
  ///   a single list of statements and the ones that provide all sections
  ///   of the script block.
  ///   We can ignore `isFilter` and `isConfiguration` because they
  ///   are taken care of by the FunctionDefinitionAst / ConfigurationStatementAst
  ///   as which they are parsed.
  /// </summary>
  private static ScriptBlockAst ScriptBlock(
    bool isRoot = false,
    IEnumerable<UsingStatementAst>? usingStatements = null,
    ParamBlockAst? paramBlock = null,
    NamedBlockAst? beginBlock = null,
    NamedBlockAst? processBlock = null,
    NamedBlockAst? endBlock = null,
    NamedBlockAst? cleanBlock = null,
    NamedBlockAst? dynamicParamBlock = null
  ) {
    var sb = new ScriptBlockAst(
      EmptyExtent,
      usingStatements,
      paramBlock,
      beginBlock,
      processBlock,
      endBlock,
      cleanBlock,
      dynamicParamBlock
    );

    if (!isRoot) {
      SetParent(sb);
    }

    return sb;
  }

  private static ScriptBlockAst ScriptBlock(
    IEnumerable<AttributeAst>? attributes = null,
    ParamBlockAst? paramBlock = null,
    NamedBlockAst? beginBlock = null,
    NamedBlockAst? processBlock = null,
    NamedBlockAst? endBlock = null,
    NamedBlockAst? cleanBlock = null,
    NamedBlockAst? dynamicParamBlock = null
  ) {
    var sb = new ScriptBlockAst(
      EmptyExtent,
      null,
      // Attributes make only sense in child script blocks
      // as there is no way to declare them on a root ScriptBlock
      attributes,
      paramBlock,
      beginBlock,
      processBlock,
      endBlock,
      cleanBlock,
      dynamicParamBlock
    );

    SetParent(sb);

    return sb;
  }

  private static ScriptBlockAst ScriptBlock(
    ParamBlockAst? paramBlock,
    StatementBlockAst? statements,
    bool isRoot = false
  ) {
    var sb = new ScriptBlockAst(
      EmptyExtent,
      paramBlock,
      statements,
      // IsFilter doesn't matter for these test cases
      false
    );

    if (!isRoot) {
      SetParent(sb);
    }

    return sb;
  }

  private static void SetScriptBlockRequirements(ScriptBlockAst sb, ScriptRequirements requirements) {
    var setScriptBlockRequirementsBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;
    var scriptBlockRequirementsSetter =
      typeof(ScriptBlockAst).GetProperty("ScriptRequirements", setScriptBlockRequirementsBindingFlags)!;
    scriptBlockRequirementsSetter.SetValue(sb, requirements);
  }

  #endregion
}