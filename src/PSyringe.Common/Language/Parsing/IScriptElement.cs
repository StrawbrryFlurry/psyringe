namespace PSyringe.Common.Language.Parsing;

public interface IScriptElement {
  public IScriptVisitor Visitor { get;  }

}