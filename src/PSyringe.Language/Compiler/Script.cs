using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Language.Compiler;

public class Script : IScript {
  public IScriptElement Element { get; }
  public IList<IScriptVariableDependency> Dependencies { get; } = new List<IScriptVariableDependency>();
  public string Name { get; }

  public string GetScriptBlockText() {
    throw new NotImplementedException();
  }
}