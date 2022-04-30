using PSyringe.Common.DI;

namespace PSyringe.Language.Compiler;

public class ScriptVariableDependency<T> {
  public IScriptProvider Provider { get; }
  public string VariableName { get; }

  public ScriptVariableDependency(IScriptProvider provider, string variableName) {
    Provider = provider;
    VariableName = variableName;
  }
}