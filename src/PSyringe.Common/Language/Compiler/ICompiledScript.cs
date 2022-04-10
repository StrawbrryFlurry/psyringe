using PSyringe.Common.Language.Parsing;

namespace PSyringe.Common.Language.Compiler;

public interface ICompiledScript {
  /// <summary>
  ///   A list of provider dependencies that are
  ///   required to run this script.
  /// </summary>
  public IList<IScriptVariableDependency> Dependencies { get; }

  public IScriptElement ScriptElement { get; }

  //  public Task Invoke(IScriptInvocationContext context);
}