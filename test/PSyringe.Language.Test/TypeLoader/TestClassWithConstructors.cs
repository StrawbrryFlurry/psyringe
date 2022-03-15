namespace PSyringe.Language.Test.TypeLoader; 

public class TestClassWithConstructors {
  public TestClassWithConstructors(string foo) {
  }
  
  public TestClassWithConstructors(string foo, int bar) {
  }

  public TestClassWithConstructors(string foo, decimal? frank = null) {}
  
  public TestClassWithConstructors(decimal baz) {
  }
}