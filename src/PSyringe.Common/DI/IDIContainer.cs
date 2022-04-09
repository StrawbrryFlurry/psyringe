namespace PSyringe.Common.DI;

public interface IDIContainer {
  public T Resolve<T>(IScriptProvider target);
}