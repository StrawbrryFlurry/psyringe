using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Core.Compiler;

public class ScriptCompiler {
  public InvokableScript CompileScript(IScriptDefinition definition, IScriptTransformer[] transformers) {
    var script = new InvokableScript {
      ScriptDefinition = definition,
      Dependencies = new List<IScriptVariableDependency>()
    };

    foreach (var transformer in transformers) {
      transformer.Transform(ref definition);
    }

    // RemoveElementAttributeFromAst(ref sb, element);
    // TransformElementAst(ref sb, element);
    return script;
  }
}