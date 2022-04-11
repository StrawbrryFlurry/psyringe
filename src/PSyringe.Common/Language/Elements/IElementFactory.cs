using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing.Elements.Base;

namespace PSyringe.Common.Language.Parsing;

public interface IElementFactory {
  public IScriptElement CreateScript(ScriptBlockAst scriptBlockAst);
  public T CreateElement<T, TA>(TA ast, Type attribute) where T : IElement<TA> where TA : Ast;
}