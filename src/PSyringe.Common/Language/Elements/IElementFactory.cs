using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements.Base;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Common.Language.Elements;

public interface IElementFactory {
  public IScriptElement CreateScript(ScriptBlockAst scriptBlockAst);
  public T CreateElement<T, TA>(IAttributedScriptElement<TA> attributedElement) where T : IElement where TA : Ast;
}