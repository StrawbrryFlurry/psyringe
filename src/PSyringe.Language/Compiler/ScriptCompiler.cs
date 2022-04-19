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
      // RemoveElementAttributeFromAst(ref sb, element);
      // TransformElementAst(ref sb, element);
    }

    return script;
  }

  private void RemoveElementAttributeFromAst(ref ScriptBlockAst scriptBlock, ScriptElement element) {
  }

  private void TransformElementAst(ref ScriptBlockAst scriptBlock, ScriptElement element) {
    var transformer = new ScriptTransformer();
    element.TransformAst(transformer);
  }
}

internal class ScriptTransformer : IAstTransformer {
  public void TransformChild(ScriptElement scriptElement) {
    throw new NotImplementedException();
  }

  public void ReplaceAst(ref ScriptBlockAst scriptBlock, Ast replacement) {
    throw new NotImplementedException();
  }

  public void InsertStatement(ref ScriptBlockAst scriptBlock, StatementAst statement) {
    throw new NotImplementedException();
  }

  public StatementAst CreateStatement() {
    throw new NotImplementedException();
  }

  public void ReplaceScriptExtent(ref Ast ast, Ast replacementAst, string replacementExtent) {
    throw new NotImplementedException();
  }
}