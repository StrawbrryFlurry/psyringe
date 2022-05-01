using System.Management.Automation.Language;
using PSyringe.Common.Compiler;

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

  protected ScriptElement(Ast ast) {
    Ast = ast;
  }

  protected ScriptElement(Ast ast, AttributeAst attribute) {
    Ast = ast;
    Attribute = attribute;
  }

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
  ///   Method that can be used to transform an AST that this element represents.
  ///   <code>
  /// // An element representing this:
  /// [Inject([ILogger])]$Logger;
  /// // would be transformed to this:
  /// $Logger = $script:ɵɵprov_GLOBAL_Logger_inj_ILogger;
  /// </code>
  ///   In order to apply updates to the script AST, use the properties on the element's
  ///   see <see cref="Ast" />. property. This should always point to a valid node within
  ///   the ScriptBlock. While we don't have the ability to verify this, transforming an AST
  ///   node should never break out a child or parent node from the Tree.
  ///   <b>
  ///     If the transform
  ///     step updates any node in the AST, it's relatives need to be updated as well.
  ///   </b>
  /// </summary>
  /// <param name="transformer">The transformer that was used to transform this element.</param>
  public abstract void TransformAst(IScriptTransformer transformer);
}