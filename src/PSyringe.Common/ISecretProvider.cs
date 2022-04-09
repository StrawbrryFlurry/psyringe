namespace PSyringe.Common;

public class ISecretProvider {
  public T GetSecret<T>(string key) {
    return default;
  }

  public void SaveSecret<T>(string key, T value) {
  }
}