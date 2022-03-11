using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing.Elements.Base;

public interface IElement<out T> where T : Ast {
  public T Ast { get; }
}