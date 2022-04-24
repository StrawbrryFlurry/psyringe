using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Reflection;
using System.Runtime.Serialization;
using static PSyringe.Language.Test.AstTransformation.Utils.AstConstants;

namespace PSyringe.Language.Test.AstTransformation.Utils;

public static class MakeAstUtils {
  public static VariableExpressionAst Var(string name, bool isSplatted = false) {
    return new VariableExpressionAst(EmptyExtent, name, isSplatted);
  }

  public static ConstantExpressionAst Const(object value) {
    return new ConstantExpressionAst(EmptyExtent, value);
  }

  public static StringConstantExpressionAst Const(string value) {
    return new StringConstantExpressionAst(EmptyExtent, value, StringConstantType.DoubleQuoted);
  }

  public static ConstantExpressionAst CmdStr(string value) {
    return new ConstantExpressionAst(EmptyExtent, value);
  }

  public static ExpressionAst Condition(ExpressionAst left, TokenKind op, ExpressionAst right) {
    return new BinaryExpressionAst(EmptyExtent, left, op, right, EmptyExtent);
  }

  public static ExpressionAst Unary(ExpressionAst left, TokenKind op) {
    return new UnaryExpressionAst(EmptyExtent, op, left);
  }

  public static AttributeAst Attr<T>(
    IEnumerable<ExpressionAst>? arguments = null,
    IEnumerable<NamedAttributeArgumentAst>? namedArguments = null) {
    arguments ??= List<ExpressionAst>();
    namedArguments ??= List<NamedAttributeArgumentAst>();
    return new AttributeAst(EmptyExtent, MakeTypeName<T>(), arguments, namedArguments);
  }

  public static NamedAttributeArgumentAst
    NamedArg(string name, ExpressionAst value, bool expressionOmitted = false) {
    return new NamedAttributeArgumentAst(EmptyExtent, name, value, expressionOmitted);
  }

  public static ParameterAst Param(VariableExpressionAst variable, IList<AttributeBaseAst>? attributes = null,
    ExpressionAst? defaultValue = null) {
    return new ParameterAst(EmptyExtent, variable, attributes, defaultValue);
  }

  public static CommandAst Command(params CommandElementAst[] elements) {
    return new CommandAst(EmptyExtent, elements, TokenKind.Unknown, null);
  }

  public static StatementBlockAst EmptyBlock() {
    return new StatementBlockAst(EmptyExtent, new StatementAst[] { }, null);
  }

  public static StatementBlockAst Block(params StatementAst[] statements) {
    return new StatementBlockAst(EmptyExtent, statements, null);
  }

  public static StatementBlockAst Block(params CommandElementAst[] elements) {
    var statements = elements.Select(e => Statement(e));
    return new StatementBlockAst(EmptyExtent, statements, null);
  }

  public static StatementBlockAst Block(IEnumerable<TrapStatementAst> traps, params StatementAst[] statements) {
    return new StatementBlockAst(EmptyExtent, statements, traps);
  }

  public static StatementAst Statement(params CommandElementAst[] elements) {
    return Pipeline(Command(elements));
  }

  public static PipelineAst Pipeline(params CommandBaseAst[] commands) {
    return new PipelineAst(EmptyExtent, commands);
  }

  public static PipelineAst Pipeline(params CommandElementAst[] elements) {
    var commands = elements.Select(e => Command((CommandElementAst) e.Copy()));
    return new PipelineAst(EmptyExtent, commands);
  }

  public static PipelineBaseAst Assign(ExpressionAst left, ExpressionAst right) {
    return new AssignmentStatementAst(EmptyExtent, left, TokenKind.Equals, Statement(right), EmptyExtent);
  }

  public static TrapStatementAst Trap(params StatementAst[] elements) {
    return new TrapStatementAst(EmptyExtent, null, Block(elements));
  }

  public static FunctionDefinitionAst Function(
    string name,
    IEnumerable<ParameterAst>? parameters,
    ScriptBlockAst body
  ) {
    return new FunctionDefinitionAst(EmptyExtent, false, false, name, parameters, body);
  }

  public static Token Token(TokenKind kind) {
    //   This is a little hacky but we don't have a good way to get a
    //   token instance as both its constructor and InternalScriptExtent
    //   are internal.
    var tokenType = typeof(Token);
    var internalScriptExtent =
      tokenType.Assembly.GetType("System.Management.Automation.Language.InternalScriptExtent")!;
    var extent = FormatterServices.GetUninitializedObject(internalScriptExtent);
    var ctor = tokenType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).First();
    return (Token) ctor.Invoke(new[] {extent, kind, null});
  }

  public static TypeConstraintAst TypeAttr<T>() {
    return new TypeConstraintAst(EmptyExtent, typeof(T));
  }

  public static ITypeName MakeReflectionTypeName<T>() {
    return new ReflectionTypeName(typeof(T));
  }

  public static ITypeName MakeArrayTypeName<T>(int depth = 1) {
    return new ArrayTypeName(EmptyExtent, MakeTypeName<T>(), depth);
  }

  public static ITypeName MakeTypeName<T>() {
    return new TypeName(EmptyExtent, GetTypeFullName<T>());
  }

  public static ITypeName MakeTypeName(Type type) {
    return new TypeName(EmptyExtent, GetTypeFullName(type));
  }

  public static ITypeName MakeGenericTypeName<T>() {
    var genericArguments = typeof(T).GetGenericArguments();
    var genericTypes = genericArguments.Select(MakeTypeName);

    return new GenericTypeName(EmptyExtent,
      MakeTypeName<T>(), genericTypes);
  }

  public static TypeExpressionAst TypeExpression<T>() {
    return new TypeExpressionAst(EmptyExtent, MakeTypeName<T>());
  }

  public static IList<ExpressionAst> ExprList(params ExpressionAst[] expressions) {
    return expressions.ToList();
  }

  public static IList<AttributeBaseAst> AttrList(params AttributeBaseAst[] expressions) {
    return expressions.ToList();
  }

  public static IList<T> List<T>(params T[] expressions) {
    return expressions.ToList();
  }

  public static Tuple<T1, T2> Tuple<T1, T2>(T1 t1, T2 t2) {
    return new Tuple<T1, T2>(t1, t2);
  }

  private static string GetTypeFullName<T>() {
    return GetTypeFullName(typeof(T));
  }

  private static string GetTypeFullName(Type type) {
    return type.FullName();
  }

  # region ScriptBlockAstUtils

  public static ParamBlockAst ParamBlock(params ParameterAst[] parameters) {
    return new ParamBlockAst(EmptyExtent, null, parameters);
  }

  public static NamedBlockAst NamedBlock(TokenKind name, params StatementAst[] statements) {
    return new NamedBlockAst(EmptyExtent, name, Block(statements), false);
  }

  public static void SetParent(Ast ast) {
    var mockParent = FormatterServices.GetUninitializedObject(typeof(ScriptBlockAst));
    var setParentBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;
    var astParentSetter = typeof(Ast).GetProperty("Parent", setParentBindingFlags)!;
    astParentSetter.SetValue(ast, mockParent);
  }

  public static void ClearParent(Ast ast) {
    var setParentBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;
    var astParentSetter = typeof(Ast).GetProperty("Parent", setParentBindingFlags)!;
    astParentSetter.SetValue(ast, null);
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
  public static ScriptBlockAst ScriptBlock(
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

  public static ScriptBlockAst ScriptBlock(
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

  public static ScriptBlockAst ScriptBlock(
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

  public static void SetScriptBlockRequirements(ScriptBlockAst sb, ScriptRequirements requirements) {
    var setScriptBlockRequirementsBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;
    var scriptBlockRequirementsSetter =
      typeof(ScriptBlockAst).GetProperty("ScriptRequirements", setScriptBlockRequirementsBindingFlags)!;
    scriptBlockRequirementsSetter.SetValue(sb, requirements);
  }

  #endregion
}

internal static class InternalTypeExtensions {
  /// <summary>
  ///   Same as .FullName but omits the generic type info
  ///   System.Collection.Generic.List`1 -> System.Collection.Generic.List
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  internal static string FullName(this Type type) {
    var name = type.FullName!;

    if (type.IsGenericType) {
      return name.Substring(0, name.IndexOf('`'));
    }

    return name;
  }
}