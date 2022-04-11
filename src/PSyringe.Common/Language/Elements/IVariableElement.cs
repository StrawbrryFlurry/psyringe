using PSyringe.Common.Language.Elements.Base;

namespace PSyringe.Common.Language.Elements;

/// <summary>
///   A base class for all variable-related operations
///   in a script. Because variables are marked by using
///   attributes, we specify the base type as an AttributedExpression
///   rather than a VariableExpression. The underlying expression
///   will always be a VariableExpression though.
/// </summary>
public interface IVariableElement : IElement {
}