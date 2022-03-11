using System.Management.Automation.Runspaces;
using PSyringe.Common.Language.Parsing;

namespace PSyringe.Common.Compiler;

public interface IScriptCompiler {
  /// <summary>
  ///   Reassembles the script element into a single script string that can be run by the powershell engine.
  ///   That script will be passed to a pipeline which contains the injected variables, templates and assemblies
  ///   required by the script.
  ///   The returned pipeline can be invoked through the Runspace host.
  /// </summary>
  /// <param name="scriptElement"></param>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public IScript CompileToScript(IScriptElement scriptElement);
}