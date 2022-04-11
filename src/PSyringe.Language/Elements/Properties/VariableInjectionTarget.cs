using System.Management.Automation.Language;
using PSyringe.Language.Extensions;
using PSyringe.Language.TypeLoader;

namespace PSyringe.Language.Elements.Properties;

public class VariableInjectionTarget {
  private readonly AttributedExpressionAst _ast;
  public readonly AttributeAst Attribute;

  public VariableInjectionTarget(AttributedExpressionAst ast) {
    _ast = ast;
    // Elements that extend this class can't have a
    // TypeConstraint as their attribute so we can
    // safely assume that the attribute of the expression
    // is an AttributeAst. 
    Attribute = (AttributeAst) _ast.Attribute;
  }

  public bool HasDefaultValue() {
    // We assume that the variable has a default value;
    // if it's part of an assigment expression.
    // [Inject()]$var = <value>;
    var isPartOfAssignment = GetVariableAssignmentStatement() is not null;
    return isPartOfAssignment;
  }

  private AssignmentStatementAst? GetVariableAssignmentStatement() {
    return _ast.GetAstInTreeAssignableToType<AssignmentStatementAst>();
  }

  public T GetInjectAttributeInstance<T>() where T : Attribute {
    return AttributeTypeLoader.CreateAttributeInstanceFromAst<T>(Attribute);
  }

  public VariableExpressionAst? GetAttributedVariableExpression() {
    return _ast.GetAstInTreeAssignableToType<VariableExpressionAst>();
  }

  public Type? GetVariableTypeConstraint() {
    var typeConstraintExpressionAst = GetTypeConstraintAst();
    return typeConstraintExpressionAst?.GetAttributeType();
  }

  private TypeConstraintAst? GetTypeConstraintAst() {
    var convertExpressionAst = _ast.GetAstInTreeAssignableToType<ConvertExpressionAst>();
    return convertExpressionAst?.Type;
  }
}