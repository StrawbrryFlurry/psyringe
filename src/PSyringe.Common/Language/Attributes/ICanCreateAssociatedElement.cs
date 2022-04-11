using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements.Base;

namespace PSyringe.Common.Language.Attributes;

public interface ICanCreateAssociatedElement<T> where T : Ast {
  public IElement<T> CreateElement(T ast);
}