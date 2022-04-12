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

  protected Ast Ast { get; }
}