using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;

namespace PSyringe.Common.Language.Elements;

/// <summary>
///   A script element is an abstraction for any part of
///   a script (The AST of that part in the script)
///   that was processed by the parser. An element may
///   be an attributed variable declaration or a function.
///   The following snipped:
///   <code>
/// [Inject([ILogger])]$Logger;
/// </code>
///   Might be converted into an `InjectElement` with the
///   AttributedExpressionAst of the `Inject` Attribute.
/// </summary>
public abstract class ScriptElement {
  protected ScriptElement(Ast ast) {
    Ast = ast;
  }

  protected ScriptElement(Ast ast, AttributeAst attribute) {
    Ast = ast;
    Attribute = attribute;
  }

  /// <summary>
  ///   The Ast in the script that this element represents.
  /// </summary>
  public Ast Ast { get; }

  /// <summary>
  ///   The attribute through which the Ast was marked
  ///   to be of this element type.
  ///   In the example below, the Attribute would be `InjectAttribute`
  ///   <code>
  ///    [Inject([ILogger])]$Logger;
  ///  </code>
  ///   Note that in case of VariableExpressions, the Attribute is only
  ///   the AttributeAst of the AttributedExpression, not the
  ///   AttributedExpressionAst with the child expression that was attributed.
  ///   Use <see cref="Ast" /> to get access to the AttributedExpressionAst.
  /// </summary>
  public AttributeAst? Attribute { get; }

  /// <summary>
  ///   Method used by the compiler to update the ScriptBlockAst
  /// </summary>
  public abstract Ast TransformAst(IAstTransformer transformer);
}