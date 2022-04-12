using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Compiler;

public class CompiledScript : ICompiledScript {
  public IScriptDefinition ScriptDefinition { init; get; }
  public IList<IScriptVariableDependency> Dependencies { get; } = new List<IScriptVariableDependency>();

  public string GetScriptBlockText() {
    throw new NotImplementedException();
  }
}