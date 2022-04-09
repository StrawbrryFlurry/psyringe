namespace PSyringe.Common.Language.Compiler;

public interface IScript {
  public string Name { get; }

  /// <summary>
  ///   A list of provider dependencies that are
  ///   required to run this script.
  /// </summary>
  public IList<IScriptVariableDependency> Dependencies { get; }
  //  public Task Invoke(IScriptInvocationContext context);
}