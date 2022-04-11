using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Language.Parsing;

public struct AttributedScriptElement<T> : IAttributedScriptElement<T> where T : Ast {
  public T Ast { init; get; }
  public AttributeBaseAst Attribute { init; get; }
}