using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using static PSyringe.Language.Test.AstTransformation.Utils.AstConstants;

namespace PSyringe.Language.Test.AstTransformation.Utils;

public static class MakeAstUtils {
  public static VariableExpressionAst Var(string name, bool isSplatted = false) {
    return new VariableExpressionAst(EmptyExtent, name, isSplatted);
  }

  public static ConstantExpressionAst Const(object value) {
    return new ConstantExpressionAst(EmptyExtent, value);
  }

  public static ConstantExpressionAst Const(string value) {
    return new StringConstantExpressionAst(EmptyExtent, value, StringConstantType.DoubleQuoted);
  }

  public static ConstantExpressionAst CmdStr(string value) {
    return new ConstantExpressionAst(EmptyExtent, value);
  }

  public static ExpressionAst Condition(ExpressionAst left, TokenKind op, ExpressionAst right) {
    return new BinaryExpressionAst(EmptyExtent, left, op, right, EmptyExtent);
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

  public static StatementBlockAst Block(TrapStatementAst[] traps, params StatementAst[] statements) {
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

  public static TrapStatementAst Trap(params StatementAst[] elements) {
    return new TrapStatementAst(EmptyExtent, null, Block(elements));
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

  public static IList<ExpressionAst> ExprList(params ExpressionAst[] expressions) {
    return expressions.ToList();
  }

  public static IList<AttributeBaseAst> AttrList(params AttributeBaseAst[] expressions) {
    return expressions.ToList();
  }

  public static IList<T> List<T>(params T[] expressions) {
    return expressions.ToList();
  }

  private static string GetTypeFullName<T>() {
    return GetTypeFullName(typeof(T));
  }

  private static string GetTypeFullName(Type type) {
    return type.FullName();
  }
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