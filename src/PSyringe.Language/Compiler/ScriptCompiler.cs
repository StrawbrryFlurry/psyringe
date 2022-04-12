using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Compiler;

public class ScriptCompiler : IScriptCompiler {
  public ICompiledScript CompileScriptElement(IScriptDefinition scriptElement) {
    var script = new CompiledScript {
      ScriptDefinition = scriptElement
    };

    return script;
  }
}