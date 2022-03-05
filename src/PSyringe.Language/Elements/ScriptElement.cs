using PSyringe.Common.Language.Parsing;

namespace PSyringe.Language.Elements; 

public class ScriptElement : IScriptElement {
  public IScriptVisitor Visitor { get; }

  public ScriptElement(IScriptVisitor visitor) {
    Visitor = visitor;
  }
}