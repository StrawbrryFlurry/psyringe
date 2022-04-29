using System.Management.Automation.Language;

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
  ///   Utility method to check whether the AST
  ///   of this element is of a certain type.
  /// </summary>
  /// <param name="ast"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  protected bool IsAst<T>(out T? ast) where T : Ast {
    if (Ast is T) {
      ast = (T) Ast;
      return true;
    }

    ast = default;
    return false;
  }

  /// <summary>
  ///   Method that can be used to transform an AST that this element represents
  ///   to it's intended form.
  ///   <code>
  /// // An element representing this:
  /// [Inject([ILogger])]$Logger;
  /// // would be transformed to this:
  /// $Logger = $script:ɵɵprov_GLOBAL_Logger_inj_ILogger;
  /// </code>
  /// </summary>
  /// <param name="source">The source AST element that this element represents</param>
  /// <typeparam name="T"></typeparam>
  /// <returns>A replacement for the element or `null` if it should not be replaced</returns>
  public abstract Ast? TransformAst<T>(T source) where T : Ast;
}