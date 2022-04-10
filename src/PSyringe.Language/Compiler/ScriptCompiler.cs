using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Language.Compiler;

public class ScriptCompiler : IScriptCompiler {
  public ICompiledScript CompileScriptElement(IScriptElement scriptElement) {
    var script = new CompiledScript {
      ScriptElement = scriptElement
    };

    return script;
  }
}