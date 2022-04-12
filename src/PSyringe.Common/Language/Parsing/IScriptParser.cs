using PSyringe.Common.Language.Elements;

namespace PSyringe.Common.Language.Parsing;

public interface IScriptParser {
  public IScriptDefinition Parse(string script, IScriptParserVisitor visitor);
}