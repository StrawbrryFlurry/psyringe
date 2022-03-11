using System.Management.Automation;
using System.Management.Automation.Runspaces;
using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Parsing;
using PSyringe.Common.Runtime;

namespace PSyringe.Language.Compiler; 

public class ScriptCompiler : IScriptCompiler {
  public IScript CompileToScript(IScriptElement scriptElement) {
    return default;
  }
}
