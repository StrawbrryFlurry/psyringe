using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Elements;

public interface IScriptDefinition {
  public ScriptBlockAst ScriptBlock { get; }
  public IEnumerable<ScriptElement> Elements { get; }

  public void AddElement(ScriptElement element);
}