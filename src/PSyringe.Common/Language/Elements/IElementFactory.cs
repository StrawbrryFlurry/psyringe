using System.Management.Automation.Language;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Common.Language.Elements;

public interface IElementFactory {
  public IScriptDefinition CreateScript(ScriptBlockAst scriptBlockAst);
  public ScriptElement CreateElement<T>(IAttributedScriptElement<T> element) where T : Ast;
}