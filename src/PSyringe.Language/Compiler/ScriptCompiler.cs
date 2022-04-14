using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Compiler;

public class ScriptCompiler : IScriptCompiler {
  public ICompiledScript CompileScriptDefinition(IScriptDefinition scriptDefinition) {
    var script = new CompiledScript {
      ScriptDefinition = scriptDefinition
    };

    var functionElements = scriptDefinition.Elements.Where(e => e.Equals(1));


    return script;
  }

  private void UpdateAllFunctionDefinitionsInAst(ref ScriptBlockAst scriptBlock) {
  }
}

internal class ScriptTransformer {
}