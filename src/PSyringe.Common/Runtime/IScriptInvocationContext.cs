namespace PSyringe.Common.Runtime;

public interface IScriptInvocationContext {
  public TimeSpan TimeElapsedSinceInvocation { get; }
}