using System.Management.Automation.Language;

namespace PSyringe.Common.Language.Parsing;

public interface IScriptParserVisitor {
  public ScriptBlockAst Ast { get; }
  public bool HasVisited { get; }

  public IEnumerable<IAttributedScriptElement<FunctionDefinitionAst>> FunctionDefinitions { get; }
  public IEnumerable<IAttributedScriptElement<AttributedExpressionAst>> AttributedVariableExpressions { get; }

  public void Visit(ScriptBlockAst ast);
}