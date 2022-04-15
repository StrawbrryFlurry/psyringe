using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Compiler;

public class CompiledScript : ICompiledScript {
  public ScriptBlockAst ScriptBlock { get; private set; }
  public IScriptDefinition ScriptDefinition { init; get; }

  public string GetScriptCode() {
    throw new NotImplementedException();
  }

  public IList<IScriptVariableDependency> Dependencies { init; get; } = new List<IScriptVariableDependency>();

  public void SetScriptBlock(ScriptBlockAst scriptBlock) {
    ScriptBlock = scriptBlock;
  }
}