using System.Management.Automation.Language;
using PSyringe.Common.Language.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Language.Compiler;

public class ScriptCompiler : IScriptCompiler {
  public ICompiledScript CompileScriptDefinition(IScriptDefinition definition) {
    var script = new CompiledScript {
      ScriptDefinition = definition,
      Dependencies = new List<IScriptVariableDependency>()
    };

    var sb = definition.ScriptBlock;

    foreach (var element in definition.Elements) {
      RemoveElementAttributeFromAst(ref sb, element);
      TransformElementAst(ref sb,);
    }

    return script;
  }

  private void RemoveElementAttributeFromAst(ref ScriptBlockAst scriptBlock, ScriptElement element) {
  }

  private void TransformElementAst(ref ScriptBlockAst scriptBlock, ScriptElement element) {
    var transformer = new ScriptTransformer();
    scriptBlock.element.TransformAst(transformer);
  }
}

internal class ScriptTransformer {
}