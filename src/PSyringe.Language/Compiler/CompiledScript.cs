using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Language.Compiler;

public class CompiledScript : ICompiledScript {
  public IScriptElement ScriptElement { init; get; }
  public IList<IScriptVariableDependency> Dependencies { get; } = new List<IScriptVariableDependency>();

  public string GetScriptBlockText() {
    throw new NotImplementedException();
  }
}