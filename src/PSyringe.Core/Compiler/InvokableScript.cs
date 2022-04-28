using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Core.Compiler;

public class InvokableScript {
  public ScriptBlockAst ScriptBlock { get; private set; }
  public IScriptDefinition ScriptDefinition { init; get; }

  public IList<IScriptVariableDependency> Dependencies { init; get; } = new List<IScriptVariableDependency>();

  public string GetScriptCode() {
    throw new NotImplementedException();
  }

  public void SetScriptBlock(ScriptBlockAst scriptBlock) {
    ScriptBlock = scriptBlock;
  }
}