using System.Management.Automation.Language;
using PSyringe.Common.Language.Elements;

namespace PSyringe.Common.Compiler;

public interface ICompiledScript {
  /// <summary>
  ///   A list of provider dependencies that are
  ///   required to run this script.
  /// </summary>
  public IList<IScriptVariableDependency> Dependencies { get; }

  /// <summary>
  ///   The script definition the compiler was called with
  /// </summary>
  public IScriptDefinition ScriptDefinition { init; get; }

  /// <summary>
  ///   The compiler generated ScriptBlock for the ScriptDefinition.
  /// </summary>
  public ScriptBlockAst ScriptBlock { get; }

  /// <summary>
  ///   Returns the invokable PowerShell code for
  ///   the compiled script definition.
  /// </summary>
  /// <returns>The script code</returns>
  public string GetScriptCode();
  //  public Task Invoke(IScriptInvocationContext context);
}