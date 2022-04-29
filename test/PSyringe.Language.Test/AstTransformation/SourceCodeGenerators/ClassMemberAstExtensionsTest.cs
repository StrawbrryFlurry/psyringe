using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using FluentAssertions;
using PSyringe.Language.AstTransformation.SourceCodeGenerators;
using Xunit;
using static System.Management.Automation.Language.MethodAttributes;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.MakeAstUtils;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.AstConstants;
using static PSyringe.Language.Test.AstTransformation.SourceCodeGenerators.Utils.StringConstants;

namespace PSyringe.Language.Test.AstTransformation.SourceCodeGenerators;

public class ClassMemberAstExtensionsTest {
  #region PropertyMemberAst

  [Fact]
  public void ToStringFromAst_PropertyMemberAst() {
    var sut = Property("Property");
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_TypeConstraint_PropertyMemberAst() {
    var sut = Property<string>("Property");
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"[string]{VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_TypeConstraintRegularAttribute_PropertyMemberAst() {
    var sut = Property<string>("Property", List(Attr<AliasAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{AttrS<AliasAttribute>()}[string]{VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_Attribute_PropertyMemberAst() {
    var sut = Property("Property", List(Attr<AliasAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{AttrS<AliasAttribute>()}{VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_Attributes_PropertyMemberAst() {
    var sut = Property("Property", List(Attr<AliasAttribute>(), Attr<AliasAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{AttrS<AliasAttribute>()}{AttrS<AliasAttribute>()}{VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_AccessModifier_PropertyMemberAst() {
    var sut = Property("Property", null, PropertyAttributes.Hidden);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"hidden {VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_AccessModifiers_PropertyMemberAst() {
    var sut = Property("Property", null, PropertyAttributes.Hidden | PropertyAttributes.Static);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"static hidden {VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_Literal_PropertyMemberAst() {
    var sut = Property("Property", null, PropertyAttributes.Literal);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Property;");
  }

  [Fact]
  public void ToStringFromAst_AccessModifierTypeConstraint_PropertyMemberAst() {
    var sut = Property<string>("Property", null, PropertyAttributes.Hidden);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"hidden [string]{VarS("Property")};");
  }

  [Fact]
  public void ToStringFromAst_InitialValue_PropertyMemberAst() {
    var sut = Property("Property", null, PropertyAttributes.None, 1);
    var actual = sut.ToStringFromAst();

    actual.Should().Be($"{VarS("Property")} = 1;");
  }

  private PropertyMemberAst Property(
    string name,
    IEnumerable<AttributeAst>? attributes = null,
    PropertyAttributes visibilityModifiers = PropertyAttributes.None,
    object? value = null
  ) {
    var initialValue = value is null ? null : Const(value);
    return new PropertyMemberAst(EmptyExtent, name, null, attributes, visibilityModifiers, initialValue);
  }

  private PropertyMemberAst Property<T>(
    string name,
    IEnumerable<AttributeAst>? attributes = null,
    PropertyAttributes visibilityModifiers = PropertyAttributes.None,
    object? value = null
  ) {
    var initialValue = value is null ? null : Const(value);
    return new PropertyMemberAst(EmptyExtent, name, TypeAttr<T>(), attributes, visibilityModifiers, initialValue);
  }

  #endregion

  # region FunctionMemberAst

  [Fact]
  public void ToStringFromAst_FunctionMemberAst() {
    var sut = Method(Function("ToString", null, ScriptBlock(true)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_ReturnType_FunctionMemberAst() {
    var sut = Method<string>(Function("ToString", null, ScriptBlock(true)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("[string] ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Attribute_FunctionMemberAst() {
    var sut = Method(Function("ToString", null, ScriptBlock(true)), List(Attr<AliasAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(AttrS<AliasAttribute>()
                       + NewLine +
                       "ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_Attributes_FunctionMemberAst() {
    var sut = Method(Function("ToString", null, ScriptBlock(true)),
      List(Attr<AliasAttribute>(), Attr<AliasAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(AttrS<AliasAttribute>()
                       + NewLine +
                       AttrS<AliasAttribute>()
                       + NewLine +
                       "ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_AttributeReturnType_FunctionMemberAst() {
    var sut = Method<string>(Function("ToString", null, ScriptBlock(true)), List(Attr<AliasAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(AttrS<AliasAttribute>()
                       + NewLine +
                       "[string] ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_VisibilityModifier_FunctionMemberAst() {
    var sut = Method(Function("ToString", null, ScriptBlock(true)), null, Hidden);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("hidden ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_VisibilityModifiers_FunctionMemberAst() {
    var sut = Method(Function("ToString", null, ScriptBlock(true)), null, Hidden | Static);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("static hidden ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_VisibilityModifierReturnType_FunctionMemberAst() {
    var sut = Method<string>(Function("ToString", null, ScriptBlock(true)), null, Hidden);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("hidden [string] ToString() {"
                       + NewLine +
                       "}");
  }

  [Fact]
  public void ToStringFromAst_BaseCtor_FunctionMemberAst() {
    var baseCtorExpression = new BaseCtorInvokeMemberExpressionAst(EmptyExtent, EmptyExtent, null);
    var commandElement = new CommandExpressionAst(EmptyExtent, baseCtorExpression, null);

    var scriptBlockWithBaseExpression = ScriptBlock(null, Block(commandElement));
    ClearParent(scriptBlockWithBaseExpression);
    var sut = Method(Function("Animal", null, scriptBlockWithBaseExpression));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("Animal() : base() {"
                       + NewLine +
                       "}");
  }

  private FunctionMemberAst Method(
    FunctionDefinitionAst? functionDefinitionAst,
    IEnumerable<AttributeAst>? attributes = null,
    MethodAttributes visibilityModifiers = None
  ) {
    return new FunctionMemberAst(EmptyExtent, functionDefinitionAst, null, attributes, visibilityModifiers);
  }

  private FunctionMemberAst Method<T>(
    FunctionDefinitionAst? functionDefinitionAst = null,
    IEnumerable<AttributeAst>? attributes = null,
    MethodAttributes visibilityModifiers = None
  ) {
    return new FunctionMemberAst(EmptyExtent, functionDefinitionAst, TypeAttr<T>(), attributes, visibilityModifiers);
  }

  #endregion

  #region TypeDefinitionAst

  [Fact]
  public void ToStringFromAst_TypeDefinitionAst() {
    var sut = TypeDefinition("Animal");
    var actual = sut.ToStringFromAst();

    actual.Should().Be("class Animal {"
                       + NewLine +
                       "}"
                       + NewLine);
  }


  [Fact]
  public void ToStringFromAst_BaseType_TypeDefinitionAst() {
    var sut = TypeDefinition("Animal", null, null, TypeAttributes.Class, List(TypeAttr<Array>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("class Animal : array {"
                       + NewLine +
                       "}"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_BaseTypes_TypeDefinitionAst() {
    var sut = TypeDefinition("Animal", null, null, TypeAttributes.Class, List(TypeAttr<Array>(), TypeAttr<Array>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("class Animal : array, array {"
                       + NewLine +
                       "}"
                       + NewLine);
  }


  [Fact]
  public void ToStringFromAst_Attribute_TypeDefinitionAst() {
    var sut = TypeDefinition("Animal", List(Attr<CmdletAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(AttrS<CmdletAttribute>()
                       + NewLine +
                       "class Animal {"
                       + NewLine +
                       "}"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_Attributes_TypeDefinitionAst() {
    var sut = TypeDefinition("Animal", List(Attr<CmdletAttribute>(), Attr<CmdletAttribute>()));
    var actual = sut.ToStringFromAst();

    actual.Should().Be(AttrS<CmdletAttribute>()
                       + NewLine +
                       AttrS<CmdletAttribute>()
                       + NewLine +
                       "class Animal {"
                       + NewLine +
                       "}"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_PropertyMember_TypeDefinitionAst() {
    var members = List(
      Property("Name")
    );
    var sut = TypeDefinition("Animal", null, members);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("class Animal {"
                       + NewLine + $"{VarS("Name")};"
                       + NewLine +
                       "}"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_MethodMember_TypeDefinitionAst() {
    var members = List(
      Method(Function("ToString", null, ScriptBlock(true)))
    );
    var sut = TypeDefinition("Animal", null, members);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("class Animal {"
                       + NewLine + "ToString() {"
                       + NewLine + "}"
                       + NewLine +
                       "}"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_MethodPropertyMember_TypeDefinitionAst() {
    var members = List<MemberAst>(
      Property("Name"),
      Method(Function("ToString", null, ScriptBlock(true)))
    );
    var sut = TypeDefinition("Animal", null, members);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("class Animal {"
                       + NewLine + $"{VarS("Name")};"
                       + NewLine + "ToString() {"
                       + NewLine + "}"
                       + NewLine +
                       "}"
                       + NewLine);
  }

  [Fact]
  public void ToStringFromAst_Enum_TypeDefinitionAst() {
    var members = List<MemberAst>(
      Property("Tiger", null, PropertyAttributes.Literal)
    );
    var sut = TypeDefinition("AnimalType", null, members, TypeAttributes.Enum);
    var actual = sut.ToStringFromAst();

    actual.Should().Be("enum AnimalType {"
                       + NewLine + "Tiger;"
                       + NewLine +
                       "}"
                       + NewLine);
  }

  private TypeDefinitionAst TypeDefinition(
    string name,
    IEnumerable<AttributeAst>? attributes = null,
    IEnumerable<MemberAst>? members = null,
    TypeAttributes typeType = TypeAttributes.None,
    IEnumerable<TypeConstraintAst>? baseTypes = null
  ) {
    return new TypeDefinitionAst(EmptyExtent, name, attributes, members, typeType, baseTypes);
  }

  #endregion

  # region BaseCtorInvokeMemberExpressionAst

  [Fact]
  public void ToStringFromAst_BaseCtorInvokeMemberExpressionAst() {
    var sut = new BaseCtorInvokeMemberExpressionAst(EmptyExtent, EmptyExtent, ExprList());
    var actual = sut.ToStringFromAst();

    actual.Should().Be("base()");
  }

  [Fact]
  public void ToStringFromAst_Argument_BaseCtorInvokeMemberExpressionAst() {
    var sut = new BaseCtorInvokeMemberExpressionAst(EmptyExtent, EmptyExtent, ExprList(Const(1)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("base(1)");
  }

  [Fact]
  public void ToStringFromAst_Arguments_BaseCtorInvokeMemberExpressionAst() {
    var sut = new BaseCtorInvokeMemberExpressionAst(EmptyExtent, EmptyExtent, ExprList(Const(1), Const(2)));
    var actual = sut.ToStringFromAst();

    actual.Should().Be("base(1, 2)");
  }

  # endregion
}