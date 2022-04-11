using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Elements.Base;

/// <summary>
///   Elements are an abstraction for a specific
///   type of AST nodes in a script. The Ast type defines
///   the type which this element works with.
/// </summary>
public interface IElement {
  public Ast Ast { get; }
}