using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing.Elements.Base;

/// <summary>
///   Elements are an abstraction for a specific
///   type of AST nodes in a script. The Ast type defines
///   the type which this element works with.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IElement<out T> where T : Ast {
  public T Ast { get; }
}