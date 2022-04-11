using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing.Elements.Base;

/// <summary>
///   A base class for all variable-related operations
///   in a script. Because variables are marked by using
///   attributes, we specify the base type as an AttributedExpression
///   rather than a VariableExpression. The underlying expression
///   will always be a VariableExpression though.
/// </summary>
public interface IVariableElement : IElement<AttributedExpressionAst> {
}