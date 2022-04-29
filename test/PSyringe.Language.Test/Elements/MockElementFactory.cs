using System;
using System.Linq;
using PSyringe.Common.Language.Elements;
using PSyringe.Language.Parsing;

namespace PSyringe.Language.Test.Elements;

public static class MockElementFactory<T> where T : ScriptElement {
  private static readonly ElementFactory _elementFactory = new();
  private static readonly ScriptParser _parser = new(_elementFactory);

  public static T CreateElement(string elementSource) {
    var scriptDef = _parser.Parse(elementSource);
    T element;

    try {
      element = (T) scriptDef.Elements.Single(e => e is T);
    }
    catch {
      throw new Exception("ElementSource didn't yield an element of type: " + typeof(T).Name);
    }

    return element;
  }
}