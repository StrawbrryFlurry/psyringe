namespace PSyringe.Common.Language.Parsing;

public interface IScriptParser {
  public IScriptElement Parse(string script, IScriptVisitor visitor);
}