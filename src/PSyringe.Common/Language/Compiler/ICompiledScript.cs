using PSyringe.Common.Language.Elements;

namespace PSyringe.Common.Language.Compiler;

public interface ICompiledScript {
  /// <summary>
  ///   A list of provider dependencies that are
  ///   required to run this script.
  /// </summary>
  public IList<IScriptVariableDependency> Dependencies { get; }

  public IScriptDefinition ScriptDefinition { get; }

  //  public Task Invoke(IScriptInvocationContext context);
}