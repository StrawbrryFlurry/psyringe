using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing;

public interface IAttributedScriptElement<T> where T : Ast {
  public T Ast { init; get; }
  public AttributeBaseAst Attribute { init; get; }
}