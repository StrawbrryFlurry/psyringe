using PSyringe.Common.Compiler;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Common.Language.Compiler;

public interface IScriptCompiler {
  /// <summary>
  ///   Reassembles the script element into a single script string that can be run by the powershell engine.
  ///   That script will be passed to a pipeline which contains the injected variables, templates and assemblies
  ///   required by the script.
  ///   The returned pipeline can be invoked through the Runspace host.
  /// </summary>
  /// <param name="scriptDefinition"></param>
  /// <returns></returns>
  public ICompiledScript CompileScriptDefinition(IScriptDefinition scriptDefinition);
}