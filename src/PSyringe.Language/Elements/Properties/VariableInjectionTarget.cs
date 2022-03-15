using System.Management.Automation.Language;
using PSyringe.Common.Language.Attributes;
using PSyringe.Common.Language.Parsing.Elements.Properties;
using PSyringe.Language.Extensions;

namespace PSyringe.Language.Elements.Properties;

public class VariableInjectionTarget : IInjectionTarget {
  public readonly AttributeBaseAst Attribute;
  public readonly ExpressionAst ChildExpression;
  public readonly StatementAst? VariableStatement;


  private readonly AttributedExpressionAst _ast;

  public VariableInjectionTarget(AttributedExpressionAst ast) {
    _ast = ast;
    ChildExpression = _ast.Child;
    VariableStatement = _ast.Parent as StatementAst;
    Attribute = _ast.Attribute;
  }

  public bool HasDefaultValue() {
    // We assume that the variable has a default value
    // if it's part of an assigment expression.
    // [Inject()]$var = <value>;
    var isPartOfAssignment = GetVariableAssignmentStatement() is not null;
    return isPartOfAssignment;
  }

  public T GetInjectAttributeInstance<T>() where T : class, IInjectionTargetAttribute {
    var attribute = _ast.Attribute as AttributeAst;
    // attribute.PositionalArguments
    // 
    // var type = attribute.GetReflectionAttributeType();
    // var x = type.GetConstructors();
    // 
    // x.Where(c => c.IsPublic).Where(c => c.GetParameters().Where(e => e.))
    // return (new InjectAttribute("") as T)!;
    return default;
  }

  internal Type GetVariableTypeConstraint() {
    var typeConstraintExpressionAst = GetVariableTypeConstraintExpressionAst();

    if (typeConstraintExpressionAst is null) {
      return null;
    }

    var typeConstraintAst = typeConstraintExpressionAst.Attribute;
    return typeConstraintAst.GetReflectionAttributeType();
  }

  private VariableExpressionAst? GetAttributedVariableExpression() {
    var typeConstraintExpression = GetVariableTypeConstraintExpressionAst();

    if (typeConstraintExpression is null) {
      return ChildExpression as VariableExpressionAst;
    }

    return typeConstraintExpression.Child as VariableExpressionAst;
  }

  private ConvertExpressionAst? GetVariableTypeConstraintExpressionAst() {
    return ChildExpression as ConvertExpressionAst;
  }

  private AssignmentStatementAst? GetVariableAssignmentStatement() {
    return VariableStatement as AssignmentStatementAst;
  }
}